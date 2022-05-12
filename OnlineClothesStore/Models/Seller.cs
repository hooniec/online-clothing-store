//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineClothesStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Seller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Seller()
        {
            this.SellerProducts = new HashSet<SellerProduct>();
        }
    
        public int SId { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(30, ErrorMessage = "Please do not enter more than 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        [StringLength(100, ErrorMessage = "Please do not enter more than 100 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter your phone")]
        [StringLength(20, ErrorMessage = "Please do not enter more than 20 characters")]
        [RegularExpression(@"^02[0-2,6-9](\s|-|)\d{3,4}(\s|-|)\d{3,4}$", ErrorMessage = "Invalid NZ mobile number")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [StringLength(30, ErrorMessage = "Please do not enter more than 30 characters")]
        [RegularExpression(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$", ErrorMessage = "Invalid email address")]

        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your username")]
        [StringLength(20, ErrorMessage = "Please do not enter more than 20 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SellerProduct> SellerProducts { get; set; }
    }
}
