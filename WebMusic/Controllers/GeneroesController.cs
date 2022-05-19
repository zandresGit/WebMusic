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
                  .Include(b => b.Bandas)
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
                .Include(g => g.Bandas)
                .ThenInclude(b => b.Albums)
                .ThenInclude(a => a.Cancions)
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
                .Include(g => g.Bandas)
                .ThenInclude(b => b.Albums)
                .ThenInclude(a => a.Cancions)
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




        //-----------------------------BANDAS----------------------------------------------------------------------
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
                    .Include(g => g.Bandas)
                    .FirstOrDefaultAsync(g => g.Id == banda.IdGenero);

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
                    return RedirectToAction(nameof(Details), new{ Id = genero.Id
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
            .Include(b => b.Albums)
            .ThenInclude(a => a.Cancions)
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

        public async Task<IActionResult> DetailsBanda(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Banda banda = await _context.Bandas
            .Include(b => b.Albums)
            .ThenInclude(a => a.Cancions)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (banda == null)
            {
                return NotFound();
            }
            Genero genero = await _context.Generos.FirstOrDefaultAsync(g =>
            g.Bandas.FirstOrDefault(b => b.Id == banda.Id) != null);
            banda.IdGenero = genero.Id;
            return View(banda);
        }

        //-----------------------------ALBUMES-----------------------------------

        public async Task<IActionResult> AddAlbum(int? id)
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
            Album model = new Album { IdBanda = banda.Id };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAlbum(Album album)
        {
            if (ModelState.IsValid)
            {
                Banda banda = await _context.Bandas
                .Include(b => b.Albums)
                .FirstOrDefaultAsync(a => a.Id == album.IdBanda);
                if (banda == null)
                {
                    return NotFound();
                }
                try
                {
                    album.Id = 0;
                    banda.Albums.Add(album);
                    _context.Update(banda);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsBanda), new { Id = banda.Id });
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
            return View(album);
        }

        public async Task<IActionResult> EditAlbum(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Album album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            Banda banda = await _context.Bandas.FirstOrDefaultAsync(b =>
            b.Albums.FirstOrDefault(a => a.Id == album.Id) != null);
            album.IdBanda = banda.Id;
            return View(album);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAlbum(Album album)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsBanda), new { Id = album.IdBanda });
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
            return View(album);
        }

        public async Task<IActionResult> DeleteAlbum(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Album album = await _context.Albums
             .Include(a => a.Cancions)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }
            Banda banda = await _context.Bandas.FirstOrDefaultAsync(b
            => b.Albums.FirstOrDefault(a => a.Id == album.Id) != null);
            _context.Albums.Remove(album);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DetailsBanda), new
            {
                Id = banda.Id
            });
        }

        public async Task<IActionResult> DetailsAlbum(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Album album = await _context.Albums
            .Include(a => a.Cancions)
            .FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }
            Banda banda = await _context.Bandas.FirstOrDefaultAsync(b =>
            b.Albums.FirstOrDefault(a => a.Id == album.Id) != null);
            album.IdBanda = banda.Id;
            return View(album);
        }

        //-----------------------CANCIONES---------------------------------

        public async Task<IActionResult> AddCancion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Album album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            Cancion model = new Cancion { IdAlbum = album.Id };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCancion(Cancion cancion)
        {
            if (ModelState.IsValid)
            {
                Album album = await _context.Albums
                .Include(a => a.Cancions)
                .FirstOrDefaultAsync(c => c.Id == cancion.IdAlbum);
                if (album == null)
                {
                    return NotFound();
                }
                try
                {
                    cancion.Id = 0;
                    album.Cancions.Add(cancion);
                    _context.Update(album);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsAlbum), new { Id = album.Id });
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
            return View(cancion);
        }

        public async Task<IActionResult> EditCancion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cancion cancion = await _context.Cancions.FindAsync(id);
            if (cancion == null)
            {
                return NotFound();
            }
            Album album = await _context.Albums.FirstOrDefaultAsync(a =>
            a.Cancions.FirstOrDefault(c => c.Id == cancion.Id) != null);
            cancion.IdAlbum = album.Id;
            return View(cancion);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCancion(Cancion cancion)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cancion);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(DetailsAlbum), new { Id = cancion.IdAlbum });
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
            return View(cancion);
        }

        public async Task<IActionResult> DeleteCancion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Cancion cancion = await _context.Cancions
            .FirstOrDefaultAsync(m => m.Id == id);
            if (cancion == null)
            {
                return NotFound();
            }
            Album album = await _context.Albums.FirstOrDefaultAsync(a
            => a.Cancions.FirstOrDefault(c => c.Id == cancion.Id) != null);
            _context.Cancions.Remove(cancion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DetailsAlbum), new
            {
                Id = album.Id
            });
        }

    }
}
