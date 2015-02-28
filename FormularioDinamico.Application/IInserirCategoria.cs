using FormularioDinamico.Domain;
using System.Threading.Tasks;

namespace FormularioDinamico.Application
{
    public interface IInserirCategoria
    {
        Task Executar(Categoria entity);
    }
}
