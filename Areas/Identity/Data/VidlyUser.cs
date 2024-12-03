using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Vidly.Areas.Identity.Data;

// Add profile data for application users by adding properties to the VidlyUser class
public class VidlyUser : IdentityUser
{
    [Required]
    [StringLength(255)]
    public string DrivingLicense { get; set; }
    [Required]
    [StringLength(50)]
    public string Phone { get; set; }
}

