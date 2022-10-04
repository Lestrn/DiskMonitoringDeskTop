

namespace Disk_Monitoring
{
    class DiskMonitoring
    {
        public string tempPath { get; private set; }
        public bool PathIsChanged { get; set; }
        List<string> buffer = new List<string>(5);
        public void GetAvailableDisks()
        {
            string[] availableDisks = Directory.GetLogicalDrives();
            Console.WriteLine("Available disks");
            foreach (var disk in availableDisks)
            {
                Console.WriteLine(disk);
            }
        }
        public  string GetPathFromUser()
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
                    Console.WriteLine("You path / disk was unknown");
                }

            } while (true);
            return diskPath;
        }
        public  void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                string pathExtension;
                pathExtension = Path.GetExtension(e.FullPath);
                bool bIsFile = false;

                try                                                                 // Checking if path is directory or path 
                {
                    string[] subfolders = Directory.GetDirectories(e.FullPath);

                    bIsFile = false;
                }
                catch (System.IO.IOException)
                {
                    bIsFile = true;
                }
                if (bIsFile)
                {
                    Console.WriteLine($"File was changed! Path: {e.FullPath} Name:{e.Name}");
                    return;
                }
                if (!buffer.Contains(e.FullPath))
                {                    
                    new Thread(this.MonitoringDirectories).Start(e.FullPath);
                }
                
                buffer.Add(e.FullPath);

            }
            else
            {
                Console.WriteLine($"Directory changed with path: {e.FullPath}\nType: {e.ChangeType}\nName: {e.Name}");
            }
        }
        public  void WatcherRenamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine($"Directory was renamed old path: {e.OldFullPath}\n" +
                $"New path  {e.FullPath}\n" +
                $"Old Name: {e.OldName}\n" +
                $"New Name: {e.Name}");
        }
        public  void DiskMemoryLeft(string diskPath)
        {
            long diskSizeLeftByte = new DriveInfo(diskPath).AvailableFreeSpace;
            int diskSizeLeftMb = (int)(diskSizeLeftByte / 1048576);
            Console.WriteLine($"Total disk space left: {diskSizeLeftMb}mb");
        }
        public  void GetGeneralInfo(string diskPath)
        {
            long diskSizeByte = new DriveInfo(diskPath).TotalSize;
            long diskSizeLeftByte = new DriveInfo(diskPath).AvailableFreeSpace;
            int diskSizeMb = (int)(diskSizeByte / 1048576);
            Console.WriteLine($"Ok i will be monitoring {Path.GetPathRoot(diskPath)}");
            Console.WriteLine($"Total disk space: {diskSizeMb}mb");
        }
        public void MonitoringDirectories(object pathObject)
        {
            string path = pathObject as string;
            FileSystemWatcher watcher = new FileSystemWatcher() { Path = path };
            watcher.EnableRaisingEvents = true;
            watcher.Renamed += new RenamedEventHandler(WatcherRenamed);
            watcher.Changed += new FileSystemEventHandler(WatcherChanged);
            var change = watcher.WaitForChanged(WatcherChangeTypes.All);
            Console.WriteLine($"Type of change: {change.ChangeType}\nName:{change.Name}");
            watcher.Deleted += new FileSystemEventHandler(WatcherChanged);
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
            while (true)
            {
                monitorer.GetGeneralInfo(diskPath);
                monitorer.DiskMemoryLeft(diskPath);
                new Thread(monitorer.MonitoringDirectories).Start(diskPath);
                string userStop = Console.ReadLine();
                if(userStop == "stop")
                {
                    break;
                }
                
            }
        }
    }
}