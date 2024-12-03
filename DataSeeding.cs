using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Vidly.Areas.Identity.Data;
using Vidly.Data;
using Microsoft.Extensions.Options;

internal class DataSeeding
{
    private DbContext _context;
    public DataSeeding(DbContext context)
    {
        _context = context;
    }

    internal void SeedRoles(IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            var identityRole = _context.Set<IdentityRole>().FirstOrDefault(r => r.Name.Equals(role));
            if (identityRole == null)
            {
                _context.Set<IdentityRole>().Add(new IdentityRole(role));
            }
        }
        _context.SaveChanges();
    }

    internal void SeedUsers(IEnumerable<string> emails)
    {
        foreach (var email in emails)
        {
            var VidlyUser = _context.Set<VidlyUser>().FirstOrDefault(u => u.NormalizedUserName.Equals(email.ToUpper()));
            if (VidlyUser == null)
            {
                var user = new VidlyUser
                {
                    UserName = email,
                    NormalizedUserName = email.ToUpper(),
                    Email = email,
                    NormalizedEmail = email.ToUpper(),
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                };

                var hashedPassword = new PasswordHasher<VidlyUser>().HashPassword(user, email.Split("@")[0]);
                user.PasswordHash = hashedPassword;

                _context.Set<VidlyUser>().Add(user);
            }
        }
        _context.SaveChanges();
    }

    internal void SeedUserRoles()
    {
        var identityRole = _context.Set<IdentityRole>().FirstOrDefault(r => r.Name.Equals("Administrator"));
        if (identityRole == null)
            throw new NullReferenceException("The role 'Administrator' has not been found.");

        var identityUser = _context.Set<VidlyUser>().FirstOrDefault(u => u.UserName.Equals("admin@vidly.com"));
        if (identityUser == null)
            throw new NullReferenceException("The user 'admin' has not been found.");

        var identityUserRole = _context.Set<IdentityUserRole<string>>().FirstOrDefault(x => x.UserId == identityUser.Id);
        if (identityUserRole == null)
        {
            _context.Set<IdentityUserRole<string>>().Add(new IdentityUserRole<string> { UserId = identityUser.Id, RoleId = identityRole.Id });
            _context.SaveChanges();
        }
    }
}

