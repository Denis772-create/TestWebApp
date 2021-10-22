using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace TestWebApp.DAL.Models.Entities
{
    public abstract class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual string Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfCreation { get; set; }

        protected BaseEntity()
        {
            DateOfCreation = DateTime.Now;
        }
    }
}
