using FormularioDinamico.Domain;
using System.Threading.Tasks;

namespace FormularioDinamico.Application
{
    public interface IInserirCategoria
    {
        Task<Notification> Executar(Categoria entity);
    }
}
