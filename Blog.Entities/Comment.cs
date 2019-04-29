using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Entities
{
    [Table("Comments")]
    public class Comment : MyEntityBase
    {
        [Required, StringLength(60)]
        public string NameSurname { get; set; }
        [Required, StringLength(70)]
        public string Email { get; set; }
        [Required, StringLength(200)]
        public string Text { get; set; }

        public virtual Post Post { get; set; }
        public virtual BlogUser Owner { get; set; }
    }
}
