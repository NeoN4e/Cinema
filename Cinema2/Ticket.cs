namespace CinemaLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    /// <summary>
    /// Коллекция проданных Билетов
    /// </summary>
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
    
        public virtual Chair Chair { get; set; }
        public virtual Session Session { get; set; }
   //   public virtual Hall Hall { get; set; }
    }
}
