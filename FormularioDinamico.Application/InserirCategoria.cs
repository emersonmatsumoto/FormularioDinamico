using FormularioDinamico.Domain;
using FormularioDinamico.Domain.Repositories;
using System.Threading.Tasks;

namespace FormularioDinamico.Application
{
    public class InserirCategoria : IInserirCategoria
    {
        private ICategoriaRepository _repository;

        public InserirCategoria(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task Executar(Categoria entity)
        {
            _repository.Insert(entity);
            await _repository.SaveAsync();
        }
    }
}
