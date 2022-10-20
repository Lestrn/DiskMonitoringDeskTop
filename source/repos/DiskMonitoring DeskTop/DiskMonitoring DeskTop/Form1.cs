using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiskMonitoring_DeskTop
{
  
    public partial class DiskMonitoring : Form
    {
        private string path;
        private bool keepMemoryMonitoringRun = true;
        private bool keepDiskMonitoringRun = true;
        private static Thread monitorMemory;
        private static ParamsMemoryMonitoring paramsMemory = new ParamsMemoryMonitoring();
        public DiskMonitoring()
        {
            InitializeComponent();
        }        
        private void FileChangesBtn_Click(object sender, EventArgs e)
        {
            if (keepDiskMonitoringRun)
            {
                StartMonitoringChanges();
                return;
            }
            StopMonitoringChanges();      
        }
        private void StartMonitoringChanges()
        {
            if (string.IsNullOrEmpty(path))
            {
                path = GetPathFromUser();
            }
            KeepWatch = true;
            MonitoringDirectories(path);
            watcher.EnableRaisingEvents = KeepWatch;
            FileChangesBtn.Text = "Stop Monitoring";
            keepDiskMonitoringRun = false;
        }
        private void StopMonitoringChanges()
        {
            KeepWatch = false;
            watcher.EnableRaisingEvents = KeepWatch;
            FileChangesBtn.Text = "Start Monitoring";
            keepDiskMonitoringRun = true;
        }
        private void MemoryChangesBtn_Click(object sender, EventArgs e)
        {
            if (keepMemoryMonitoringRun)
            {
                MemoryChangesStart();
                return;
            }
            StopMemoryChanges();          
        }
        private void MemoryChangesStart()
        {
            if (string.IsNullOrEmpty(path))
            {
                path = GetPathFromUser();
            }                
            keepMemoryMonitoringRun = false;      
            MemoryChangesLabel.ResetText();
            monitorMemory = new Thread(MemoryMonitoring);
            paramsMemory.Path = path;
            paramsMemory.KeepRun = true;
            monitorMemory.Start(paramsMemory);
            monitorMemory.IsBackground = true;
            MemoryChangesBtn.Text = "Stop Monitoring";
        }
        private void StopMemoryChanges()
        {
            keepMemoryMonitoringRun = true;
            paramsMemory.KeepRun = false;
            MemoryChangesBtn.Text = "Start Monitoring";
        }

        private void currentPathUsedLabel_Click(object sender, EventArgs e)
        {
            path = GetPathFromUser();
            if(watcher != null)
            {
                StopMonitoringChanges();
            }
        }

        private void DiskMonitoring_Closed(object sender, System.EventArgs e)
        {
            if (keepMemoryMonitoringRun && watcher != null) // Closing all threads after program was closed  if they were opened
            {
                StopMonitoringChanges();
            }
            if (keepDiskMonitoringRun)
            {
                StopMemoryChanges();
            }
        }

        private void OpenLogFilesBtn_Click(object sender, EventArgs e)
        {
            OpenLogDirectory();
        }
        private void OpenLogDirectory()
        {
            Program.CheckOrCreateLogDirectory();
            Process.Start(@"..\log");
        }

        private void DeleteLogFilesBtn_Click(object sender, EventArgs e)
        {
            DeleteFile(LOGPATHFILECHANGES);
            DeleteFile(LOGPATHMEMORYCHANGES);
        }
        private void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void IncludeSubDirectoriesCB_CheckedChanged(object sender, EventArgs e)
        {
            StopMonitoringChanges();
        }
    }
}
