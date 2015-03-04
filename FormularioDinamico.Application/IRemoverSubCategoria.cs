using FormularioDinamico.Domain;
using System.Threading.Tasks;

namespace FormularioDinamico.Application
{
    public interface IRemoverSubCategoria
    {
        Task<Notification> Executar(int categoriaId);
    }
}
