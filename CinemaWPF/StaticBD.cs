using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CinemaLib;

namespace CinemaWPF
{
    static class StaticDB
    {
        public static CinemaDB db = new CinemaDB();

        public static void Add(object obj)
        {
            //если ID не 0 не нужно поаторно добалять в базу
            if (!obj.GetType().GetProperty("Id").GetValue(obj).Equals(0))
            {
                //Для сохранения связанніх обїектов
                db.SaveChanges();
                return;
            }

            if(obj is Hall)
            {
                Hall h = obj as Hall;
                //if ( (from hi in db.Halls where hi.Id == h.Id select hi).Count() ==0)
                    db.Halls.Add(h);
            }

            if (obj is Film)
            {
                Film f = obj as Film;
                //if ( ! (from fi in db.Films select fi).Contains(f) )
                    db.Films.Add(f);
            }

            if (obj is Ticket)
                db.Tickets.Add(obj as Ticket);

            if (obj is ChairCategory)
                db.Category.Add(obj as ChairCategory);

            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            { System.Windows.MessageBox.Show(ex.Message); }
        }
    }
}
