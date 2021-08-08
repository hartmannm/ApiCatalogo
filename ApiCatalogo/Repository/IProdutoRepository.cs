using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetProdutosPorPreco();

        Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters);
    }
}
