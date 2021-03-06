﻿using System;
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
using System.Web.Script.Serialization;
using AutoMapper;
using FormularioDinamico.Application;

namespace FormularioDinamico.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SubCategoriaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private IInserirSubCategoria _inserirSubCategoria;
        private IAtualizarSubCategoria _atualizarSubCategoria;
        private IRemoverSubCategoria _removerSubCategoria;

        public SubCategoriaController()
        {

        }

        public SubCategoriaController(IInserirSubCategoria inserirSubCategoria, IAtualizarSubCategoria atualizarSubCategoria, IRemoverSubCategoria removerSubCategoria)
        {
            _inserirSubCategoria = inserirSubCategoria;
            _atualizarSubCategoria = atualizarSubCategoria;
            _removerSubCategoria = removerSubCategoria;
        }


        // GET: SubCategoria/Details/5
        [AllowAnonymous]
        public async Task<ActionResult> Details(string slug, string subslug)
        {
            SubCategoria subCategoria = await db.SubCategorias.FirstOrDefaultAsync(f => f.Slug == subslug && f.Categoria.Slug == slug);
            if (subCategoria == null)
            {
                return HttpNotFound();
            }
            return View(subCategoria);
        }

        // GET: SubCategoria/Create
        public ActionResult Create(int categoriaId)
        {
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Descricao", categoriaId);
            var model = new SubCategoriaBM() { 
                Campos = new List<CampoBM>()
            };
            return View(model);
        }

        // POST: SubCategoria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(SubCategoriaBM subCategoriaBM)
        {
            if (ModelState.IsValid)
            {
                SubCategoria subCategoria = Mapper.Map<SubCategoria>(subCategoriaBM);

                Notification notification = await _inserirSubCategoria.Executar(subCategoria);

                if (notification.HasErrors == false)
                {
                    return RedirectToAction("Index", "Categoria", null);
                }

                ModelState.AddModelError("", String.Join(", ", notification.Errors));
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Descricao", subCategoriaBM.CategoriaId);
            return View(subCategoriaBM);
        }

        // GET: SubCategoria/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoria subCategoria = await db.SubCategorias.FindAsync(id);
            if (subCategoria == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Descricao", subCategoria.CategoriaId);
            var subCategoriaBM = Mapper.Map<SubCategoriaBM>(subCategoria);
            return View(subCategoriaBM);
        }

        // POST: SubCategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SubCategoriaBM subCategoriaBM)
        {
            if (ModelState.IsValid)
            {
                SubCategoria subCategoria = Mapper.Map<SubCategoria>(subCategoriaBM);

                Notification notification = await _atualizarSubCategoria.Executar(subCategoria);

                if (notification.HasErrors == false)
                {
                    return RedirectToAction("Index", "Categoria", null);
                }

                ModelState.AddModelError("", String.Join(", ", notification.Errors));
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Descricao", subCategoriaBM.CategoriaId);
            return View(subCategoriaBM);
        }

        // GET: SubCategoria/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoria subCategoria = await db.SubCategorias.FindAsync(id);
            if (subCategoria == null)
            {
                return HttpNotFound();
            }
            return View(subCategoria);
        }

        // POST: SubCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            SubCategoria subCategoria = await db.SubCategorias.FindAsync(id);
            if (subCategoria == null)
            {
                return HttpNotFound();
            }

            Notification notification = await _removerSubCategoria.Executar(id);
            if (notification.HasErrors == false)
            {
                return RedirectToAction("Index", "Categoria", null);
            }
            else
            {
                ViewBag.ErrorMessage = String.Join(", ", notification.Errors);
                var subCategoriaBM = Mapper.Map<SubCategoriaBM>(subCategoria);
                return View(subCategoriaBM);
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
