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

using System.Data.Entity;
using CinemaLib;

namespace CinemaWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CinemaDB db = StaticDB.db;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            #region Инициализация Меню
            //Обработчик меню
            RoutedUICommand uicmd = new RoutedUICommand("MyCommand", "MyCommand", typeof(ApplicationCommands));
            MenuCatalogs.CommandBindings.Add(new CommandBinding(uicmd, MenuCatalogss_Click));

            //Пункты меню
            MenuCatalogs.Items.Add(new MenuItem() { Header = "Films", Command = uicmd, CommandParameter = db.Films.Local });
            MenuCatalogs.Items.Add(new MenuItem() { Header = "Halls", Command = uicmd, CommandParameter = db.Halls.Local });

            #endregion

            //Заполним таблицу фильмов
            try
            {
                db.Films.Load();
                db.Halls.Load();
                this.FilmsView.ItemsSource = db.Films.Local;
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            

        }

        //Обработчик нажатий пунктов меню Каталог
        private void MenuCatalogss_Click(object sender, RoutedEventArgs e)
        {
            object Param = (e.Source as MenuItem).CommandParameter;

            Lister lWindow = new Lister(Param);
            lWindow.ShowDialog();
        }

        private void FilmsView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            if (e.NewValue is Session)
            { }
        }

    }
}
