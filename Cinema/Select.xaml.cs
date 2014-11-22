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
                
                datagrid.AutoGenerateColumns = false;
                datagrid.ItemsSource = enumerator;
                datagrid.IsReadOnly = true;

                foreach (System.Reflection.PropertyInfo item in enumerator.AsQueryable().ElementType.GetProperties())
                {
                    if (item.Name.Substring(0,2) != "Id" && (item.PropertyType.IsValueType || item.PropertyType == typeof(string))) // Исключим ИД
                    {
                        datagrid.Columns.Add(new DataGridTextColumn() { Header = item.Name, Binding = new Binding(item.Name), IsReadOnly = true });
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

       }

        private void datagrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

    }
}
