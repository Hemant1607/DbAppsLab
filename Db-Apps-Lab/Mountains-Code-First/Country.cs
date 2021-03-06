using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mountains_Code_First
{
    public class Country
    {
        public Country()
        {
            this.Mountains = new HashSet<Mountain>();
        }

        public virtual ICollection<Mountain> Mountains { get; set; }

        [Key]
        [StringLength(2,MinimumLength =2)]
        [Column(TypeName ="char")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }


    }
}
