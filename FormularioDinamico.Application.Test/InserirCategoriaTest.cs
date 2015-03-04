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
    public class InserirCategoriaTest
    {
        [TestMethod]
        public async Task InserindoCategoria()
        {
            var entity = new Categoria();
            var repository = new Mock<ICategoriaRepository>();
            repository
                .Setup(s => s.Add(It.IsAny<Categoria>()))
                .Verifiable();
            
            repository
                .Setup(s => s.SaveAsync())       
                .Returns(Task.Delay(1))
                .Verifiable();

            InserirCategoria testClass = new InserirCategoria(repository.Object);
                        
            Notification note = await testClass.Executar(entity);

            repository.Verify(v => v.Add(entity));
            repository.Verify(v => v.SaveAsync());

            Assert.AreEqual(false, note.HasErrors);
        }

        [TestMethod]
        public async Task InserindoCategoriaComSlugDuplicado()
        {
            var entity = new Categoria();
            var repository = new Mock<ICategoriaRepository>();
            repository
                .Setup(s => s.Add(It.IsAny<Categoria>()))
                .Verifiable();

            repository
                .Setup(s => s.SaveAsync())
                .Returns(Task.Delay(1))
                .Verifiable();

            repository
                .Setup(s => s.FindBy(It.IsAny<Expression<Func<Categoria, bool>>>()))
                .Returns(new List<Categoria>{ new Categoria() }.AsQueryable());

            InserirCategoria testClass = new InserirCategoria(repository.Object);

            Notification note = await testClass.Executar(entity);

            repository.Verify(v => v.Add(entity));
            repository.Verify(v => v.SaveAsync());

            Assert.AreEqual(true, note.HasErrors);
            Assert.AreEqual("Já existe outra categoria com o mesmo slug", note.Errors.FirstOrDefault());
        }
    }
}
