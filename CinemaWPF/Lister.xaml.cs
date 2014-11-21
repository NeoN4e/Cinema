using System;
using System.Collections;
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
using System.Data.Entity;


namespace CinemaWPF
{
    /// <summary>
    /// Логика взаимодействия для Lister.xaml
    /// </summary>
    public partial class Lister : Window
    {
        object inputParam;

        public Lister(object param)
        {
            this.inputParam = param;
            InitializeComponent();
 
            //Список наименований текущей коллекции
            datagrid.ItemsSource = (param as System.Collections.IEnumerable);
            datagrid.AutoGenerateColumns = false;
            datagrid.Columns.Add(new DataGridTextColumn() { Header = "Name", Binding = new Binding("Name"), Width = new DataGridLength(100,DataGridLengthUnitType.Star) ,IsReadOnly = true });
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            Film f = new Film();

            FilmEdit fWindow = new FilmEdit(f);
            fWindow.ShowDialog();

            (inputParam as System.Collections.IList).Add(f);
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
