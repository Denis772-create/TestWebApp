using System.ComponentModel.DataAnnotations;
using System;

namespace TestWebApp.DAL.Models.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual long Id { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime DateOfCreation { get; set; }

        protected BaseEntity()
        {
            DateOfCreation = DateTime.Now;
        }
    }
}
