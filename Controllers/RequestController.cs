using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ip_request.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RequestController: ControllerBase
    {
        private static readonly Dictionary<string, int> requests = new Dictionary<string, int>();

        private readonly ILogger<RequestController> _logger;

        public RequestController(ILogger<RequestController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public void requesHandle(string ip)
        {
            requests.Add(ip, requests[ip] + 1);
        }

        [HttpGet]
        public List top100([FromQuery] RequestParameter parameters)
        {
            return requests.OrderBy(r => r.value)
                .Skip( (parameters.PageNumber-1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToList();
        }

        [HttpPost("clear")]
        public void clear()
        {
            requests = new Dictionary<string, int>();
        }
    }
}
