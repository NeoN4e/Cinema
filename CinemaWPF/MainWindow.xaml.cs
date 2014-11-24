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

            //Для обновления сеансов
            this.FilmsView.Items.Refresh();
        }

        private void FilmsView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue is Session)
            { 
                Session s = (e.NewValue as Session);
                this.HallName.Content = s.Hall.Name;

                //Получим колекцию проданных билетов
                List<Chair> tikets = (from t in db.Tickets where t.Session.Id == s.Id select t.Chair).ToList();

                //Перебирем все сидения в Зале
                this.ChairGrid.Children.Clear();
                foreach (Chair item in s.Hall.Chairs)
                {
                    //Добавим строку
                    while (this.ChairGrid.RowDefinitions.Count < item.Row + 1)
                        this.ChairGrid.RowDefinitions.Add(new RowDefinition());

                    //Добавим Колонку
                    while (this.ChairGrid.ColumnDefinitions.Count < item.Col + 1)
                        this.ChairGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    Button b = new Button() { Tag=item,Margin = new Thickness(5), Content = String.Format("{0} / {1}", item.Row + 1, item.Col + 1) };
                    b.Click += (bts, bte) =>
                    {
                        //Продадим билет
                        Button bt = (bts as Button);
                        bt.IsEnabled = false;

                        StaticDB.Add(new Ticket() { Session= s, Chair=(bts as Button).Tag as Chair,Price=50});
                    };
                    
                    //Если Билет продан сделаем кнопку не активной
                    if (tikets.Contains(item)) b.IsEnabled = false;

                    Grid.SetColumn(b, item.Col);
                    Grid.SetRow(b, item.Row);
                    this.ChairGrid.Children.Add(b);
                }
            }
        }

    }
}
