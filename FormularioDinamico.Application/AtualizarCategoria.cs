﻿using FormularioDinamico.Domain;
using FormularioDinamico.Domain.Repositories;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace FormularioDinamico.Application
{
    public class AtualizarCategoria : IAtualizarCategoria
    {
        private ICategoriaRepository _repository;
        private Notification _notification = new Notification();

        public AtualizarCategoria(ICategoriaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Notification> Executar(Categoria entity)
        {
            _notification.Errors.Clear();

            Validate(entity);

            if (_notification.HasErrors == true)
            {
                return _notification;
            }

            _repository.Edit(entity);

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
            int exist = _repository.FindBy(f => f.Slug == entity.Slug  && f.Id != entity.Id).Count();
            Fail(exist > 0, "Já existe outra categoria com o mesmo slug");
        }

        protected void Fail(bool condition, string error)
        {
            if (condition) _notification.Errors.Add(error);
        }
    }
}
