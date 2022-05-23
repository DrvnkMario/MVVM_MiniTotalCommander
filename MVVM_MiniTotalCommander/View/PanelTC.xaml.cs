using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
namespace MVVM_MiniTotalCommander.View
{
    /// <summary>
    /// Logika interakcji dla klasy UserControl1.xaml
    /// </summary>
    public partial class PanelTC : UserControl
    {
        public PanelTC()
        {
            InitializeComponent();
        }
        #region currPath
        public static readonly DependencyProperty TextProperty = // creating DependencyProperty for currPath field 
            DependencyProperty.Register(
                    nameof(CurrentPath),
                    typeof(string),
                    typeof(PanelTC)
                );
        public string CurrentPath 
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        #region combobox
        public static readonly DependencyProperty ComboBoxContentProperty = // creating DependencyProperty for combobox,
                                                                            // this will allow proper binding of combobox to content
            DependencyProperty.Register(
                    nameof(comboContent),
                    typeof(List<string>),
                    typeof(PanelTC)
                );
        public List<string> comboContent
        {
            get { return (List<string>)GetValue(ComboBoxContentProperty); }
            set { SetValue(ComboBoxContentProperty, value); }
        }

        public static readonly RoutedEvent evComboBox_ContextMenuOpening = // registration of custom event for opening ComboBox context menu
        EventManager.RegisterRoutedEvent(nameof(evComboBox_ContextMenuOpening_handler),
                   RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                   typeof(PanelTC));

        public event RoutedEventHandler evComboBox_ContextMenuOpening_handler // evComboBox_ContextMenuOpening handler
        {
            add { AddHandler(evComboBox_ContextMenuOpening, value); }
            remove { RemoveHandler(evComboBox_ContextMenuOpening, value); }
        }

        void raise_ComboBox_ContextMenuOpening() 
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(PanelTC.evComboBox_ContextMenuOpening);
            RaiseEvent(newEventArgs);
        }

        private void ComboBox_ContextMenuOpening(object sender, EventArgs e) // This method will raise event on ComboBox Context Menu opening
        {
            raise_ComboBox_ContextMenuOpening();
        }
        public static readonly RoutedEvent evComboBox_DropdownClose = // registration of custom event for closing ComboBox context menu
       EventManager.RegisterRoutedEvent(nameof(evComboBox_DropdownClose_handler),
                  RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                  typeof(PanelTC));


        public event RoutedEventHandler evComboBox_DropdownClose_handler // evComboBox_DropdownClose handler
        {
            add { AddHandler(evComboBox_DropdownClose, value); }
            remove { RemoveHandler(evComboBox_DropdownClose, value); }
        }

        void raise_evComboBox_DropdownClose() 
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(PanelTC.evComboBox_DropdownClose);
            RaiseEvent(newEventArgs);
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e) // This method will raise event on ComboBox Dropdown being closed
        {
            raise_evComboBox_DropdownClose();
        }
        #endregion

        #region listbox
        public static readonly DependencyProperty selectedItemProperty =
            DependencyProperty.Register(nameof(selectedItem),
                typeof(string),
                typeof(PanelTC));

        public string selectedItem
        {
            get { return (string)GetValue(selectedItemProperty); }
            set { SetValue(selectedItemProperty, value); }
        }

        public static readonly DependencyProperty ListBoxContentProperty = // creating DependencyProperty for ListBox,
                                                                           // this will allow proper binding of ListBox to content
            DependencyProperty.Register(
                    nameof(listBoxContent),
                    typeof(List<string>),
                    typeof(PanelTC)
                );
        public List<string> listBoxContent
        {
            get { return (List<string>)GetValue(ListBoxContentProperty); }
            set { SetValue(ListBoxContentProperty, value); }
        }

        public static readonly RoutedEvent ListBox_doubleClick = // Registration of cutom event - doubleClick on ListBox content
       EventManager.RegisterRoutedEvent(nameof(ListBox_doubleClick_handler),
                    RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                    typeof(PanelTC));

        public event RoutedEventHandler ListBox_doubleClick_handler // ListBox_doubleClick handler
        {
            add { AddHandler(ListBox_doubleClick, value); }
            remove { RemoveHandler(ListBox_doubleClick, value); }
        }

        void raiseListBox_doubleClick() // This method will raise event on ListBox element getting doubleclicked
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(PanelTC.ListBox_doubleClick);
            RaiseEvent(newEventArgs);
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            raiseListBox_doubleClick();
        }
        #endregion
    }
}
