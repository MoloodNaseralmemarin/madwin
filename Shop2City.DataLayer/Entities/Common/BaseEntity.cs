using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop2City.DataLayer.Entities.Common
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateDate= DateTime.Now;
            LastUpdateDate= DateTime.Now;
            IsDelete= false;
            Description = "توضیحی درج نشده است";
        }
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateDate { get; set; } 

        public DateTime LastUpdateDate { get; set; }
    }
}
