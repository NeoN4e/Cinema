namespace CinemaLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public class Session
    {
        public Session()
        { this.Tickets = new HashSet<Ticket>(); }

        [Key]
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        
        public virtual Film Film { get; set; }
        public virtual Hall Hall { get; set; }
        
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
