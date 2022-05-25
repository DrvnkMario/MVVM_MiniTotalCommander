using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Input;
namespace MVVM_MiniTotalCommander.ViewModel
{
    public class TCPanelViewModel : INotifyPropertyChanged
    {
        #region properties
        public event PropertyChangedEventHandler PropertyChanged;
        public Model.PanelTC panelTC { get; set; }
        private ObservableCollection<string> availableDirectories { get; set; }
        public ObservableCollection<string> AvailableDirectories
        {
            get => availableDirectories;
            set
            {
                availableDirectories = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableDirectories)));
            }
        }
        private ObservableCollection<string> dirsAndFiles { get; set; }
        public ObservableCollection<string> DirsAndFiles
        {
            get => dirsAndFiles;
            set
            {
                dirsAndFiles = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DirsAndFiles)));
            }
        }
        private string currentPath { get; set; }
        public string CurrentPath
        {
            get => currentPath;
            set
            {
                currentPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentPath)));
            }
        }
        private string listBoxSelectedItem { get; set; }
        public string ListBoxSelectedItem
        {
            get =>listBoxSelectedItem;
            set
            {
                listBoxSelectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListBoxSelectedItem)));
            }
        }
        private bool isSelected { get; set; }
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
            }
        }
        #endregion
        #region constructors and initialization of properties
        public TCPanelViewModel()
        {
            panelTC = new Model.PanelTC();
            panelTC.searchForAvailableDrives();
            AvailableDirectories = panelTC.AvailableDirectories;
            CurrentPath = AvailableDirectories[0];
            panelTC.getDirectoriesAndFiles(CurrentPath);
            DirsAndFiles = panelTC.SubDirsAndFiles;
            isSelected = false;
        }
        #endregion
        #region ICommand event handling

        // Handling ListBox item double click with ICommand
        // This method changes path and refreshes ListBox content
        private ICommand listBox_db_click { get; set; }
        public ICommand ListBox_db_click => listBox_db_click ?? (listBox_db_click = new RelayCommand(o =>
        {
            if (ListBoxSelectedItem != null)
            {
                if (ListBoxSelectedItem.StartsWith("<D>") == true)
                {
                    if (Path.GetPathRoot(CurrentPath) == CurrentPath)
                    {
                        this.CurrentPath += ListBoxSelectedItem.Remove(0, 3) + "\\";
                    }
                    else
                    {
                        this.CurrentPath += ListBoxSelectedItem.Remove(0, 3) + "\\";
                    }
                }
                else if (ListBoxSelectedItem == "..")
                {
                    CurrentPath = CurrentPath.Remove(CurrentPath.Length - 1);
                    CurrentPath = Path.GetDirectoryName(CurrentPath);
                }
            }
            panelTC.getDirectoriesAndFiles(CurrentPath);
            DirsAndFiles = new ObservableCollection<string>(panelTC.SubDirsAndFiles);
        }, null));


        // Handling ComboBox Dropdown opening with ICommand
        // This method refreshes content of ComboBox
        private ICommand comboBox_ContentMenuOpening { get; set; }
        public ICommand ComboBox_ContentMenuOpening => comboBox_ContentMenuOpening ?? (comboBox_ContentMenuOpening =
            new RelayCommand(o =>
            {
                panelTC.searchForAvailableDrives();
                availableDirectories = new ObservableCollection<string>(panelTC.AvailableDirectories);
            }, null));

        // Handling Combobox Dropdown close with ICommand
        // This method updates ListBox content
        private ICommand comboBox_DropdownClose { get; set; }
        public ICommand ComboBox_DropdownClose => comboBox_DropdownClose ?? (comboBox_DropdownClose =
            new RelayCommand( o=>
            {
                panelTC.getDirectoriesAndFiles(CurrentPath);
                DirsAndFiles = new ObservableCollection<string>(panelTC.SubDirsAndFiles);

            }, null));

        private ICommand gotFocus { get; set; }
        public ICommand GotFocus => gotFocus ?? (gotFocus =
            new RelayCommand(o =>
           {
               isSelected = true;
           }, null));

        private ICommand lostFocus { get; set; }
        public ICommand LostFocus => lostFocus ?? (lostFocus =
            new RelayCommand(o =>
           {
               isSelected = false;
           }, null));
        #endregion
    }
}
