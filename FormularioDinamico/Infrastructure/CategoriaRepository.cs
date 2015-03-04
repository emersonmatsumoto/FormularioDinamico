using FormularioDinamico.Domain;
using FormularioDinamico.Domain.Repositories;
using FormularioDinamico.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormularioDinamico.Infrastructure
{
    public class CategoriaRepository : Repository<ApplicationDbContext, Categoria>, ICategoriaRepository
    {
        public Categoria GetSingle(int categoriaId)
        {
            var query = GetAll().FirstOrDefault(x => x.Id == categoriaId);
            return query;
        }
    }
}