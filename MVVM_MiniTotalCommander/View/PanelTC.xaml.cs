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

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                    nameof(currPath),
                    typeof(string),
                    typeof(PanelTC)
                );
        public string currPath
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public static readonly DependencyProperty ComboBoxContentProperty =
            DependencyProperty.Register(
                    nameof(comboContent),
                    typeof(List<String>),
                    typeof(PanelTC)
                );
        public List<String> comboContent
        {
            get { return (List<String>)GetValue(ComboBoxContentProperty); }
            set { SetValue(ComboBoxContentProperty, value); }
        }

        public static readonly DependencyProperty ListBoxContentProperty =
            DependencyProperty.Register(
                    nameof(listBoxContent),
                    typeof(List<String>),
                    typeof(PanelTC)
                );
        public List<String> listBoxContent
        {
            get { return (List<String>)GetValue(ListBoxContentProperty); }
            set { SetValue(ListBoxContentProperty, value); }
        }

        public static readonly RoutedEvent evComboBox_ContextMenuOpening =
        EventManager.RegisterRoutedEvent(nameof(evComboBox_ContextMenuOpening_handler),
                   RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                   typeof(PanelTC));


        public event RoutedEventHandler evComboBox_ContextMenuOpening_handler
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

        private void ComboBox_ContextMenuOpening(object sender, EventArgs e)
        {
            Console.WriteLine("CC: " + comboContent.Count);
            raise_ComboBox_ContextMenuOpening();
        }


        public static readonly RoutedEvent ListBox_doubleClick =
       EventManager.RegisterRoutedEvent(nameof(ListBox_doubleClick_handler),
                    RoutingStrategy.Bubble, typeof(RoutedEventHandler),
                    typeof(PanelTC));

        public event RoutedEventHandler ListBox_doubleClick_handler
        {
            add { AddHandler(ListBox_doubleClick, value); }
            remove { RemoveHandler(ListBox_doubleClick, value); }
        }

        void raiseListBox_doubleClick()
        {
            RoutedEventArgs newEventArgs =
                    new RoutedEventArgs(PanelTC.ListBox_doubleClick);
            RaiseEvent(newEventArgs);
        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            raiseListBox_doubleClick();
        }

    }
}
