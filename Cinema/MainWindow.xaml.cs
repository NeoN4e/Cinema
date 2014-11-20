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

namespace Cinema
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CinemaDBEntities db = new CinemaDBEntities();

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                db.Films.Load();
                this.FilmsView.ItemsSource = db.Films.Local;
            }
            catch(Exception ex)
            { MessageBox.Show(ex.Message); }
 
            
        }

        private void FilmsView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            
            if (e.NewValue is Session)
            {
                Session s = (e.NewValue as Session);
               
                //Выберем только те билеты которые небыли проданны
                // this.infoView.ItemsSource = db.Chairs.SqlQuery("Select * from Chairs Where Id not in(Select Id_Chair from Tickets where Id_Session = @p0)",s.Id).ToList();

                //Получим колекцию проданных билетов
                List<Chair> tikets = (from t in db.Tickets select t.Chair).ToList();

                //Перебирем все сидения в Зале
                this.InfoGrid.Children.Clear();
                foreach (Chair item in db.Chairs.SqlQuery("Select * from Chairs Where Id_Hall =@p0", s.Id_Hall))
                {
                    Button b = new Button() { Margin = new Thickness(5), Content = item.C, Tag = item};
                    b.Click += (bts, bte) =>
                    {
                        //Продадим билет
                        Button bt = (bts as Button);
                        bt.IsEnabled = false;
                        db.Tickets.Add(new Ticket() { Chair = (bt.Tag as Chair), Price = 50, Id_Session = s.Id });
                        db.SaveChanges();

                    };

                    //Если Билет продан сделаем кнопку не активной
                    if (tikets.Contains(item)) b.IsEnabled = false;
                    
                    Grid.SetColumn(b, item.C-1);
                    Grid.SetRow(b, item.R-1);

                    //Добавим строку
                    while (this.InfoGrid.RowDefinitions.Count < item.R)
                        this.InfoGrid.RowDefinitions.Add(new RowDefinition());

                    //Добавим Колонку
                    while (this.InfoGrid.ColumnDefinitions.Count < item.C)
                        this.InfoGrid.ColumnDefinitions.Add(new ColumnDefinition());

                    this.InfoGrid.Children.Add(b);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                Film film = new Film();
                Edit eWindow = new Edit(film);
                
                eWindow.ShowDialog();
                
                db.Films.Add(film);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FilmsView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if ((sender as TreeView).SelectedItem is Film)
            {
                Edit eWindow = new Edit((sender as TreeView).SelectedItem);
                eWindow.ShowDialog();

                db.SaveChanges();
            }


        }
    }
}
