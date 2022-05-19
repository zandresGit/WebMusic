using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebMusic.Data;
using WebMusic.Models;

namespace WebMusic.Controllers
{
    public class GeneroesController : Controller
    {
        private readonly DataContext _context;

        public GeneroesController(DataContext context)
        {
            _context = context;
        }

        // GET: Generoes
        public async Task<IActionResult> Index()
        {
              return View(await _context.Generos
                  .Include(c => c.Bandas)
                  .ToListAsync());
        }

        // GET: Generoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos
                .Include(c => c.Bandas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        // GET: Generoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,des_genero")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(genero);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(genero);
        }

        // GET: Generoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Generos == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }

        // POST: Generoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,des_genero")] Genero genero)
        {
            if (id != genero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genero);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }

            return View(genero);
        }

        // GET: Generoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos
                .Include(c => c.Bandas)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genero == null)
            {
                return NotFound();
            }

            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GeneroExists(int id)
        {
          return _context.Generos.Any(e => e.Id == id);
        }




        //---------------------------------------------------------------------------------------------------
        public async Task<IActionResult> AddBanda(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Genero genero = await _context.Generos.FindAsync(id);
            if (genero == null)
            {
                return NotFound();
            }
            Banda model = new Banda { IdGenero = genero.Id };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBanda(Banda banda)
        {
            if (ModelState.IsValid)
            {
                Genero genero = await _context.Generos
                    .Include(c => c.Bandas)
                    .FirstOrDefaultAsync(c => c.Id == banda.IdGenero);

                if (genero == null)
                {
                    return NotFound();
                }

                try
                {
                    banda.Id = 0;
                    genero.Bandas.Add(banda);
                    _context.Update(genero);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new
                    {
                        Id = genero.Id
                    });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(banda);
        }


        public async Task<IActionResult> EditBanda(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Banda banda = await _context.Bandas.FindAsync(id);
            if (banda == null)
            {
                return NotFound();
            }
            Genero genero = await _context.Generos.FirstOrDefaultAsync(g =>
            g.Bandas.FirstOrDefault(b => b.Id == banda.Id) != null);
            banda.IdGenero = genero.Id;
            return View(banda);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBanda(Banda banda)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(banda);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details), new
                    {
                        Id = banda.IdGenero
                    });
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "There are a record with the same name.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                        dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {

                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(banda);
        }

        public async Task<IActionResult> DeleteBanda(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Banda banda = await _context.Bandas
            .FirstOrDefaultAsync(m => m.Id == id);
            if (banda == null)
            {
                return NotFound();
            }
            Genero genero = await _context.Generos.FirstOrDefaultAsync(g =>
            g.Bandas.FirstOrDefault(b => b.Id == banda.Id) != null);
            _context.Bandas.Remove(banda);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Details), new { Id = genero.Id });
        }

    }
}
