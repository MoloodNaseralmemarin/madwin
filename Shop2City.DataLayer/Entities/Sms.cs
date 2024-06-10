using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop2City.DataLayer.Entities
{
    public class Sms
    {

        public int SmsId { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public DateTime registerDateTime { get; set; }=DateTime.Now;

        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime? updateDateTime { get; set; }= DateTime.Now;

        public int SmsCount { get; set; }= 2;

        public string Message { get; set; }

        public string CellPhone { get; set; }

    }
}
