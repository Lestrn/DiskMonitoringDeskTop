namespace Disk_Monitoring
{
    class ThreadParams
    {
        public RenamedEventHandler RenamedEventHandler { get; set; }
        public FileSystemEventHandler FileSystemEventHandler { get; set; }
        public string Path { get; set; }

    }
    class ThreadMethod
    {
        private bool _keepRun = true;
        public void KillThread()
        {
            _keepRun = false;
        }
        public void MonitoringDirectories(object pathObject)
        {
            ThreadParams parameters = pathObject as ThreadParams;
            FileSystemWatcher watcher = new FileSystemWatcher() { Path = parameters.Path };
            watcher.EnableRaisingEvents = _keepRun;
            watcher.Renamed += new RenamedEventHandler(parameters.RenamedEventHandler);
            watcher.Changed += new FileSystemEventHandler(parameters.FileSystemEventHandler);
            watcher.Created += new FileSystemEventHandler(parameters.FileSystemEventHandler);
            watcher.Deleted += new FileSystemEventHandler(parameters.FileSystemEventHandler);
        }
    }
    class DiskMonitoring
    {
        public string tempPath { get; private set; }
        public bool PathIsChanged { get; set; }
        private List<string> buffer = new List<string>(5);
        private Dictionary<string, Thread> _threads = new Dictionary<string, Thread>(10);
        private Dictionary<string, ThreadMethod> _threadsMethods = new Dictionary<string, ThreadMethod>(10);
        private Dictionary<string, string> _pathOldPathNew = new Dictionary<string, string>(10);
        private Dictionary<string, string> _savedFilePathes = new Dictionary<string, string>(10);
        private string[] allPathesAfterRenamed;

        public void GetAvailableDisks()
        {
            string[] availableDisks = Directory.GetLogicalDrives();
            Console.WriteLine("Available disks");
            foreach (var disk in availableDisks)
            {
                Console.WriteLine(disk);
            }
        }
        public string GetPathFromUser()
        {
            string diskPath;
            do
            {
                diskPath = Console.ReadLine();
                if (Path.IsPathFullyQualified(diskPath))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Your path / disk was unknown");
                }

            } while (true);
            return diskPath;
        }
        private bool IsFile(string path)
        {
            bool isFile;
            try                                                                 // Checking if path is directory or path 
            {
                string[] subfolders = Directory.GetDirectories(path);

                isFile = false;
            }
            catch (System.IO.IOException)
            {
                isFile = true;
            }
            return isFile;
        }
        private void ThreadCreation(string pathNew, string pathOld = "")
        {
            string path = pathOld.Replace(@"\\", @"\");
            if (pathOld != "") // Killing Previous thread if it existed
            {               
                _threadsMethods[pathOld].KillThread();
               // _threads[pathOld].Join();
               
            }
            ThreadParams parameters = new ThreadParams() { FileSystemEventHandler = WatcherChanged, Path = pathNew, RenamedEventHandler = WatcherRenamed };
            _threadsMethods[pathNew] = new ThreadMethod();
            Thread thread = new Thread(_threadsMethods[pathNew].MonitoringDirectories);
            thread.Start(parameters);
        }

        public void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            string path;
            if (_pathOldPathNew.ContainsKey(e.FullPath))
            {
                path = _pathOldPathNew[e.FullPath];
            }
            else
            {
                path = e.FullPath;
            }
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                if (IsFile(e.FullPath))
                {
                    _savedFilePathes[e.FullPath] = e.FullPath;
                }
                else
                {
                    ThreadCreation(path);
                }
            }
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                bool isFile = IsFile(e.FullPath);

                if (isFile)
                {
                    Console.WriteLine($"File was changed! Path: {e.FullPath} Name:{e.Name}");
                    FileInfo fileInfo = new FileInfo(e.FullPath);
                    try
                    {
                        Console.WriteLine($"Size of file {fileInfo.Length / 1024}kb");
                    }
                    catch
                    {

                    }
                    return;
                }

            }
            else
            {
                Console.WriteLine($"Directory changed with path: {e.FullPath}\nType: {e.ChangeType}\nName: {e.Name}");
            }
        }
        public void WatcherRenamed(object sender, RenamedEventArgs e)
        {
     
            Console.WriteLine($"Directory was renamed old path: {e.OldFullPath}\n" +
                $"New path  {e.FullPath}\n" +
                $"Old path  {e.OldFullPath}\n" +
                $"Old Name: {e.OldName}\n" +
                $"New Name: {e.Name}");
            if (IsFile(e.FullPath))
            {
                _savedFilePathes[e.OldFullPath] = e.FullPath;
                return;
            }
            ThreadCreation(e.FullPath, e.OldFullPath);
            List<List<string>> directories = new List<List<string>>(10);
            directories.Add(Directory.GetDirectories(e.FullPath).ToList());
            for (int i = 0; i < directories.Count; i++)
            {
                for (int j = 0; j < directories[i].Count; j++)
                {
                    directories.Add(Directory.GetDirectories(directories[i][j]).ToList());
                }
            }
            foreach (var item in directories)
            {
                foreach (var filename2 in item)
                {
                    if (!IsFile(filename2))
                    {
                        ThreadCreation(filename2);
                    }
                }
            }
        }
        public void DiskMemoryLeft(string diskPath)
        {
            var drive = new DriveInfo(diskPath);
            if (!drive.IsReady)
            {
                return;
            }
            long diskSizeLeftByte = drive.AvailableFreeSpace;
            int diskSizeLeftMb = (int)(diskSizeLeftByte / 1048576);
            Console.WriteLine($"Total disk space left: {diskSizeLeftMb}mb");
        }
        public void GetGeneralInfo(string diskPath)
        {
            var drive = new DriveInfo(diskPath);
            if (!drive.IsReady)
            {
                Console.WriteLine("Disk is not ready");
                return;
            }
            long diskSizeByte = drive.TotalSize;
            int diskSizeMb = (int)(diskSizeByte / 1048576);
            Console.WriteLine($"Ok i will be monitoring {Path.GetPathRoot(diskPath)}");
            Console.WriteLine($"Total disk space: {diskSizeMb}mb");
        }

    }
    class Program
    {
        private static void Main()
        {

            DiskMonitoring monitorer = new DiskMonitoring();
            monitorer.GetAvailableDisks();
            Console.WriteLine("Welcome! which disk should I monitor?");
            string diskPath = monitorer.GetPathFromUser();
            monitorer.GetGeneralInfo(diskPath);
            monitorer.DiskMemoryLeft(diskPath);
            ThreadMethod threadMethod = new ThreadMethod();
            ThreadParams threadParams = new ThreadParams() { FileSystemEventHandler = monitorer.WatcherChanged, Path = diskPath, RenamedEventHandler = monitorer.WatcherRenamed };
            new Thread(threadMethod.MonitoringDirectories).Start(threadParams);
            Console.ReadLine();

        }
    }
}