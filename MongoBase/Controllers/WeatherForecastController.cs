using Microsoft.AspNetCore.Mvc;
using MongoBase.Configuration;
using MongoBase.Entities;

using MongoDB.Driver;

namespace MongoBase.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        protected readonly IMongoDatabaseContext _context;
        protected IMongoCollection<Customer> _dbSet;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMongoDatabaseContext context)
        {
            _logger = logger;
            _context= context;
            _dbSet = _context.Set<Customer>(typeof(Customer).Name);

        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("/Customer")]
        public async Task<IEnumerable<Customer>> GetCustomerAsync()
        {

            var all = await _dbSet.FindAsync(Builders<Customer>.Filter.Empty);
            return await all.ToListAsync();

        }

        [HttpPost("/Customer")]
        public ActionResult<Customer> addCustomer(Customer customerDto)
        {

            _dbSet.InsertOne(customerDto);
            return customerDto;



        }
    }
}