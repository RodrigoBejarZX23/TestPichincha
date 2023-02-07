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
    public class MovimientoController : ControllerBase
    {
        private readonly IMovimientoRepository _movimientoRepository;
        public MovimientoController(IMovimientoRepository movimientoRepository)
        {
            _movimientoRepository = movimientoRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var Movimiento = _movimientoRepository.GetMovimientos();
                return new JsonResult(Movimiento);
            }
            catch (Exception e)
            {
                return new JsonResult("Error");
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var Movimiento = _movimientoRepository.GetMovimiento(id);
                if (Movimiento.CuentaId > 0)
                {
                    return new JsonResult(Movimiento);
                }
                else
                {
                    return BadRequest("Error: Movimiento no encontrado");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

        [HttpPost]
        [Route("AddMovimiento")]
        public IActionResult Post([FromBody] Movimiento value)
        {
            try
            {
                Cuenta cuenta = _movimientoRepository.ObtenerCuenta(value.CuentaId);
                if (cuenta == null)
                {
                    return BadRequest("Cuenta no encontrada");
                }
                else {
                    value.Cuenta = cuenta;
                }
                //1 Retiro 2 Deposito
                if (value.TipoMovimiento == "Retiro")
                {

                    if (value.Cuenta.SaldoInicial < 0)
                    {
                        return BadRequest("Saldo no disponible");
                    }
                    else if ((value.Cuenta.SaldoInicial - value.Valor) < 0)
                    {
                        return BadRequest("Saldo no disponible");
                    }
                    else
                    {
                        value.Saldo = value.Cuenta.SaldoInicial - value.Valor;
                        value.Cuenta.SaldoInicial = value.Cuenta.SaldoInicial - value.Valor;               
                    }
                }
                else {                    
                    value.Saldo = value.Cuenta.SaldoInicial + value.Valor;
                    value.Cuenta.SaldoInicial = value.Cuenta.SaldoInicial + value.Valor;
                }
                var MovimientoId = _movimientoRepository.InsertMovimiento(value);
                if (int.Parse(MovimientoId) > 0)
                {
                    value.MovimientoId = int.Parse(MovimientoId);
                    return new JsonResult(value);
                }
                else
                {
                    return BadRequest("Error: Movimiento no agregado");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

        [HttpPut]
        [Route("UpdateMovimiento/{id}")]
        public IActionResult Put(int id, [FromBody] Movimiento value)
        {
            try
            {
                if (value.MovimientoId != id)
                {
                    return BadRequest("ids did not match");
                }
                var MovimientoId = _movimientoRepository.UpdateMovimiento(value);
                if (int.Parse(MovimientoId) > 0)
                {
                    return new JsonResult(value);
                }
                else
                {
                    return BadRequest("Error: Movimiento no actualizado");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var MovimientoId = _movimientoRepository.DeleteMovimiento(id);
                if (int.Parse(MovimientoId) > 0)
                {
                    return new JsonResult("Movimiento con codigo: " + id + " fue eliminado correctamente");
                }
                else
                {
                    return BadRequest("Error: Movimiento no eliminado");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }

        [HttpGet]
        [Route("Reporte")]
        public IActionResult ListadoMovimientoPorUsuarios(string fecha, string nombre) {
            try
            {
                var fechaBusqueda = Convert.ToDateTime(fecha);
                var Movimientos = _movimientoRepository.GetMovimientos();

                List<MovimientosCuentasClientesPersonas> list = new List<MovimientosCuentasClientesPersonas>();

                if (Movimientos.Count() > 0)
                {
                    foreach (var movi in Movimientos) 
                    {
                        var Cuentas = _movimientoRepository.ObtenerCuenta(movi.CuentaId);
                        movi.Cuenta = Cuentas;
                    }
                    foreach (var cuentas in Movimientos)
                    {
                        var Clientes = _movimientoRepository.ObtenerCliente(cuentas.Cuenta.ClienteId);
                        cuentas.Cuenta.Cliente = Clientes;
                    }
                    foreach (var cliente in Movimientos)
                    {
                        var Persona = _movimientoRepository.ObtenerPersona(cliente.Cuenta.Cliente.PersonaId);
                        cliente.Cuenta.Cliente.Persona = Persona;

                        var mov = new MovimientosCuentasClientesPersonas();
                        mov.MovimientoId = cliente.MovimientoId;
                        mov.Fecha = cliente.Fecha;
                        mov.Valor = cliente.Valor;
                        mov.CuentaId = cliente.CuentaId;

                        mov.NumeroCuenta = cliente.Cuenta.NumeroCuenta;
                        mov.TipoCuenta = cliente.Cuenta.TipoCuenta;
                        mov.SaldoInicial = cliente.Cuenta.SaldoInicial;
                        mov.Estado = cliente.Cuenta.Estado;
                        mov.ClienteId = cliente.Cuenta.ClienteId;
                        mov.PersonaId = cliente.Cuenta.Cliente.PersonaId;
                        mov.Nombre = cliente.Cuenta.Cliente.Persona.Nombre;
                        list.Add(mov);
                    }

                    var ReporteJson = list.FindAll(x => x.Nombre == nombre && x.Fecha >= fechaBusqueda).OrderByDescending(x => x.Fecha);
                    return new JsonResult(ReporteJson);

                }
                else
                {
                    return BadRequest("Error: Listado de Movimientos por Usuarios no Procesado");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Error");
            }
        }
    }
}
