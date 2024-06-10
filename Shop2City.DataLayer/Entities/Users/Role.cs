using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Shop2City.Const;

namespace Shop2City.DataLayer.Entities.Users
{ 
    public class Role
    {

        #region Ctor
        public Role()
        {

        }

        #endregion

        #region Field
        [Key]
        public int roleId { get; set; }
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = ErrorMessage.Required)]
        [MaxLength(50, ErrorMessage = ErrorMessage.MaxLength)]
        public string roleTitle { get; set; }

        public bool isDelete { get; set; }

        #endregion

        #region Relationship
        public List<UserRole> userRoles { get; set; }
        #endregion
    }
}
