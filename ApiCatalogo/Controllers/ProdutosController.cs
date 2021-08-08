using ApiCatalogo.DTOs;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProdutosController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._uow = unitOfWork;
            this._mapper = mapper;
        }

        [HttpGet("menorpreco")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPreco()
        {
            var produtos = await _uow.ProdutoRepository.GetProdutosPorPreco();
            return _mapper.Map<List<ProdutoDTO>>(produtos);
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery]ProdutosParameters produtosParameters)
        {
            var produtos = await _uow.ProdutoRepository.GetProdutos(produtosParameters);
            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            return _mapper.Map<List<ProdutoDTO>>(produtos);
        }

        [HttpGet("{id}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> GetById(int id)
        {
            var produto = await _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null)
                return NotFound();
            return _mapper.Map<ProdutoDTO>(produto);
        }

        [HttpPost]
        public async Task<ActionResult> Incluir([FromBody] ProdutoDTO produtoDto)
        {
            var produto = _mapper.Map<Produto>(produtoDto);
            _uow.ProdutoRepository.Add(produto);
            await _uow.Commit();
            var produtoOutput = _mapper.Map<ProdutoDTO>(produto);
            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoOutput);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Alterar(int id, [FromBody] ProdutoDTO produtoDto)
        {
            if (id != produtoDto.ProdutoId)
                return BadRequest();
            var produto = _mapper.Map<Produto>(produtoDto);
            _uow.ProdutoRepository.Update(produto);
            await _uow.Commit();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produto = await _uow.ProdutoRepository.GetById(p => p.ProdutoId == id);
            if (produto == null)
                return NotFound();
            _uow.ProdutoRepository.Delete(produto);
            await _uow.Commit();
            return _mapper.Map<ProdutoDTO>(produto);
        }
    }
}
