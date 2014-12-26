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

        void OpenEditWindow(object obj)
        {
            if (obj is Film)
            {
                FilmEdit fWindow = new FilmEdit(obj as Film);
                fWindow.ShowDialog();
            }

            if (obj is Hall)
            {
                HallEdit hWindow = new HallEdit(obj as Hall);
                hWindow.ShowDialog();
            }

            if (obj is ChairCategory)
            {
                CategoryEdit cWindow = new CategoryEdit(obj as ChairCategory);
                cWindow.ShowDialog();
            }
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            object item;
            //Добавим Фильм
            //if ((inputParam as System.Collections.IEnumerable).AsQueryable().ElementType == typeof(Film))
            //    item = new Film();
            
            ////ДобавимЗал
            //if ((inputParam as System.Collections.IEnumerable).AsQueryable().ElementType == typeof(Hall))
            //    item = new Hall();
            
            Type iType = (inputParam as System.Collections.IEnumerable).AsQueryable().ElementType;
            item = Activator.CreateInstance(iType);
            OpenEditWindow(item);
            //(inputParam as System.Collections.IList).Add(item);
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        
                //Редактируем Фильм
                //FilmEdit fWindow = new FilmEdit(datagrid.SelectedItem as Film);
                //fWindow.ShowDialog();
            if (datagrid.SelectedItem == CollectionView.NewItemPlaceholder)
            { Button_Add(sender, null); }
            else
                OpenEditWindow(datagrid.SelectedItem);
         
          
        }
    }
}