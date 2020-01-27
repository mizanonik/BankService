using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BankServiceApi.Data
{
    public class PrepareDatabase
    {
        public static void PopulateDatabase(IApplicationBuilder app){
            using(var serviceScope = app.ApplicationServices.CreateScope()){
                var context = serviceScope.ServiceProvider.GetService<BankServiceContext>();
                context.Database.Migrate();
            }
        }
    }
}