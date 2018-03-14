using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStoreDAL.Models
{
    public class PetDetails
    {
        [Key]
        public int PetId { get; set; }
        public string PetName { get; set; }
        public string ImagePath { get; set; }
        public int Age { get; set; }
        public float Price { get; set; }
        public string BreedType { get; set; }

        public int TypeId { get; set; }
        public virtual Pet pet { get; set; }


    }
}
