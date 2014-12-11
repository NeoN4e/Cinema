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
        
        public DbSet<Film> Films { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Hall> Halls { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }

}
