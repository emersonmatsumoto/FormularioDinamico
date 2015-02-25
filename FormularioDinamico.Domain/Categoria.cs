using System.Collections.Generic;
namespace FormularioDinamico.Domain
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Slug { get; set; }
        public virtual ICollection<SubCategoria> SubCategorias { get; set; }
    }
}
