using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MVVM_MiniTotalCommander.Model
{
    public class PanelTC
    {
        private List<String> availableDirectories { get; set; }
        public List<String> AvailableDirectories { get => availableDirectories; set => availableDirectories = value; }
        private List<String> subDirsAndFiles { get; set; }
        public List<String> SubDirsAndFiles { get => subDirsAndFiles; set => subDirsAndFiles = value; }
        private string currentPath { get; set; }
        public string CurrentPath { get => currentPath; set => currentPath = value; }
        public PanelTC()
        {
            availableDirectories = new List<String>();
            searchForAvailableDrives();
            currentPath = availableDirectories[0];
            getDirectoriesAndFiles(currentPath);
        }

        public void searchForAvailableDrives()         // as name suggest this method will get every avaliable 
        {                                              // drive both logical and physical
            availableDirectories.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady == true)
                    availableDirectories.Add(drive.Name);
            }
        }

        private void getDirectoriesAndFiles(string path) // searches directory and creates
                                                         // list of files and subdirectories
        {
            subDirsAndFiles = new List<String>();
            foreach (var file in Directory.GetFileSystemEntries(path, "*").Select(Path.GetFileName))
            {
                if (Path.HasExtension($"{path}" + file))
                {
                    subDirsAndFiles.Add(file);
                }
                else
                {
                    subDirsAndFiles.Add($"<D>" + file);
                }
            }
            subDirsAndFiles.Sort();
        }

    }
}
