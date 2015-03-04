using FormularioDinamico.Domain;
using System.Threading.Tasks;

namespace FormularioDinamico.Application
{
    public interface IAtualizarCategoria
    {
        Task<Notification> Executar(Categoria entity);
    }
}
