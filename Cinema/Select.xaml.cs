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

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для Select.xaml
    /// </summary>
    public partial class Select : Window
    {
        System.Collections.IEnumerable enumerator;

        public Select(object Param)
        {
            //if (!( Param is IQueryable)){this.Close();return;}

            InitializeComponent();

            try
            {
                enumerator = Param as System.Collections.IEnumerable;
                datagrid.ItemsSource = enumerator;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

            //IQueryable enumerator = (Param as IQueryable);

            //datagrid.ItemsSource = enumerator;
            //datagrid.AutoGenerateColumns = false;
            //datagrid.Columns.Clear();

            //foreach (var item in enumerator.ElementType.GetProperties())
            //{
            //    if (!item.Name.Contains("_")) // Исключим ИД
            //    {
            //        //if (item.PropertyType.IsValueType || item.PropertyType == typeof(string))
            //        datagrid.Columns.Add(new DataGridTextColumn() { Header = item.Name, Binding = new Binding(item.Name), IsReadOnly = true });

            //        //else
            //        //datagrid.Columns.Add(new DataGridComboBoxColumn() { Header = item.Name, ItemsSource = db.Authors, SelectedItemBinding = new Binding(item.Name) });

            //    }


            //}
        }

        private void datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            //Определим колонки ДатаГрид

            //Переберем все элементы входящей коллекции
            //foreach (var item in enumerator)
            //{ 

            //}
            
        }

    }
}
