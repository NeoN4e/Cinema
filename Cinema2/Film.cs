namespace CinemaLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class Film
    {
        public Film()
        { this.Sessions = new HashSet<Session>(); }

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        public virtual ICollection<Session> Sessions { get; set; }
    }
}
