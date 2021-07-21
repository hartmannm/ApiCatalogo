using ApiCatalogo.Models;
using ApiCatalogo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CategoriasController : Controller
    {

        private readonly IUnitOfWork _uow;

        public CategoriasController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _uow.CategoriaRepository.Get().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar ober as categorias");
            }
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                return _uow.CategoriaRepository.GetCategoriasProdutos().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar ober as categorias");
            }
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public ActionResult<Categoria> GetById(int id)
        {
            try
            {
                var categoria = _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
                if (categoria == null)
                    return NotFound("Categoria não encontrada");
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar ober a categoria");
            }
        }

        [HttpPost]
        public ActionResult Incluir([FromBody] Categoria categoria)
        {
            try
            {
                _uow.CategoriaRepository.Add(categoria);
                _uow.Commit();
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar incluir categoria");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Alterar(int id, [FromBody] Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                    return BadRequest("Não foi possível atualizar a categoria");
                _uow.CategoriaRepository.Update(categoria);
                _uow.Commit();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar alterar categoria");
            }

        }

        [HttpDelete("{id}")]
        public ActionResult<Categoria> Delete(int id)
        {
            try
            {
                var categoria = _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
                if (categoria == null)
                    return NotFound("Categoria não encontrada");
                _uow.CategoriaRepository.Delete(categoria);
                _uow.Commit();
                return categoria;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar remover categoria");
            }
        }
    }
}
