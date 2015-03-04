using FormularioDinamico.Domain;
using System.Threading.Tasks;

namespace FormularioDinamico.Application
{
    public interface IRemoverCategoria
    {
        Task<Notification> Executar(int categoriaId);
    }
}
