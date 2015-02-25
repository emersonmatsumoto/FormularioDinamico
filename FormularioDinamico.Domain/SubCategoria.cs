using System.Collections.Generic;
namespace FormularioDinamico.Domain
{
    public class SubCategoria
    {
        public int CategoriaId { get; set; }
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Slug { get; set; }
        public virtual ICollection<Campo> Campos { get; set; }
    }
}
