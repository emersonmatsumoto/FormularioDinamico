using FormularioDinamico.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormularioDinamico.BindModels
{
    public class CampoBM
    {
        public virtual SubCategoriaBM SubCategoria { get; set; }
        public int SubCategoriaId { get; set; }
        public int Id { get; set; }
        public int Ordem { get; set; }

        [StringLength(60, ErrorMessage = "Descrição não deve ultrapassar {0} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }
        public TipoCampo Tipo { get; set; }
        public ICollection<ItemBM> Lista { get; set; }
    }
}