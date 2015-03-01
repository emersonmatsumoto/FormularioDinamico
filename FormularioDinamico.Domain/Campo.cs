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
        public ICollection<Item> Lista { get; set; }
    }

    public class Item
    {
        public virtual Campo Campo { get; set; }
        public int CampoId { get; set; }
        public int Id { get; set; }
        public string Texto { get; set; }
    }
}
