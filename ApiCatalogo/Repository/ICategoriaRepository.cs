using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using System.Collections.Generic;

namespace ApiCatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<Categoria> GetCategoriasProdutos();

        PagedList<Categoria> GetCategoriasPaginadas(CategoriasParameters categoriasParameters);
    }
}
