using System;
using System.Windows.Input;
using System.ComponentModel;
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
                Console.WriteLine(LeftPanel.CurrentPath);
            }, null));
    }
}
