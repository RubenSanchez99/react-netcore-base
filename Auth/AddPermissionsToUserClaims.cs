using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace ReactNetCoreBase.Auth
{
    public class AddPermissionsToUserClaims : UserClaimsPrincipalFactory<IdentityUser, IdentityRole>
    {
        private readonly ExtraAuthorizeDbContext _extraAuthDbContext;

        public AddPermissionsToUserClaims(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor, 
            ExtraAuthorizeDbContext extraAuthDbContext)
            : base(userManager, roleManager, optionsAccessor)
        {
            _extraAuthDbContext = extraAuthDbContext;
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(IdentityUser user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            var rtoPCalcer = new CalcAllowedPermissions(_extraAuthDbContext);
            identity.AddClaim(new Claim(PermissionConstants.PackedPermissionClaimType, 
                await rtoPCalcer.CalcPermissionsForUser(identity.Claims)));
            return identity;
        }    
    }
}