using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace MVVM_MiniTotalCommander.Model
{
    public class PanelTC
    {
        public List<string> AvailableDirectories = new List<string>();
        public List<string> SubDirsAndFiles = new List<string>();
        public string CurrentPath { get; set; }

        public void searchForAvailableDrives()         // as name suggest this method will get every avaliable 
        {                                              // drive both logical and physical
            AvailableDirectories.Clear();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady == true)
                    AvailableDirectories.Add(drive.Name);
            }
        }

        public void getDirectoriesAndFiles(string path) // searches directory and creates
                                                         // list of files and subdirectories
        {
            SubDirsAndFiles.Clear();
            if (path != Path.GetPathRoot(path)) // checking if given path is root or subdirectory.
                                                // If path is not root then add ".." to subDirsAndFiles property.
            {
                SubDirsAndFiles.Add("..");
            }
            foreach (var file in Directory.GetDirectories(path))
            {
                SubDirsAndFiles.Add("<D>"+file.Remove(0,file.LastIndexOf("\\")+1));
            }
            foreach (var file in Directory.GetFiles(path))
            {
                SubDirsAndFiles.Add(file.Remove(0, file.LastIndexOf("\\") + 1));
            }
        }

    }
}
