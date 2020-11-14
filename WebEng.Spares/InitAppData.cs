using IBM.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using WebEng.ReplacementParts.Data;
using WebEng.ReplacementParts.Models;

namespace WebEng.ReplacementParts
{
    internal class InitAppData
    {
        private ApplicationDbContext _context;

        public InitAppData()
        {
            IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseNpgsql(config.GetConnectionString("postgres"));
            _context = new ApplicationDbContext(optionsBuilder.Options);
            _context.Database.EnsureCreated();
        }

        public async Task Init()
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Data/";
            try
            {
                string[][] Brands = ReadCSV(path + "BRANDS.csv");
                for (int i = 0; i < Brands.Length; i++)
                {
                    Brand newBrand = new Brand
                    {
                        Name = Brands[i][0],
                        Description = Brands[i][1],
                        PictureUrl = Brands[i][2]
                    };
                    await _context.Brand.AddAsync(newBrand) ;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured:" + ex.Message);
            }

            try
            {
                string[][] Manufacturers = ReadCSV(path + "MANUFACTURERS.csv");
                for (int i = 0; i < Manufacturers.Length; i++)
                {
                    Manufacturer newManu = new Manufacturer
                    {
                        Name = Manufacturers[i][0],
                        Description = Manufacturers[i][1]
                    };
                    await _context.Manufacturer.AddAsync(newManu);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured:" + ex.Message);
            }

            try
            {
                string[][] Cars = ReadCSV(path + "CARS.csv");
                for (int i = 0; i < Cars.Length; i++)
                {
                    Brand fkBrand = await _context.Brand.FirstOrDefaultAsync(e => e.Name.Equals(Cars[i][0]));
                    Car newCar = new Car
                    {
                        BrandFK = fkBrand.Key,
                        Name = Cars[i][1],
                        Description = Cars[i][2],
                        Started = Convert.ToInt32(Cars[i][3]),
                        Finished = Convert.ToInt32(Cars[i][4]),
                        Weight = Convert.ToInt32(Cars[i][5])
                    };
                    await _context.Car.AddAsync(newCar);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured:" + ex.Message);
            }

            try
            {
                string[][] OEM = ReadCSV(path + "OEM.csv");
                for (int i = 0; i < OEM.Length; i++)
                {
                    OEM newOEM = new OEM
                    {
                        OEMNumber = OEM[i][0],
                        Name = OEM[i][1],
                        Description = OEM[i][2],
                        Weight = Convert.ToDouble(OEM[i][3])
                    };
                    await _context.OEM.AddAsync(newOEM);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured:" + ex.Message);
            }

            try
            {
                string[][] Spares = ReadCSV(path + "SPARES.csv");
                for (int i = 0; i < Spares.Length; i++)
                {
                    Manufacturer man = await _context.Manufacturer.FirstOrDefaultAsync(e => e.Name.Equals(Spares[i][1]));
                    OEM oem = await _context.OEM.FirstOrDefaultAsync(e => e.OEMNumber.Equals(Spares[i][0]));
                    SparePart newSpare = new SparePart
                    {
                        OEMKey = oem.OEMNumber,
                        ManufacturerKey = man.Key,
                        Name = Spares[i][2],
                        Description = Spares[i][3],
                        Price = Convert.ToDouble(Spares[i][4]),
                        Weight = Convert.ToDouble(Spares[i][5]),
                        Available = Convert.ToInt32(Spares[i][6])
                    };
                    await _context.ReplacementPart.AddAsync(newSpare);
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