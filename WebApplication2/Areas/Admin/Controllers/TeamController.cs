using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.DAL;
using WebApplication2.Models;

namespace WebApplication2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext context;

        public TeamController(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View(await context.teams.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Team teams)
        {
            if (!ModelState.IsValid)
                return View();
            if (teams == null)
                return NotFound();
            await context.teams.AddAsync(teams);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            return View(await context.teams.FirstOrDefaultAsync(x => x.Id==Id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Team teams)
        {
            if(!ModelState.IsValid)
            return View();
            if (teams == null)
            return NotFound();
            Team? exist=await context.teams.FirstOrDefaultAsync(x=>x.Id==teams.Id);
            exist.Name=teams.Name;
            exist.Description = teams.Description;
            exist.Profession=teams.Profession;
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            Team exist = await context.teams.FirstOrDefaultAsync(x => x.Id == id);
            if (exist == null)
                return NotFound();
            context.teams.Remove(exist);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
    }
    }
