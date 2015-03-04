
namespace FormularioDinamico.Domain.Repositories
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Categoria GetSingle(int categoriaId);
    }
}
