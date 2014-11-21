namespace CinemaLib
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Зал кинотеатра Хранит коллекцию кресел
    /// </summary>
    public class Hall
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Chair> Chairs{ get; set; }
    }
}
