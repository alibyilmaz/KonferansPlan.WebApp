
using Frontend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services
{

    public class AdminService : IAdminService
    {

        private readonly IServiceProvider _serviceProvider;
        private bool _adminExists;

        public AdminService(IServiceProvider serviceProvider)
        {

           _serviceProvider = serviceProvider;
        }


        public async Task<bool> AllowAdminUserCreationAsync()
        {

            if (_adminExists)
            {
                return false;

            }
            else
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
                    if (await dbContext.Users.AnyAsync(user => user.IsAdmin))
                    {
                        _adminExists = true;
                        return false;
                    }
                    return true;
                }

            } 
        }
    }

}
