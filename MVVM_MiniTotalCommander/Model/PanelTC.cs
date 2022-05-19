using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace MVVM_MiniTotalCommander.Model
{
    public class PanelTC
    {
        private ObservableCollection<string> availableDirectories { get; set; }
        public ObservableCollection<string> AvailableDirectories { get => availableDirectories; set => availableDirectories = value; }
        private ObservableCollection<string> subDirsAndFiles { get; set; }
        public ObservableCollection<string> SubDirsAndFiles { get => subDirsAndFiles; set => subDirsAndFiles = value; }
        private string currentPath { get; set; }
        public string CurrentPath { get => currentPath; set => currentPath = value; }
        public PanelTC()
        {
            availableDirectories = new ObservableCollection<string>();
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

        public void getDirectoriesAndFiles(string path) // searches directory and creates
                                                         // list of files and subdirectories
        {
            subDirsAndFiles = new ObservableCollection<string>();
            if (path != Path.GetPathRoot(path)) // checking if given path is root or subdirectory. If path is not root then add ".." to subDirsAndFiles property.
            {
                subDirsAndFiles.Add("..");
            }
            foreach (var file in Directory.GetFileSystemEntries(path, "*").Select(Path.GetFileName))
            {
                if(file[0] != '$')
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
                
            }
            subDirsAndFiles = new ObservableCollection<string>(subDirsAndFiles.OrderBy(x => x));
        }

    }
}
