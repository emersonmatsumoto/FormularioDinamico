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
    public class RemoverSubCategoriaTest
    {
        [TestMethod]
        public async Task RemovendoSubCategoria()
        {
            var entity = new SubCategoria() {};
            var repository = new Mock<ISubCategoriaRepository>();
            repository
                .Setup(s => s.Delete(It.IsAny<SubCategoria>()))
                .Verifiable();
            
            repository
                .Setup(s => s.SaveAsync())       
                .Returns(Task.Delay(1))
                .Verifiable();

            repository
                .Setup(s => s.GetSingle(It.IsAny<int>()))
                .Returns(entity);

            RemoverSubCategoria testClass = new RemoverSubCategoria(repository.Object);
                        
            Notification note = await testClass.Executar(1);

            repository.Verify(v => v.Delete(entity));
            repository.Verify(v => v.SaveAsync());

            Assert.AreEqual(false, note.HasErrors);
        }

    }
}
