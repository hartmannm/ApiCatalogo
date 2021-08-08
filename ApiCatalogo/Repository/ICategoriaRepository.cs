using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();

        Task<PagedList<Categoria>> GetCategoriasPaginadas(CategoriasParameters categoriasParameters);
    }
}
