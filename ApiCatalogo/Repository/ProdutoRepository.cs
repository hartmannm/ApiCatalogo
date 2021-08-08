using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
        {
            var produtos = Get()
                .OrderBy(produto => produto.Nome);
            return await PagedList<Produto>.toPageList(produtos, produtosParameters.PageNumber, produtosParameters.PageSize);
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
        {
            return await Get().OrderBy(p => p.Preco).ToListAsync();
        }
    }
}
