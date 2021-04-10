using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalCoreAsp.Context;
using FinalCoreAsp.Models;
using AutoMapper;
using FinalCoreAsp.Entities;

namespace FinalCoreAsp.Controllers
{
    public class AutorController : Controller
    {
        private readonly ApplicationContext _context;

        public readonly IMapper _Mapper;

        public AutorController(ApplicationContext context, IMapper mapper)
        {
            _context = context;
            _Mapper = mapper;
        }

        // GET: Autor
        public async Task<ActionResult<AutorDTO>> Index()
        {
            var autor = await _context.Autores.Include(x => x.Libros).ToListAsync();
            var autotDTO = _Mapper.Map<List<AutorDTO>>(autor);
            return View(autotDTO);
        }

        // GET: Autor/Details/5
        public async Task<ActionResult<AutorDTO>> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores.Include(x => x.Libros).FirstOrDefaultAsync(m => m.Id == id);
            var autorDTO = _Mapper.Map<AutorDTO>(autor);
            
            if (autorDTO == null)
            {
                return NotFound();
            }

            return View(autorDTO);
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<AutorDTO>> Create([Bind("Id,Name,Description,Image")] AutorDTO autorDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(autorDTO);
            }
            //_context.Add(autorDTO);
            //await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            //return View(autorDTO);

            var autor = _Mapper.Map<Autor>(autorDTO);
            await _context.Autores.AddAsync(autor);
            await _context.SaveChangesAsync();
            var AutorDTO = _Mapper.Map<AutorDTO>(autor);
            return View(AutorDTO);
        }

        // GET: Autor/Edit/5
        public async Task<ActionResult<AutorDTO>> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores.FindAsync(id);
            var autorDTO = _Mapper.Map<AutorDTO>(autor);
            if (autorDTO == null)
            {
                return NotFound();
            }
            return View(autorDTO);
        }

        // POST: Autor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, [Bind("Id,Name,Description,Image")] AutorDTO autorDTO)
        {
            if (id != autorDTO.Id)
            {
                return NotFound();
            }


            var autor = _Mapper.Map<Autor>(autorDTO);
            autor.Id = id;
         

             _context.Entry(autor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Autor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

             var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }

            _context.Remove(autor);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autorDTO = await _context.AutorDTO.FindAsync(id);
            _context.AutorDTO.Remove(autorDTO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutorDTOExists(int id)
        {
            return _context.AutorDTO.Any(e => e.Id == id);
        }
    }
}
