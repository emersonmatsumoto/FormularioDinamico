using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormularioDinamico.Domain;
using System.Threading.Tasks;
using Moq;
using FormularioDinamico.Domain.Repositories;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace FormularioDinamico.Application.Test
{
    [TestClass]
    public class RemoverCategoriaTest
    {
        [TestMethod]
        public async Task RemovendoCategoria()
        {
            var entity = new Categoria() { SubCategorias = new List<SubCategoria>() };
            var repository = new Mock<ICategoriaRepository>();
            repository
                .Setup(s => s.Delete(It.IsAny<Categoria>()))
                .Verifiable();
            
            repository
                .Setup(s => s.SaveAsync())       
                .Returns(Task.Delay(1))
                .Verifiable();

            repository
                .Setup(s => s.GetSingle(It.IsAny<int>()))
                .Returns(entity);

            RemoverCategoria testClass = new RemoverCategoria(repository.Object);
                        
            Notification note = await testClass.Executar(1);

            repository.Verify(v => v.Delete(entity));
            repository.Verify(v => v.SaveAsync());

            Assert.AreEqual(false, note.HasErrors);
        }

        [TestMethod]
        public async Task RemovendoCategoriaComSubCategorias()
        {
            var entity = new Categoria() { SubCategorias = new List<SubCategoria> { new SubCategoria() } };
            var repository = new Mock<ICategoriaRepository>();
            repository
                .Setup(s => s.Delete(It.IsAny<Categoria>()))
                .Verifiable();

            repository
                .Setup(s => s.SaveAsync())
                .Returns(Task.Delay(1))
                .Verifiable();

            repository
                .Setup(s => s.GetSingle(It.IsAny<int>()))
                .Returns(entity);

            RemoverCategoria testClass = new RemoverCategoria(repository.Object);

            Notification note = await testClass.Executar(1);

            Assert.AreEqual(true, note.HasErrors);
            Assert.AreEqual("É necessário remover todas sub-categorias desta categoria", note.Errors.FirstOrDefault());
        }
    }
}
