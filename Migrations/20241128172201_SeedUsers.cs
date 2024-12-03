using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vidly.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                @"
                INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7f682ce2-e9df-47db-86b7-303df548eb81', N'admin@vidly.com', N'ADMIN@VIDLY.COM', N'admin@vidly.com', N'ADMIN@VIDLY.COM', 1, N'AQAAAAIAAYagAAAAEKUCkFQCAWmCaJaxnYEvjZ42uHBbbOgBWC2z6w5hgNzeRE8H4XVNMxZ1OX3kh4zFlA==', N'a8af4827-6628-490b-91a2-c0c94094a0c6', N'a2ad42a4-ac21-47a7-bd1a-8524f0be8b14', NULL, 0, 0, NULL, 1, 0)
                INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'fd0c5bfe-c903-41b3-be2a-103416cef96c', N'user@vidly.com', N'USER@VIDLY.COM', N'user@vidly.com', N'USER@VIDLY.COM', 1, N'AQAAAAIAAYagAAAAEKs+2DlppsERAXFgcfPvKV2zmFQq+SA488G86DdXqHvmwoRWhXw6GiUDNcFm+NRKvg==', N'63a5064f-4855-47ff-854d-93416b9e1027', N'810f4915-5fc3-452e-8a62-c810b5514c95', NULL, 0, 0, NULL, 1, 0)
                
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'a4af72f5-a772-497f-9124-1f9d7d01c927', N'Administrator', NULL, NULL)

                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'7f682ce2-e9df-47db-86b7-303df548eb81', N'a4af72f5-a772-497f-9124-1f9d7d01c927')
                ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
