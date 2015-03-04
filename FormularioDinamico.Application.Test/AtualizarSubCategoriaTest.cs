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
    public class AtualizarSubCategoriaTest
    {
        [TestMethod]
        public async Task AtualizandoSubCategoria()
        {
            var entity = new SubCategoria();
            var repository = new Mock<ISubCategoriaRepository>();
            repository
                .Setup(s => s.Edit(It.IsAny<SubCategoria>()))
                .Verifiable();
            
            repository
                .Setup(s => s.SaveAsync())       
                .Returns(Task.Delay(1))
                .Verifiable();

            AtualizarSubCategoria testClass = new AtualizarSubCategoria(repository.Object);
                        
            Notification note = await testClass.Executar(entity);

            repository.Verify(v => v.Edit(entity));
            repository.Verify(v => v.SaveAsync());

            Assert.AreEqual(false, note.HasErrors);
        }

        [TestMethod]
        public async Task AtualizandoSubCategoriaComSlugDuplicado()
        {
            var entity = new SubCategoria();
            var repository = new Mock<ISubCategoriaRepository>();
            repository
                .Setup(s => s.Edit(It.IsAny<SubCategoria>()))
                .Verifiable();

            repository
                .Setup(s => s.SaveAsync())
                .Returns(Task.Delay(1))
                .Verifiable();

            repository
                .Setup(s => s.FindBy(It.IsAny<Expression<Func<SubCategoria, bool>>>()))
                .Returns(new List<SubCategoria> { new SubCategoria() }.AsQueryable());

            AtualizarSubCategoria testClass = new AtualizarSubCategoria(repository.Object);

            Notification note = await testClass.Executar(entity);

            Assert.AreEqual(true, note.HasErrors);
            Assert.AreEqual("Já existe outra sub-categoria com o mesmo slug", note.Errors.FirstOrDefault());
        }
    }
}
