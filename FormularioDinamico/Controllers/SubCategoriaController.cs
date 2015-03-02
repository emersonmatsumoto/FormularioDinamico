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

namespace FormularioDinamico.Controllers
{
    public class SubCategoriaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: SubCategoria/Details/5
        public async Task<ActionResult> Details(int? id)
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
        public async Task<ActionResult> Create(SubCategoriaBM subCategoria)
        {
            if (ModelState.IsValid)
            {
                //db.SubCategorias.Add(subCategoria);
                //await db.SaveChangesAsync();
                return RedirectToAction("Index", "Categoria", null);
            }

            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Descricao", subCategoria.CategoriaId);
            return View(subCategoria);
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
            return View(subCategoria);
        }

        // POST: SubCategoria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CategoriaId,Descricao,Slug")] SubCategoria subCategoria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subCategoria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index", "Categoria", null);
            }
            ViewBag.CategoriaId = new SelectList(db.Categorias, "Id", "Descricao", subCategoria.CategoriaId);
            return View(subCategoria);
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
            db.SubCategorias.Remove(subCategoria);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Categoria", null);
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
