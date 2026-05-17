using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using hackathon_project.Data;

namespace hackathon_project.Controllers
{
    public class OrderPoolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderPoolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pools = await _context.OrderPools
                .Include(x => x.Restaurant)
                .ToListAsync();

            return View(pools);
        }
    }
}