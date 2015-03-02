
using System.ComponentModel.DataAnnotations;
namespace FormularioDinamico.BindModels
{
    public class ItemBM
    {
        public virtual CampoBM Campo { get; set; }
        public int CampoId { get; set; }
        public int Id { get; set; }

        [StringLength(60, ErrorMessage = "Texto não deve ultrapassar {0} caracteres")]
        [Display(Name = "Texto")]
        public string Texto { get; set; }
    }
}