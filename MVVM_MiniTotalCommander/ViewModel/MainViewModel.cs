using System;
using System.Windows.Input;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows;
namespace MVVM_MiniTotalCommander.ViewModel
{
    public class MainViewModel
    {
        public TCPanelViewModel LeftPanel { get; set; }
        public TCPanelViewModel RightPanel { get; set; }
        public MainViewModel()
        {
            LeftPanel = new TCPanelViewModel();
            RightPanel = new TCPanelViewModel();
        }
        // Handling button click with ICommand
        // This method copies file from one path to another
        private ICommand copy;
        public ICommand Copy => copy ?? (copy =
            new RelayCommand(o =>
            {
                try
                {

                    if (LeftPanel.IsSelected == true)
                    {
                        string source = LeftPanel.CurrentPath + LeftPanel.ListBoxSelectedItem;
                        string destination = RightPanel.CurrentPath + LeftPanel.ListBoxSelectedItem;
                        MVVM_MiniTotalCommander.Model.FileOperations.copyElem(source, destination);
                    }
                    else if (RightPanel.IsSelected == true)
                    {
                        string source = RightPanel.CurrentPath + RightPanel.ListBoxSelectedItem;
                        string destiantion = LeftPanel.CurrentPath + RightPanel.ListBoxSelectedItem;
                        File.Copy(source, destiantion);
                    }
                    // Reloading left and right panel content
                    LeftPanel.panelTC.getDirectoriesAndFiles(LeftPanel.CurrentPath);
                    LeftPanel.DirsAndFiles = new ObservableCollection<string>(LeftPanel.panelTC.SubDirsAndFiles);
                    RightPanel.panelTC.getDirectoriesAndFiles(RightPanel.CurrentPath);
                    RightPanel.DirsAndFiles = new ObservableCollection<string>(RightPanel.panelTC.SubDirsAndFiles);
                }
                catch (System.UnauthorizedAccessException)
                {
                    MessageBox.Show("You can not access this path.", "Error", MessageBoxButton.OK);
                }
                catch (DirectoryNotFoundException)
                {
                    MessageBox.Show("Could not find target path.", "Error", MessageBoxButton.OK);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Wrong file.", "Error", MessageBoxButton.OK);
                }
                catch (IOException)
                {
                    MessageBox.Show("The file already exists.", "Error", MessageBoxButton.OK);
                }

            }, null));
    }
}
