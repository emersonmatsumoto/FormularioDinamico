using FormularioDinamico.Domain;
using FormularioDinamico.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace FormularioDinamico.Application
{
    public class RemoverCategoria : IRemoverCategoria
    {
        private ICategoriaRepository _repository;
        private Notification _notification = new Notification();

        public RemoverCategoria(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Notification> Executar(int categoriaId)
        {
            _notification.Errors.Clear();

            Categoria entity = _repository.GetSingle(categoriaId);

            Validate(entity);

            if (_notification.HasErrors == true)
            {
                return _notification;
            }

            _repository.Delete(entity);

            try
            {
                await _repository.SaveAsync();
            }
            catch(Exception ex)
            {
                _notification.Errors.Add(ex.Message);
            }

            return _notification;
        }

        private void Validate(Categoria entity)
        {
            int subcategorias = entity.SubCategorias.Count();
            Fail(subcategorias > 0, "É necessário remover todas sub-categorias desta categoria");
        }

        protected void Fail(bool condition, string error)
        {
            if (condition) _notification.Errors.Add(error);
        }
    }
}
