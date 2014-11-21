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
using System.Windows.Shapes;

using CinemaLib;


namespace CinemaWPF
{
    /// <summary>
    /// Логика взаимодействия для FilmEdit.xaml
    /// </summary>
    public partial class FilmEdit : Window
    {
        Film edititem;

        public FilmEdit(Film edititem)
        {
            InitializeComponent();
            this.edititem = edititem;

            //Binding
            this.TbName.SetBinding(TextBox.TextProperty, new Binding() { Source = edititem, Path = new PropertyPath("Name"), Mode = BindingMode.TwoWay } );
            this.TbDescription.SetBinding(TextBox.TextProperty, new Binding() { Source = edititem, Path = new PropertyPath("Description"), Mode = BindingMode.TwoWay } );

            this.SessionDGrid.ItemsSource = edititem.Sessions;
            
            this.SessionDGrid.Columns.Add(new DataGridTextColumn() { Header = "Date", Binding = new Binding("Date") });
            this.SessionDGrid.Columns.Add(new DataGridComboBoxColumn() { Header = "Hall", ItemsSource = new CinemaDB().Halls , SelectedItemBinding = new Binding("Hall")});
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
