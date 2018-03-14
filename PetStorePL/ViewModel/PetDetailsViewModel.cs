using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PetStorePL.ViewModel
{
    public class PetDetailsViewModel
    {
        [Required]
        [Display(Name ="Pet Name")]
        public string PetName { get; set; }
        [Required(ErrorMessage = "Please select file")]
        [RegularExpression("", ErrorMessage = "Only Image files allowed.")]
        public string ImagePath { get; set; }
        [Required]
        [Range(0,100)]
        public int Age { get; set; }
        [Required]
        public float Price { get; set; }
        [Required, Display(Name = "Breed Type")]
        public string BreedType { get; set; }
        [Required]
        [Display(Name ="Pet Type")]
        public string PetType { get; set; }



        //public static implicit operator PetDetailsViewModel(User user)
        //{
        //    return new RegisterViewModel
        //    {
        //        UserID = user.UserID,
        //        FirstName = user.FirstName,
        //        UserName = user.UserName,
        //        Password = user.Password,
        //        ConfirmPassword = user.Password,
        //        Email = user.Email
        //    };
        //}

        //public static implicit operator User(RegisterViewModel vm)
        //{
        //    return new User
        //    {
        //        FirstName = vm.FirstName,
        //        LastName = vm.LastName,
        //        UserName = vm.UserName,
        //        Email = vm.Email,
        //        Password = vm.Password
        //    };
        //}
    }
}