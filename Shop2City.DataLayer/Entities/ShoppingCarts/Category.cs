using System.ComponentModel.DataAnnotations;

namespace Shop2City.DataLayer.Entities.ShoppingCarts
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name="Display Order")]
        public int DisplayOrder { get; set; }

        public DateTime CreateDateTime { get; set; } = DateTime.Now;
    }
}
