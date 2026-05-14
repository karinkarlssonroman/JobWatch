using JobWatch.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;

namespace JobWatch.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        var config = services.GetRequiredService<IConfiguration>();

        var sysAdminPassword = config["AdminSeed:Password"];

        if (string.IsNullOrWhiteSpace(sysAdminPassword))
        {
            throw new Exception("AdminSeed:Password saknas.");
        }

        // Seed roles
        string[] roles = ["Admin", "Candidate", "SystemAdministrator"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed admin
        await SeedUserAsync(userManager, "sysadmin", sysAdminPassword, "SystemAdministrator", new Claim("SystemAdministration", "true"));

        var env = services.GetRequiredService<IWebHostEnvironment>();

        if (env.IsDevelopment())
        {
            await SeedUserAsync(userManager, "candidate", "candidate", "Candidate",
                new Claim("Department", "Sales"));
            await SeedUserAsync(userManager, "engineeringmanager", "engineeringmanager", "Admin",
                new Claim("Department", "Engineering"));
            await SeedUserAsync(userManager, "salesmanager", "salesmanager", "Admin",
                new Claim("Department", "Sales"));
        }       
    }

    private static async Task SeedUserAsync(
        UserManager<ApplicationUser> userManager,
        string username, string password, string role, Claim customClaim)
    {
        if (await userManager.FindByNameAsync(username) is not null)
            return;

        var user = new ApplicationUser { UserName = username };
        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            throw new Exception($"Failed to seed user '{username}': {string.Join(", ", result.Errors.Select(e => e.Description))}");

        await userManager.AddToRoleAsync(user, role);

        var existingClaims = await userManager.GetClaimsAsync(user);
        if (!existingClaims.Any(c => c.Type == customClaim.Type))
        {
            await userManager.AddClaimAsync(user, customClaim);
        }
    }
}
