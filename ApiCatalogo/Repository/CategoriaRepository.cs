using ApiCatalogo.Context;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Repository
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PagedList<Categoria>> GetCategoriasPaginadas(CategoriasParameters categoriasParameters)
        {
            var categorias = Get()
                .OrderBy(categoria => categoria.Nome);
            return await PagedList<Categoria>.toPageList(categorias, categoriasParameters.PageNumber, categoriasParameters.PageSize);
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            return await Get().Include(c => c.Produtos).ToListAsync();
        }
    }
}
