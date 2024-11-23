using Shop2City.DataLayer.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop2City.DataLayer.Entities.Advices
{
    public class AdviceModel:BaseEntity
    {
        public string Status { get; set; }
        public string ReturnId { get; set; }
        public string Message { get; set; }
    }
}
