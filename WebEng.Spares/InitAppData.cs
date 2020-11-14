using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebEng.ReplacementParts.Data;

namespace WebEng.ReplacementParts
{
    internal class InitAppData
    {
        private ApplicationDbContext _context;

        public InitAppData()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            _context = new ApplicationDbContext(optionsBuilder.Options);
        }

        public async Task Init()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Data/Data/";
            try
            {
                string[][] Brands = ReadCSV(path + "Brand.csv");
                for (int i = 0; i < Brands.Length; i++)
                {
                    await _context.Brand.AddAsync(new Brand { Name = Brands[i][0] });
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured:" + ex.Message);
            }
        }

        private static string[][] ReadCSV(string filepath)
        {
            if (File.Exists(filepath))
            {

                string[] lines = File.ReadAllLines(filepath, Encoding.Default);

                string[][] result = new string[lines.Length][];

                //Split all lines with a ','
                for (int i = 0; i < lines.Length; i++)
                {
                    result[i] = lines[i].Split(';');
                }
                return result;
            }
            else
            {
                throw new FileNotFoundException();
            }
        }
    }
}