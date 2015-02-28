using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormularioDinamico.Domain;
using System.Threading.Tasks;
using Moq;
using FormularioDinamico.Domain.Repositories;

namespace FormularioDinamico.Application.Test
{
    [TestClass]
    public class InserirCategoriaTest
    {
        [TestMethod]
        public async Task InserindoCategoria()
        {
            var entity = new Categoria();
            var repository = new Mock<ICategoriaRepository>();
            repository
                .Setup(s => s.Insert(It.IsAny<Categoria>()))
                .Verifiable();
            
            repository
                .Setup(s => s.SaveAsync())       
                .Returns(Task.Delay(1))
                .Verifiable();

            InserirCategoria testClass = new InserirCategoria(repository.Object);
                        
            await testClass.Executar(entity);

            repository.Verify(v => v.Insert(entity));
            repository.Verify(v => v.SaveAsync());
        }
    }
}
