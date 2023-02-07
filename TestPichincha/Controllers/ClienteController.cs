using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.Repository;
using System.Transactions;
using TestPichincha.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestPichincha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteController(IClienteRepository clienteRepository) {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try {
                var Usuario = _clienteRepository.GetClientes();
                return new JsonResult(Usuario);
            }
            catch(Exception e)
            {
                return BadRequest("Error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var Cliente = _clienteRepository.GetCliente(id);
                if (Cliente.ClienteId > 0) {
                    return new JsonResult(Cliente);
                }
                else {
                    return BadRequest("Error: Cliente no encontrado");
                }                
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

        [HttpPost]
        [Route("AddCliente")]
        public IActionResult Post([FromBody] Cliente value)
        {
            try
            {
                var ClienteId = _clienteRepository.InsertCliente(value);
                if (int.Parse(ClienteId) > 0)
                {
                    value.ClienteId = int.Parse(ClienteId);
                    return new JsonResult(value);
                }
                else
                {
                    return BadRequest("Error: Cliente no agregado");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }         
        }

        [HttpPut]
        [Route("UpdateCliente/{id}")]
        public IActionResult Put(int id, [FromBody] Cliente value)
        {
            try
            {
                if (value.ClienteId != id) {
                    return BadRequest("ids did not match");
                }
                var Cliente = _clienteRepository.UpdateCliente(value);
                if (int.Parse(Cliente) > 0)
                {
                    return new JsonResult(value);
                }
                else
                {
                    return BadRequest("Error: Cliente no actualizado");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

        [HttpDelete]
        [Route("DeleteCliente/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Cliente = _clienteRepository.DeleteCliente(id);
                if (int.Parse(Cliente) > 0)
                {
                    return new JsonResult("Cliente con codigo: "+ id + " fue eliminado correctamente");
                }
                else
                {
                    return BadRequest("Error: Cliente no eliminado");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }
    }
}
