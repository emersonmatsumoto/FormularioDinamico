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
    public class AtualizarCategoriaTest
    {
        [TestMethod]
        public async Task AtualizandoCategoria()
        {
            var entity = new Categoria();
            var repository = new Mock<ICategoriaRepository>();
            repository
                .Setup(s => s.Edit(It.IsAny<Categoria>()))
                .Verifiable();
            
            repository
                .Setup(s => s.SaveAsync())       
                .Returns(Task.Delay(1))
                .Verifiable();

            AtualizarCategoria testClass = new AtualizarCategoria(repository.Object);
                        
            Notification note = await testClass.Executar(entity);

            repository.Verify(v => v.Edit(entity));
            repository.Verify(v => v.SaveAsync());

            Assert.AreEqual(false, note.HasErrors);
        }

        [TestMethod]
        public async Task AtualizandoCategoriaComSlugDuplicado()
        {
            var entity = new Categoria();
            var repository = new Mock<ICategoriaRepository>();
            repository
                .Setup(s => s.Edit(It.IsAny<Categoria>()))
                .Verifiable();

            repository
                .Setup(s => s.SaveAsync())
                .Returns(Task.Delay(1))
                .Verifiable();

            repository
                .Setup(s => s.FindBy(It.IsAny<Expression<Func<Categoria, bool>>>()))
                .Returns(new List<Categoria>{ new Categoria() }.AsQueryable());

            AtualizarCategoria testClass = new AtualizarCategoria(repository.Object);

            Notification note = await testClass.Executar(entity);

            Assert.AreEqual(true, note.HasErrors);
            Assert.AreEqual("Já existe outra categoria com o mesmo slug", note.Errors.FirstOrDefault());
        }
    }
}
