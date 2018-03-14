using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreDAL.Models
{
    public class Pet
    {
        [Key]
        public int TypeId { get; set; }
        public string PetType { get; set; }

        public virtual ICollection<PetDetails> petDetails { get; set; }

    }
}
