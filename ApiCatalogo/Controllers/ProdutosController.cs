using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uow;

        public ProdutosController(IUnitOfWork unitOfWork)
        {
            this._uow = unitOfWork;
        }

        [HttpGet("menorpreco")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPreco()
        {
            return _uow.ProdutoRepository.GetProdutosPorPreco().ToList();
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            return _uow.ProdutoRepository.Get().ToList();
        }

        [HttpGet("{id}", Name = "ObterProduto")]
        public ActionResult<Produto> GetById(int id)
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null)
                return NotFound();
            return produto;
        }

        [HttpPost]
        public ActionResult Incluir([FromBody] Produto produto)
        {
            _uow.ProdutoRepository.Add(produto);
            _uow.Commit();
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id}")]
        public ActionResult Alterar(int id, [FromBody] Produto produto)
        {
            if (id != produto.ProdutoId)
                return BadRequest();
            _uow.ProdutoRepository.Update(produto);
            _uow.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<Produto> Delete(int id)
        {
            var produto = _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null)
                return NotFound();
            _uow.ProdutoRepository.Delete(produto);
            _uow.Commit();
            return produto;
        }
    }
}
