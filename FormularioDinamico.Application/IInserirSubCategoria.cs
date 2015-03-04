using FormularioDinamico.Domain;
using System.Threading.Tasks;

namespace FormularioDinamico.Application
{
    public interface IInserirSubCategoria
    {
        Task<Notification> Executar(SubCategoria entity);
    }
}
