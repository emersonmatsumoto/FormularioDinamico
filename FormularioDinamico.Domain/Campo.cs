using System.Collections.Generic;
namespace FormularioDinamico.Domain
{
    public class Campo
    {
        public int SubCategoriaId { get; set; }
        public int Id { get; set; }
        public int Ordem { get; set; }
        public string Descricao { get; set; }
        public TipoCampo Tipo { get; set; }
        public ICollection<string> Lista { get; set; }
    }
}
