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
        string path;
        bool memoryIsRunning;
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
            MemoryChangesStart();
        }
        private void MemoryChangesStart()
        {
            MemoryChangesLabel.ResetText();
            Thread monitorMemory = new Thread(MemoryMonitoring);
            monitorMemory.Start(path);
            memoryIsRunning = true;
        }
        private void MemoryChangesStop()
        {

        }
    }
}
