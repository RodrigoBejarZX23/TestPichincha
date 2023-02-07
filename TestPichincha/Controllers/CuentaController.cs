using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestPichincha.Models;
using TestPichincha.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestPichincha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly ICuentaRepository _cuentaRepository;
        public CuentaController(ICuentaRepository cuentaRepository)
        {
            _cuentaRepository = cuentaRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var Cuenta = _cuentaRepository.GetCuentas();
                return new JsonResult(Cuenta);
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            };
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var Cuenta = _cuentaRepository.GetCuenta(id);
                if (Cuenta.CuentaId > 0)
                {
                    return new JsonResult(Cuenta);
                }
                else
                {
                    return BadRequest("Error: Cuenta no encontrado");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

        [HttpPost]
        [Route("AddCuenta")]
        public IActionResult Post([FromBody] Cuenta value)
        {
            try
            {
                var CuentaId = _cuentaRepository.InsertCuenta(value);
                if (int.Parse(CuentaId) > 0)
                {
                    value.CuentaId = int.Parse(CuentaId);
                    return new JsonResult(value);
                }
                else
                {
                    return BadRequest("Error: Cuenta no agregada");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

        [HttpPut]
        [Route("UpdateCuenta/{id}")]
        public IActionResult Put(int id, [FromBody] Cuenta value)
        {
            try
            {
                if (value.CuentaId != id)
                {
                    return BadRequest("ids did not match");
                }
                var CuentaId = _cuentaRepository.UpdateCuenta(value);
                if (int.Parse(CuentaId) > 0)
                {
                    return new JsonResult(value);
                }
                else
                {
                    return BadRequest("Error: Cuenta no actualizada");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

        [HttpDelete]
        [Route("DeleteCuenta/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var Cuenta = _cuentaRepository.DeleteCuenta(id);
                if (int.Parse(Cuenta) > 0)
                {
                    return new JsonResult("Cuenta con codigo: " + id + " fue eliminado correctamente");
                }
                else
                {
                    return BadRequest("Error: Cuenta no eliminado");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

    }
}
