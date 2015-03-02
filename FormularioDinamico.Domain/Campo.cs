using System.Collections.Generic;
namespace FormularioDinamico.Domain
{
    public class Campo
    {
        public virtual SubCategoria SubCategoria { get; set; }
        public int SubCategoriaId { get; set; }
        public int Id { get; set; }
        public int Ordem { get; set; }
        public string Descricao { get; set; }
        public TipoCampo Tipo { get; set; }
        public string Lista { get; set; }
    }
}
