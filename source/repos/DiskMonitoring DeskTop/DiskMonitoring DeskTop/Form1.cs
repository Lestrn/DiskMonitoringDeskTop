using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        private bool keepRun = true;
        private static Thread monitorMemory;
        private static ParamsMemoryMonitoring paramsMemory = new ParamsMemoryMonitoring();
        public DiskMonitoring()
        {
            InitializeComponent();
        }

        
        private void FileChangesBtn_Click(object sender, EventArgs e)
        {
            FileChangesShowInfo();
        }
        private void FileChangesShowInfo()
        {
            path = GetPathFromUser();
            MonitoringDirectories(path);            
        }
        private void MemoryChangesBtn_Click(object sender, EventArgs e)
        {
            if (keepRun)
            {
                MemoryChangesStart();
                return;
            }
            MemoryChangesStop();
           
        }
        private void MemoryChangesStart()
        {
            if (string.IsNullOrEmpty(path))
            {
                path = GetPathFromUser();
            }                
            keepRun = false;      
            MemoryChangesLabel.ResetText();
            monitorMemory = new Thread(MemoryMonitoring);
            paramsMemory.Path = path;
            paramsMemory.KeepRun = true;
            monitorMemory.Start(paramsMemory);
            MemoryChangesBtn.Text = "Stop Monitoring";
        }
        private void MemoryChangesStop()
        {
            keepRun = true;
            paramsMemory.KeepRun = false;
            MemoryChangesBtn.Text = "Start Monitoring";
        }
    }
}
