
namespace FormularioDinamico.Domain.Repositories
{
    public interface ISubCategoriaRepository : IRepository<SubCategoria>
    {
        SubCategoria GetSingle(int subCategoriaId);
    }
}
