using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static List<string> Summaries = new()
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{index}")]
        public IActionResult GetByIndex(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("������ ������ ���� ��������������� � ������ ���������� �������.");
            }
            return Ok(Summaries[index]);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            Summaries.Add(name);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int index, string name)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("������ ������ ���� ��������������� � ������ ���������� �������.");
            }
            Summaries[index] = name;
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int index)
        {
            if (index < 0 || index >= Summaries.Count)
            {
                return BadRequest("������ ������ ���� ��������������� � ������ ���������� �������.");
            }
            Summaries.RemoveAt(index);
            return Ok();
        }

        [HttpGet("findbyname")]
        public IActionResult GetCountByName(string name)
        {
            var count = Summaries.Count(s => s.Equals(name, StringComparison.OrdinalIgnoreCase));
            return Ok(count);
        }

        [HttpGet]
        public IActionResult GetAll(int? sortStrategy)
        {
            if (sortStrategy.HasValue)
            {
                if (sortStrategy.Value == 1)
                {
                    return Ok(Summaries.OrderBy(s => s).ToList());
                }
                else if (sortStrategy.Value == -1)
                {
                    return Ok(Summaries.OrderByDescending(s => s).ToList());
                }
                else
                {
                    return BadRequest("������������ �������� ��������� sortStrategy");
                }
            }
            return Ok(Summaries);
        }
    }
}
