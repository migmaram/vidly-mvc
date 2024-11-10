using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.DTOs
{
    public class CustomerDTO 
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSuscribedToNewsletter { get; set; }
        [Required]
        [Display(Name = "Membership type")]
        public byte MembershipTypeId { get; set; }
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}
