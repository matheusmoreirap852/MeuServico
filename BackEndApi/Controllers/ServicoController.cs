using BackEndApi.Dtos;
using BackEndApi.Models;
using BackEndApi.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEndApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly IMinhaInformacoes _minhaInformacoes;
            
        public ServicoController(IMinhaInformacoes minhaInformacoes)
        {
            _minhaInformacoes = minhaInformacoes ?? throw new ArgumentNullException(nameof(minhaInformacoes));
        }


        [HttpGet(Name = "GetAllServicos")]
        /*[Authorize]*/
        public async Task<ActionResult<IEnumerable<RegistroServico>>> GetAllServicos()
        {
            try
            {
              var dadosDto = (await _minhaInformacoes.GetAll())  // Aguardar a Task para obter o IEnumerable
                    .OrderByDescending(x => x.Id)   // Ordenar os dados por DataCadastro
                    .ToList();     

                if (dadosDto == null)
                    return NotFound("Atletas not found");
                return Ok(dadosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
         
        [HttpGet("{id}", Name = "GetServicoById")]
        /*[Authorize]*/
        public async Task<ActionResult<RegistroServico>> GetServicoById(decimal id)
        {
            try
            {
                var dadosDto = await _minhaInformacoes.GetAllId(id);
                if (dadosDto == null)
                    return NotFound("Atletas not found");
                return Ok(dadosDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(Name = "PostServico")]
        /*[Authorize]*/
        public async Task<ActionResult<RegistroServico>> PostServico([FromBody] RegistroServiceDto tbEqp_PesDto)
        {
            try
            {
                if (tbEqp_PesDto == null)
                    return BadRequest("Invalid data");
                var result = await _minhaInformacoes.Set(tbEqp_PesDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut(Name = "PutServico")]
        /*[Authorize]*/
        public async Task<ActionResult<RegistroServico>> PutServico(int id, [FromBody] RegistroServiceDto tbEqp_PesDto)
        {
            try
            {
                if (tbEqp_PesDto == null)
                    return BadRequest("Invalid data");
                var result = await _minhaInformacoes.Update(tbEqp_PesDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<RegistroServiceDto>> Delete(int id)
        {
            try
            {
                var dados = await _minhaInformacoes.GetAllId(id);
                if (dados == null)
                {
                    return NotFound("Atleta not found");
                }
                await _minhaInformacoes.DeleteById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
