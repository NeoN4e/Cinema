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

using System.Data.Entity;
using CinemaLib;


namespace CinemaWPF
{
    /// <summary>
    /// Логика взаимодействия для FilmEdit.xaml
    /// </summary>
    public partial class FilmEdit : Window
    {
        Film edititem;
  
        public FilmEdit(Film edititem)
        {
            InitializeComponent();
            this.edititem = edititem;

            this.Date.Text = DateTime.Now.ToString();

            //Binding
            this.Hall.ItemsSource = StaticDB.db.Halls.Local;
            this.Hall.DisplayMemberPath = "Name";

            this.TbName.SetBinding(TextBox.TextProperty, new Binding() { Source = edititem, Path = new PropertyPath("Name"), Mode = BindingMode.TwoWay } );
            this.TbDescription.SetBinding(TextBox.TextProperty, new Binding() { Source = edititem, Path = new PropertyPath("Description"), Mode = BindingMode.TwoWay } );

            this.SessionDGrid.ItemsSource = edititem.Sessions;
            this.SessionDGrid.AutoGenerateColumns = false;
            //this.SessionDGrid.ItemsSource = StaticDB.db.Films.Local;
            this.SessionDGrid.Columns.Add(new DataGridTextColumn() { Header = "Date", Binding = new Binding("Date") });
            this.SessionDGrid.Columns.Add(new DataGridComboBoxColumn() { Header = "Hall", ItemsSource = StaticDB.db.Halls.Local, DisplayMemberPath = "Name" ,SelectedItemBinding = new Binding("Hall") });
        }

         private void Button_Close(object sender, RoutedEventArgs e)
        {
            StaticDB.db.Films.Load();
            this.Close();
        }

        private void Button_Save(object sender, RoutedEventArgs e)
        {
            StaticDB.Add(edititem);
        }

        //private void AddSession_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Session s = new Session();
        //        edititem.Sessions.Add(s);
                
        //        //CinemaDB db = StaticDB.db;
        //        //db.Films.Add(edititem);
        //        //db.SaveChanges();

        //        //db.Sessions.Add(new Session() { Film = edititem , Date = DateTime.Now});
        //        //db.SaveChanges();
        //        //edititem.Sessions.Add(new Session() { Film = edititem });
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

    }
}
