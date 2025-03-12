using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domains
{
    public class AddRestaurantViewModel
    {
        public int RestaurantId { get; set; }

        [Required(ErrorMessage = "Restaurant name is required.")]
        public string RestaurantName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string RestaurantAddress { get; set; }

        [Required(ErrorMessage = "Owner name is required.")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        public string? RestaurantImage { get; set; } // الصورة الحالية

        public IFormFile? RestaurantImageFile { get; set; }

    }
}
