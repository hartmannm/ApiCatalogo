﻿using ApiCatalogo.DTOs;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiCatalogo.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CategoriasController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CategoriasController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriasParameters categoriasParameters)
        {
            try
            {
                var categorias = await _uow.CategoriaRepository.GetCategoriasPaginadas(categoriasParameters);
                var metadata = new
                {
                    categorias.TotalCount,
                    categorias.PageSize,
                    categorias.CurrentPage,
                    categorias.TotalPages,
                    categorias.HasNext,
                    categorias.HasPrevious
                };
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
                return _mapper.Map<List<CategoriaDTO>>(categorias);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar ober as categorias");
            }
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
        {
            try
            {
                var categorias = await _uow.CategoriaRepository.GetCategoriasProdutos();
                return _mapper.Map<List<CategoriaDTO>>(categorias);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar ober as categorias");
            }
        }

        [HttpGet("{id}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoriaDTO>> GetById(int id)
        {
            try
            {
                var categoria = await _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
                if (categoria == null)
                    return NotFound("Categoria não encontrada");
                return _mapper.Map<CategoriaDTO>(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar ober a categoria");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Incluir([FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaDto);
                _uow.CategoriaRepository.Add(categoria);
                await _uow.Commit();
                var categoriaOutput = _mapper.Map<CategoriaDTO>(categoria);
                return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoriaOutput);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar incluir categoria");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Alterar(int id, [FromBody] CategoriaDTO categoriaDto)
        {
            try
            {
                var categoria = _mapper.Map<Categoria>(categoriaDto);
                if (id != categoria.CategoriaId)
                    return BadRequest("Não foi possível atualizar a categoria");
                _uow.CategoriaRepository.Update(categoria);
                await _uow.Commit();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar alterar categoria");
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            try
            {
                var categoria = await _uow.CategoriaRepository.GetById(p => p.CategoriaId == id);
                if (categoria == null)
                    return NotFound("Categoria não encontrada");
                _uow.CategoriaRepository.Delete(categoria);
                await _uow.Commit();
                return _mapper.Map<CategoriaDTO>(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao tentar remover categoria");
            }
        }
    }
}
