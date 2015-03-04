using FormularioDinamico.Domain;
using System.Threading.Tasks;

namespace FormularioDinamico.Application
{
    public interface IAtualizarSubCategoria
    {
        Task<Notification> Executar(SubCategoria entity);
    }
}
