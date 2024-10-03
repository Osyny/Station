using AutoMapper;

using Station.Web.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Station.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMapper _mapper;
       /// private readonly CavellDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
           // _dbContext = dbContext;
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return Redirect("/swagger");
        }
    }
}
