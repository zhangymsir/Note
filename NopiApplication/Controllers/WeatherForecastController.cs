using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NpoiApplication.Dto.Test;
using NpoiApplication.Service.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NpoiApplication.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IExcelHelper _excelHelper;
        private readonly IWordHelper _wordHelper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IExcelHelper excelHelper, IWordHelper wordHelper)
        {
            _logger = logger;
            _excelHelper = excelHelper;
            _wordHelper = wordHelper;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            await _wordHelper.SaveWord();
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpPost]
        public IActionResult UpLoadFile(IFormFile file)
        {
            var msg = "未知错误";

            if (file.Length > 0)
            {
                var dataList = _excelHelper.ExcelToList<TestDto>(file.OpenReadStream(), Path.GetExtension(file.FileName), out string strMsg);
                return Ok(new { code = 0, msg = "导入成功", data = dataList });
            }
            return Ok(new { code = 0, msg });
        }

        [HttpPost]
        public IActionResult ExportExcel()
        {
            //var rng = new Random();
            //var data = Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
            var data = new List<TestDto> {
                new TestDto { BaseEmployee = "张三", Code = "编码1", Contractor = "服务商1", EndTime = DateTime.Now, StartTime = DateTime.Now },
                new TestDto { BaseEmployee = "李四", Code = "编码2", Contractor = "服务商2", EndTime = DateTime.Now, StartTime = DateTime.Now, Count = 1 },
                new TestDto { BaseEmployee = "王五", Code = "编码3", Contractor = "服务商3", EndTime = DateTime.Now, StartTime = DateTime.Now, Count = 2 },
                new TestDto { BaseEmployee = "马六", Code = "编码4", Contractor = "服务商4", EndTime = DateTime.Now, StartTime = DateTime.Now, Count = 3 }
            };

            _excelHelper.SaveExcel("test.xlsx", data, "sheet1");
            return File(System.IO.File.OpenRead("test.xlsx"), "application/octet-stream", "test.xlsx");
        }
    }
}
