using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReactNetCoreBase.Auth
{
    public class CalcAllowedPermissions : IDisposable
    {
        /// <summary>
        /// NOTE: This class is used in OnValidatePrincipal so it can't use DI, so I can't inject the DbContext here because that is dynamic.
        /// Therefore I can pass in the database options because that is a singleton
        /// From that the method can create a valid dbContext to access the database
        /// </summary>
        private readonly DbContextOptions<ExtraAuthorizeDbContext> _extraAuthDbContextOptions;

        private ExtraAuthorizeDbContext _context;

        public CalcAllowedPermissions(ExtraAuthorizeDbContext context)
        {
            _context = context;
        }

        public CalcAllowedPermissions(DbContextOptions<ExtraAuthorizeDbContext> extraAuthDbContextOptions)
        {
            _extraAuthDbContextOptions = extraAuthDbContextOptions;
        }

        /// <summary>
        /// This is called if the Permissions that a user needs calculating.
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public async Task<string> CalcPermissionsForUser(IEnumerable<Claim> claims)
        {
            var usersRoles = claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value)
                .ToList();

            var dbContext = GetContext();
            //This gets all the permissions, with a distinct to remove duplicates
            var permissionsForUser = await dbContext.RolesToPermissions.Where(x => usersRoles.Contains(x.RoleName))
                .SelectMany(x => x.PermissionsInRole)
                .Distinct()
                .ToListAsync();
            
            return permissionsForUser.PackPermissionsIntoString();
        }

        private ExtraAuthorizeDbContext GetContext()
        {
            return _context ?? new ExtraAuthorizeDbContext(_extraAuthDbContextOptions);
        }

        #region IDisposable Support

        protected virtual void Dispose(bool disposing)
        {
            if (_extraAuthDbContextOptions != null && _context != null)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                _context = null;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~CalcAllowedPermissions() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}