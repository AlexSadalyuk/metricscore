using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MetricsCore.Interfaces;
using MetricsCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMetricsService _metricService;
        public MetricsController(IMetricsService metricsService)
        {
            _metricService = metricsService;
        }

        [HttpGet]
        public IActionResult Get(string text)
        {
            IEnumerable<MetricsEntryResult> results = _metricService.RunMetricsCheck(text);
            return Ok(results);
        }

        [HttpPost] 
        public IActionResult Post(IFormFile file)
        {
            IEnumerable<MetricsEntryResult> results = _metricService.RunMetricsCheck(file.OpenReadStream());
            return Ok(results);
        }
    }
}
