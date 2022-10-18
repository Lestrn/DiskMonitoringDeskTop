﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DiskMonitoring_DeskTop
{
    delegate void SetTextCallback(string text, bool isMemory);
    public partial class DiskMonitoring : Form
    {
        private static FileSystemWatcher watcher;
        private bool IsFile(string path)
        {
            bool isFile = false;
            try                                                                 // Checking if path is directory or path 
            {
                string[] subfolders = Directory.GetDirectories(path);
                isFile = false;
            }
            catch (System.IO.IOException)
            {
                isFile = true;
            }
            catch (Exception)
            {

            }
            return isFile;
        }
        private void Printer(string text, bool isMemory = false)
        {
            text += "\n";
            switch (isMemory)
            {
                case false:
                    if (this.FileChangesLable.InvokeRequired)
                    {
                        SetTextCallback d = new SetTextCallback(Printer);
                        this.Invoke(d, new object[] { text, false });
                    }
                    else
                    {
                        FileChangesLable.Text += text;
                        flowLayoutPanel1.AutoScrollPosition = new System.Drawing.Point(flowLayoutPanel1.HorizontalScroll.Maximum, flowLayoutPanel1.VerticalScroll.Maximum);
                    }
                    break;
                case true:
                    if (this.MemoryChangesLabel.InvokeRequired)
                    {
                        SetTextCallback d = new SetTextCallback(Printer);
                        this.Invoke(d, new object[] { text, true });
                    }
                    else
                    {
                        MemoryChangesLabel.Text += text;
                        flowLayoutPanel2.AutoScrollPosition = new System.Drawing.Point(flowLayoutPanel2.HorizontalScroll.Maximum, flowLayoutPanel2.VerticalScroll.Maximum);
                    }
                    break;
            }
     
        } 
        private void MemoryMonitoringAddon(string pathRoot, long temp, long driveSizeMb)
        {
            if (temp > driveSizeMb)
            {
                Printer(new String('*', 50), true);
                Printer($"Memory of disk changed! ({pathRoot})\nTotal memory space + {temp - driveSizeMb}kb", true);
                Printer(new String('*', 50), true);
                
            }
            else if (temp < driveSizeMb)
            {
                Printer(new String('*', 50), true);
                Printer($"Memory of disk changed! ({pathRoot})\nTotal memory space  {temp - driveSizeMb}kb", true);
                Printer(new String('*', 50), true);
            }
        }
        private void WatcherChanged(object sender, FileSystemEventArgs e)
        {
            Random random = new Random();

            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                bool isFile = IsFile(e.FullPath);

                if (isFile)
                {
                    Printer($"File was changed! Path: {e.FullPath} Name:{e.Name}");
                    FileInfo fileInfo = new FileInfo(e.FullPath);
                    try
                    {
                        Printer($"Size of file {fileInfo.Length / 1024}kb");
                    }
                    catch
                    {
                        Printer($"Accsess to file was denied!, {e.FullPath}");
                    }
                }
            }
            else
            {
                Printer($"Directory changed with path: {e.FullPath}\nType: {e.ChangeType}\nName: {e.Name}");

            }
        }
        private void Watcher_Error(object sender, ErrorEventArgs e)
        {
            Exception ex = e.GetException();
            Printer(ex.Message);
        }
        private void WatcherRenamed(object sender, RenamedEventArgs e)
        {
            Random random = new Random();
            Printer($"Directory was renamed old path: {e.OldFullPath}\n" +
                $"New path  {e.FullPath}\n" +
                $"Old path  {e.OldFullPath}\n" +
                $"Old Name: {e.OldName}\n" +
                $"New Name: {e.Name}");
        }
        public void GetAvailableDisks()
        {
            string[] availableDisks = Directory.GetLogicalDrives();
            Printer("Available disks");
            foreach (var disk in availableDisks)
            {
                Printer(disk);
            }
        }
        public void MemoryMonitoring(object pathObj)
        {
            string path = pathObj as string;
            string pathRoot = Path.GetPathRoot(path);
            var driveInfo = new DriveInfo(Path.GetPathRoot(pathRoot));
            long driveSizeMb = driveInfo.AvailableFreeSpace / 1000; //kb
            long temp;
            while (true)
            {
                Thread.Sleep(4000);
                temp = driveInfo.AvailableFreeSpace / 1000; //kb
                MemoryMonitoringAddon(pathRoot, temp, driveSizeMb);
                driveSizeMb = temp;
            }
        }
        public string GetPathFromUser()
        {
            string diskPath;
            using (var fbd = new FolderBrowserDialog())
            {
                while (true)
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                    {
                        diskPath = fbd.SelectedPath;
                        break;
                    }                
                }
            }
            FileChangesLable.ResetText();
            GetGeneralInfo(diskPath);
            DiskMemoryLeft(diskPath);
            return diskPath;
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
            Printer($"Total disk space left: {diskSizeLeftMb}mb");
        }
        public void GetGeneralInfo(string diskPath)
        {
            var drive = new DriveInfo(Path.GetPathRoot(diskPath));
            if (!drive.IsReady)
            {
                Printer("Disk is not ready");
                return;
            }
            long diskSizeByte = drive.TotalSize;
            int diskSizeMb = (int)(diskSizeByte / 1048576);
            Printer($"Ok i will be monitoring {diskPath}");
            Printer($"Total disk space ({Path.GetPathRoot(diskPath)}): {diskSizeMb}mb");
        }
        public void MonitoringDirectories(string path)
        {
            watcher = new FileSystemWatcher() { Path = path };
            watcher.EnableRaisingEvents = true;
            watcher.IncludeSubdirectories = true;
            watcher.Renamed += new RenamedEventHandler(WatcherRenamed);
            watcher.Changed += new FileSystemEventHandler(WatcherChanged);
            watcher.Created += new FileSystemEventHandler(WatcherChanged);
            watcher.Deleted += new FileSystemEventHandler(WatcherChanged);
            watcher.Error += Watcher_Error;
            GC.KeepAlive(watcher);
        }

    }
}