using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FormularioDinamico.Domain;
using FormularioDinamico.Models;
using FormularioDinamico.BindModels;
using AutoMapper;
using FormularioDinamico.Application;

namespace FormularioDinamico.Controllers
{
    [Authorize(Roles="Admin")]
    public class CategoriaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IInserirCategoria _inserirCategoria;
        private IAtualizarCategoria _atualizarCategoria;
        private IRemoverCategoria _removerCategoria;

        public CategoriaController()
        {

        }

        public CategoriaController(IInserirCategoria inserirCategoria, IAtualizarCategoria atualizarCategoria, IRemoverCategoria removerCategoria)
        {
            _inserirCategoria = inserirCategoria;
            _atualizarCategoria = atualizarCategoria;
            _removerCategoria = removerCategoria;
        }

        // GET: Categoria
        public async Task<ActionResult> Index()
        {
            return View(await db.Categorias.ToListAsync());
        }

        // GET: Categoria/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(string slug)
        {          
            Categoria categoria = await db.Categorias.FirstOrDefaultAsync(f => f.Slug == slug);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            return View(categoria);
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoriaBM categoriaBM)
        {
            if (ModelState.IsValid)
            {
                var categoria = Mapper.Map<Categoria>(categoriaBM);

                Notification notification = await _inserirCategoria.Executar(categoria);

                if (notification.HasErrors == false)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", String.Join(", ", notification.Errors));
            }

            return View(categoriaBM);
        }

        // GET: Categoria/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }

            var categoriaBM = Mapper.Map<CategoriaBM>(categoria);
            return View(categoriaBM);
        }

        // POST: Categoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoriaBM categoriaBM)
        {
            if (ModelState.IsValid)
            {
                var categoria = Mapper.Map<Categoria>(categoriaBM);

                Notification notification = await _atualizarCategoria.Executar(categoria);

                if (notification.HasErrors == false)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", String.Join(", ", notification.Errors));
            }
            return View(categoriaBM);
        }

        // GET: Categoria/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }
            var categoriaBM = Mapper.Map<CategoriaBM>(categoria);
            return View(categoriaBM);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categoria categoria = await db.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return HttpNotFound();
            }

            Notification notification = await _removerCategoria.Executar(id);
            if (notification.HasErrors == false)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = String.Join(", ", notification.Errors);
                var categoriaBM = Mapper.Map<CategoriaBM>(categoria);
                return View(categoriaBM);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
