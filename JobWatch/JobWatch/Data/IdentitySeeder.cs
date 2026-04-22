using JobWatch.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace JobWatch.Data;

public static class IdentitySeeder
{
    public static async Task SeedAsync(IServiceProvider services)
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        // Seed roles
        string[] roles = ["Admin", "Candidate"];
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        // Seed users
        await SeedUserAsync(userManager, "admin", "admin", "Admin",
            new Claim("Department", "Engineering"));
        await SeedUserAsync(userManager, "candidate", "candidate", "Candidate",
            new Claim("Department", "Sales"));
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
