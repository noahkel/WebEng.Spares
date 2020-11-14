using System;
using System.Threading.Tasks;
using WebEng.ReplacementParts.Data;

namespace WebEng.ReplacementParts
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context)
        {
            bool existsNot = true;
            try
            {
                existsNot = context.Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                InitAppData initializer = new InitAppData();
                await initializer.Init();
        }
    }
}