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
    /// Логика взаимодействия для HallEdit.xaml
    /// </summary>
    public partial class HallEdit : Window
    {
        Hall edititem;

        public HallEdit(Hall edititem)
        {
            InitializeComponent();
            this.edititem = edititem;
            this.TbName.SetBinding(TextBox.TextProperty, new Binding() { Source = edititem, Path = new PropertyPath("Name"), Mode = BindingMode.TwoWay });

            //Считаем все кресла
            foreach(Chair item in edititem.Chairs)
            {
                 //Добавим строку
                while (this.ChairGrid.RowDefinitions.Count < item.Row+1)
                    this.ChairGrid.RowDefinitions.Add(new RowDefinition());

                //Добавим Колонку
                while (this.ChairGrid.ColumnDefinitions.Count < item.Col+1)
                    this.ChairGrid.ColumnDefinitions.Add(new ColumnDefinition());


                Button b = new Button() { Margin = new Thickness(5), Content = item.Col + 1, Background = new SolidColorBrush( (Color)ColorConverter.ConvertFromString("#FF00FFFF") ) };
                b.Click += (bts, bte) =>
                {
                    //Удалим кресло
                    this.ChairGrid.Children.Remove(bts as Button);
                };

                Grid.SetColumn(b, item.Col);
                Grid.SetRow(b, item.Row);
                this.ChairGrid.Children.Add(b);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.ChairGrid.Children.Clear();
            this.ChairGrid.ColumnDefinitions.Clear();
            this.ChairGrid.RowDefinitions.Clear();

            for (int row = 0; row < int.Parse(this.Rows.Text); row++)
            {
                //Добавим строку
                this.ChairGrid.RowDefinitions.Add(new RowDefinition() );

                for (int col = 0; col < int.Parse(this.Columns.Text); col++)   
                {
                    if (row == 0) //Добавим Колонку
                        this.ChairGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    Button b = new Button() { Margin = new Thickness(5), Content = col+1};
                    
                    b.Click +=  (bts, bte) =>
                    {
                        //Удалим кресло
                        this.ChairGrid.Children.Remove(bts as Button);
                    };


                    Grid.SetColumn(b, col);
                    Grid.SetRow(b, row);

                    this.ChairGrid.Children.Add(b);
                }
            }
        }

        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            StaticDB.Add(edititem);

            edititem.Chairs.Clear();
            foreach (Button b in this.ChairGrid.Children)
            {
                Chair c = new Chair() { Row = Grid.GetRow(b), Col = Grid.GetColumn(b) };
                edititem.Chairs.Add(c);
            }

            StaticDB.Add(edititem);

            this.Close();
        }

    }
}
