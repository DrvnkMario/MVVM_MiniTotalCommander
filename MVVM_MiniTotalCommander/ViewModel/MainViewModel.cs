using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Controls;
using System.IO;
using System.Windows.Input;
namespace MVVM_MiniTotalCommander.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Model.PanelTC panelTC { get; set; }
        private List<String> availableDrives { get; set; }
        public List<String> AvailableDrives
        {
            get => availableDrives;
            set
            {
                availableDrives = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AvailableDrives)));
            }
        }
        private List<String> dirsAndFiles { get; set; }
        public List<String> DirsAndFiles { get => dirsAndFiles; set => dirsAndFiles = value; }
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

        public MainViewModel()
        {
            panelTC = new Model.PanelTC();
            availableDrives = new List<String>();
            dirsAndFiles = new List<String>();
            availableDrives = panelTC.AvailableDirectories;
            dirsAndFiles = panelTC.SubDirsAndFiles;
            currentPath = panelTC.CurrentPath;
        }

        private ICommand listBox_db_click;
        public ICommand ListBox_db_click => listBox_db_click ?? (listBox_db_click = new RelayCommand(o => Console.WriteLine("Test"), null));

        private ICommand comboBox_ContentMenuOpening;
        public ICommand ComboBox_ContentMenuOpening => comboBox_ContentMenuOpening ?? (comboBox_ContentMenuOpening =
            new RelayCommand(o =>
            {
                panelTC.searchForAvailableDrives();
                availableDrives = new List<String>(panelTC.AvailableDirectories);
                Console.WriteLine(availableDrives.Count);
            }, null));

    }
}
