using System.Security.Claims;
using IdentityModel;
using V2Layground.Identity.Data;
using V2Layground.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace V2Layground.Identity;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var vuslat = userMgr.FindByNameAsync("vuslat.ozel").Result;
            if (vuslat == null)
            {
                vuslat = new ApplicationUser
                {
                    UserName = "vuslat.ozel",
                    Email = "ozel.vuslat@gmail.com",
                    EmailConfirmed = true,
                    Birthday = new DateTime(2018, 5, 4) // aynen LOL
                };
                var result = userMgr.CreateAsync(vuslat, "Pass123+").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(vuslat, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Vuslat Özel"),
                            new Claim(JwtClaimTypes.GivenName, "Vuslat"),
                            new Claim(JwtClaimTypes.FamilyName, "Özel"),
                            new Claim(JwtClaimTypes.WebSite, "http://vslzl.com"),
                            new Claim(JwtClaimTypes.BirthDate, vuslat.Birthday?.ToString("D")),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("vuslat projected");
            }
            else
            {
                Log.Debug("vuslat already exists");
            }

            var kemal = userMgr.FindByNameAsync("kemal.dogan").Result;
            if (kemal == null)
            {
                kemal = new ApplicationUser
                {
                    UserName = "kemal.dogan",
                    Email = "kemal@bucocuktaisvar.net",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(kemal, "Pass123+").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(kemal, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Kemal Doğan"),
                            new Claim(JwtClaimTypes.GivenName, "Kemal"),
                            new Claim(JwtClaimTypes.FamilyName, "Doğan"),
                            new Claim(JwtClaimTypes.WebSite, "http://facebook.com/benkemalamadogandegil"),
                            new Claim("location", "TR")
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("kemal created");
            }
            else
            {
                Log.Debug("kemal already exists");
            }
        }
    }
}
