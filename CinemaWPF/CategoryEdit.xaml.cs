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
using Xceed.Wpf.Toolkit;

namespace CinemaWPF
{
    /// <summary>
    /// Логика взаимодействия для CategoryEdit.xaml
    /// </summary>
    public partial class CategoryEdit : Window
    {
        ChairCategory edititem;

        public CategoryEdit(ChairCategory edititem)
        {
            InitializeComponent();
            this.edititem = edititem;

            //Binding
            this.TbName.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = edititem, Mode = BindingMode.TwoWay } );
            
            this.ChairColor.SetBinding(ColorPicker.SelectedColorTextProperty, new Binding("Color") { Source = edititem, Mode = BindingMode.TwoWay });

            if (edititem.Color != null)
                this.ChairColor.SelectedColor = (Color)ColorConverter.ConvertFromString(edititem.Color);
        }

        
        private void Button_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            StaticDB.Add(edititem);
            this.Close();
        }

    }
}
