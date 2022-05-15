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
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        #region properties
        private Model.PanelTC panelTC { get; set; }
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
        #endregion
        #region constructors and initialization of properties
        public MainViewModel()
        {
            panelTC = new Model.PanelTC();
            availableDirectories = new ObservableCollection<string>();
            dirsAndFiles = new ObservableCollection<string>();
            availableDirectories = panelTC.AvailableDirectories;
            dirsAndFiles = panelTC.SubDirsAndFiles;
            currentPath = panelTC.CurrentPath;
        }
        #endregion
        #region ICommand event handling

        // Handling ListBox item double click with ICommand
        // This method changes path and refreshes ListBox content
        private ICommand listBox_db_click;
        public ICommand ListBox_db_click => listBox_db_click ?? (listBox_db_click = new RelayCommand(o => Console.WriteLine("Test"), null));


        // Handling ComboBox Dropdown opening with ICommand
        // This method refreshes content of ComboBox
        private ICommand comboBox_ContentMenuOpening;
        public ICommand ComboBox_ContentMenuOpening => comboBox_ContentMenuOpening ?? (comboBox_ContentMenuOpening =
            new RelayCommand(o =>
            {
                panelTC.searchForAvailableDrives();
                availableDirectories = new ObservableCollection<string>(panelTC.AvailableDirectories);
                
            }, null));

        // Handling Combobox Dropdown close with ICommand
        // This method updates ListBox content
        private ICommand comboBox_DropdownClose;
        public ICommand ComboBox_DropdownClose => comboBox_DropdownClose ?? (comboBox_DropdownClose =
            new RelayCommand( o=>
            {
                panelTC.getDirectoriesAndFiles(CurrentPath);
                DirsAndFiles = new ObservableCollection<string>(panelTC.SubDirsAndFiles);

            }, null));

        // Handling button click with ICommand
        // This method copies file from one path to another
        private ICommand copy;
        public ICommand Copy => copy ?? (copy =
            new RelayCommand(o =>
            {
                Console.WriteLine(CurrentPath);
            }, null));
        #endregion
    }
}
