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
    public class InserirSubCategoriaTest
    {
        [TestMethod]
        public async Task InserindoSubCategoria()
        {
            var entity = new SubCategoria();
            var repository = new Mock<ISubCategoriaRepository>();
            repository
                .Setup(s => s.Add(It.IsAny<SubCategoria>()))
                .Verifiable();
            
            repository
                .Setup(s => s.SaveAsync())       
                .Returns(Task.Delay(1))
                .Verifiable();

            InserirSubCategoria testClass = new InserirSubCategoria(repository.Object);
                        
            Notification note = await testClass.Executar(entity);

            repository.Verify(v => v.Add(entity));
            repository.Verify(v => v.SaveAsync());

            Assert.AreEqual(false, note.HasErrors);
        }

        [TestMethod]
        public async Task InserindoCategoriaComSlugDuplicado()
        {
            var entity = new SubCategoria();
            var repository = new Mock<ISubCategoriaRepository>();
            repository
                .Setup(s => s.Add(It.IsAny<SubCategoria>()))
                .Verifiable();

            repository
                .Setup(s => s.SaveAsync())
                .Returns(Task.Delay(1))
                .Verifiable();

            repository
                .Setup(s => s.FindBy(It.IsAny<Expression<Func<SubCategoria, bool>>>()))
                .Returns(new List<SubCategoria> { new SubCategoria() }.AsQueryable());

            InserirSubCategoria testClass = new InserirSubCategoria(repository.Object);

            Notification note = await testClass.Executar(entity);

            Assert.AreEqual(true, note.HasErrors);
            Assert.AreEqual("Já existe outra sub-categoria com o mesmo slug", note.Errors.FirstOrDefault());
        }
    }
}
