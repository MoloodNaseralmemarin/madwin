using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Shop2City.Const;
using Shop2City.DataLayer.Entities.Products;

namespace Shop2City.DataLayer.Entities.Properties
{
    public class ProductProperty
    {
        #region ctor
        public ProductProperty()
        {

        }
        #endregion
        #region Field
        [Key]
        public int PropertyProductId { get; set; }

        public int ProductId { get; set; }

        public int PropertyId { get; set; }

        public int Price { get; set; }
        #endregion
        #region Relationship
        public Product Product { get; set; }
        public Property Property { get; set; }
        #endregion
    }
}
