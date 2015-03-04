using FormularioDinamico.Domain;
using FormularioDinamico.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace FormularioDinamico.Application
{
    public class RemoverSubCategoria : IRemoverSubCategoria
    {
        private ISubCategoriaRepository _repository;
        private Notification _notification = new Notification();

        public RemoverSubCategoria(ISubCategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Notification> Executar(int subCategoriaId)
        {
            _notification.Errors.Clear();

            SubCategoria entity = _repository.GetSingle(subCategoriaId);

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

        private void Validate(SubCategoria entity)
        {
            
        }

        protected void Fail(bool condition, string error)
        {
            if (condition) _notification.Errors.Add(error);
        }
    }
}
