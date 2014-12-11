namespace CinemaLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    /// <summary>Коллекция кресел в зале</summary>
    public class Chair
    {
        [Key]
        public int Id { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        //Определяет цвет стульчика :)
        public virtual ChairCategory Category { get; set; }
    }
}
