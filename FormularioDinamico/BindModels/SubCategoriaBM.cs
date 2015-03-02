using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FormularioDinamico.BindModels
{
    public class SubCategoriaBM
    {
        public virtual CategoriaBM Categoria { get; set; }
        public int CategoriaId { get; set; }
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "Descrição não deve ultrapassar {0} caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [StringLength(60, ErrorMessage = "Slug não deve ultrapassar {0} caracteres")]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        public virtual ICollection<CampoBM> Campos { get; set; }
    }
}