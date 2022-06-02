using System.Linq;
using System.Threading.Tasks;
using EShop.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EShop.Web.Models
{
    public static class DataBaseConfiguration
    {
        public static void PopulateMigrate(EShopContext context)
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }
    }
}
