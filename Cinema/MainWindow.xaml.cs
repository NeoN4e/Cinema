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
                //Віберем только те билеты которые небыли проданны
                this.infoView.ItemsSource = db.Chairs.SqlQuery("Select * from Chairs Where Id not in(Select Id_Chair from Tickets where Id_Session = @p0)",s.Id).ToList();
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
    }
}
