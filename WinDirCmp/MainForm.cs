using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinDirCmp
{
    public partial class MainForm : Form
    {

        private String directoryName1;
        private String directoryName2;
        private WinDir directory1;
        private WinDir directory2;

        public MainForm()
        {
            InitializeComponent();
            folderBrowserDialog1.Description = "Please select the first directory.";
            folderBrowserDialog1.Description = "Please select the second directory.";
        }

        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            directoryName1 = folderBrowserDialog1.SelectedPath;
            DialogResult result2 = folderBrowserDialog2.ShowDialog();
            if (result != DialogResult.OK)
            {
                return;
            }
            directoryName2 = folderBrowserDialog2.SelectedPath;
            FindDifferences();
        }

        private void FindDifferences()
        {
            treeListView1.ClearObjects();

            toolStripProgressBar1.Maximum = getDirectoryCount(directoryName1) + getDirectoryCount(directoryName2);
            toolStripProgressBar1.Value = 0;

            directory1 = analyzeDirectory1(0, directoryName1);
            directory2 = analyzeDirectory2(0, directoryName2);
        }

        private int getDirectoryCount(String directory)
        {
            int result = 0;
            foreach(String dir in Directory.GetDirectories(directory))
            {
                result += 1;
                result += getDirectoryCount(dir);
            }
            foreach (String file in Directory.GetFiles(directory))
            {
                result += 1;
            }
            return result;
        }
        private WinDir analyzeDirectory1(int depth, String directoryPath)
        {
            WinDir result = new WinDir(depth, directoryPath, Directory.GetDirectories(directoryPath), Directory.GetFiles(directoryPath));

            foreach (String dir in result.SubdirectoryNames)
            {
                WinDir temp = analyzeDirectory1(depth + 1, dir);
                result.Subdirectories.Add(temp);
                treeListView1.AddObject(temp);
                toolStripProgressBar1.Value += 1;
            }

            foreach (String file in result.FileNames)
            {
                WinDir temp = new WinDir(1, file, new string[0], new string[0]);
                FileInfo fi = new FileInfo(file);
                temp.Size = fi.Length;
                temp.Date = fi.LastAccessTime.ToString();
                result.Subdirectories.Add(temp);
                treeListView1.AddObject(temp);
                toolStripProgressBar1.Value += 1;
            }
            return result;
        }

        private WinDir analyzeDirectory2(int depth, String directoryPath)
        {
            WinDir result = new WinDir(depth, directoryPath, Directory.GetDirectories(directoryPath), Directory.GetFiles(directoryPath));

            foreach (String dir in result.SubdirectoryNames)
            {
                WinDir temp = analyzeDirectory2(depth + 1, dir);
                result.Subdirectories.Add(temp);
                treeListView2.AddObject(temp);
                toolStripProgressBar1.Value += 1;
            }

            foreach (String file in result.FileNames)
            {
                WinDir temp = new WinDir(1, file, new string[0], new string[0]);
                FileInfo fi = new FileInfo(file);
                temp.Size = fi.Length;
                temp.Date = fi.LastAccessTime.ToString();
                result.Subdirectories.Add(temp);
                treeListView2.AddObject(temp);
                toolStripProgressBar1.Value += 1;
            }
            return result;
        }
    }
}
