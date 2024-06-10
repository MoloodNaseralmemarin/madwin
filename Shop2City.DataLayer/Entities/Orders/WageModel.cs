
using Shop2City.Const;
using Shop2City.DataLayer.Entities.Common;
using System.ComponentModel.DataAnnotations;


namespace Shop2City.DataLayer.Entities.Ordering
{
    public class WageModel :BaseEntity
    {
        [Display(Name ="مقدار")]
        [Required(ErrorMessage =ErrorMessage.Required)]
        [Range(0,100,ErrorMessage =ErrorMessage.Range)]
        public int Value { get; set; }
    }
}
