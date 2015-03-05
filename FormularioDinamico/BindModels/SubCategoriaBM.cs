using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace FormularioDinamico.BindModels
{
    public class SubCategoriaBM
    {
        public SubCategoriaBM()
        {
            Campos = new List<CampoBM>();
        }
        public int CategoriaId { get; set; }
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "Descrição não deve ultrapassar {0} caracteres")]
        [Required(ErrorMessage = "Descrição é obrigatória")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [RegularExpression("^[a-z0-9-]+$", ErrorMessage = "Slug deve conter somente letras e números separados por '-'")]
        [StringLength(60, ErrorMessage = "Slug não deve ultrapassar {0} caracteres")]
        [Required(ErrorMessage = "Slug é obrigatória")]
        [Display(Name = "Slug")]
        public string Slug { get; set; }

        public virtual ICollection<CampoBM> Campos { get; set; }
    }
}