using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForeSpark.EntityFrameworkCore.Seed.Cities
{
    public class CitiesBuilder
    {
        private readonly ForeSparkDbContext _context;

        public CitiesBuilder(ForeSparkDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateDefaultCities();
        }

        private void CreateDefaultCities()
        {
            if (_context.Cities.Count() == 0)
            {
                _context.Cities.Add(new ForeSpark.Cities.Cities("Islamabad", 33.6845867, 73.0304453));
                _context.Cities.Add(new ForeSpark.Cities.Cities("Rawalpindi", 33.585468, 73.021904));
                _context.Cities.Add(new ForeSpark.Cities.Cities("Lahore", 31.4826352, 74.0541999));
                _context.Cities.Add(new ForeSpark.Cities.Cities("Karachi", 25.1929385, 66.87527));
                _context.Cities.Add(new ForeSpark.Cities.Cities("Peshawar", 33.9772137, 71.4253854));
                _context.Cities.Add(new ForeSpark.Cities.Cities("Multan", 30.1811818, 71.334573));
                _context.Cities.Add(new ForeSpark.Cities.Cities("Quetta", 30.1797555, 66.8786035));
                _context.SaveChanges();
            }
        }
    }
}
