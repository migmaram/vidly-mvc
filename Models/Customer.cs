using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSuscribedToNewsletter { get; set; }
        public MembershipType? MembershipType { get; set; }
        [Required]
        [Display(Name = "Membership type")]
        public byte MembershipTypeId { get; set; }
        [Min18YearsIfAMember]
        public DateTime? Birthdate { get; set; }
    }
}
