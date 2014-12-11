namespace CinemaLib
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using CinemaLib.Migrations; 

    public class CinemaDB : DbContext
    {
        static CinemaDB()
        {
            //http://habrahabr.ru/post/121132/
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CinemaDB>());
            //Database.SetInitializer(new CreateDatabaseIfNotExists<CinemaDB>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CinemaDB, Configuration>());
        }
        
        /// <summary>
        /// Список Фильмов
        /// </summary>
        public DbSet<Film> Films { get; set; }

        /// <summary>
        /// Список Сеансов
        /// </summary>
        public DbSet<Session> Sessions { get; set; }

        /// <summary>
        /// Список Залов
        /// </summary>
        public DbSet<Hall> Halls { get; set; }

        /// <summary>
        /// Список проданных билетов
        /// </summary>
        public DbSet<Ticket> Tickets { get; set; }

        /// <summary>
        /// Разные категории стульчиков (Стандарт Вип ....)
        /// </summary>
        public DbSet<ChairCategory> Category { get; set; }
    }

}
