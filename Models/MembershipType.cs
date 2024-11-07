using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Runtime.InteropServices.Marshalling;

namespace Vidly.Models
{
    public class MembershipType
    {
        public byte Id { get; set; }
        [Required]
        [StringLength(100)] 
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

        public static readonly byte Unkonwn = 0;
        public static readonly byte PayAsYouGo = 1;
    }
}
