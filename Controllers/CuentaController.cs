using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.BD;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    [Route("api/cuenta")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly AppBDContext _context;

        public CuentaController(AppBDContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("listar")]

        public async Task<ActionResult<IEnumerable<CuentaModel>>> GetCuenta()
        {
            return await _context.Cuentas.ToListAsync();
        }

        [HttpGet]
        [Route("obtener/{Id_Cuenta}")]

        public async Task<ActionResult<CuentaModel>> GetCuentaById(int Id_Cuenta)
        {
            var cuenta = await _context.Cuentas.FindAsync(Id_Cuenta);

            if (cuenta == null)
            {
                return NotFound("El Id proporcionado no coincide con el Id de la cuenta.");
            }
            return cuenta;
        }

        [HttpPut]
        [Route("actualizar/{Id_Cuenta}")]

        public async Task<ActionResult> PutCuenta(int Id_Cuenta, [FromBody] CuentaModel cuenta)
        {
            if (Id_Cuenta != cuenta.Id_Cuenta)
            {
                return BadRequest();
            }

            _context.Entry(cuenta).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Cuentas.Any(e => e.Id_Cuenta == Id_Cuenta))
                {
                    return NotFound("Cuenta no encontrada");
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpGet]
        [Route("eliminar/{Id_Cuenta}")]

        public async Task<ActionResult> DeleteCuenta(int Id_Cuenta)
        {
            var cuenta = await _context.Cuentas.FindAsync(Id_Cuenta);

            if (cuenta == null)
            {
                return NotFound();
            }

            _context.Cuentas.Remove(cuenta);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpPost]
        [Route("crear")]

        public async Task<ActionResult<CuentaModel>> PostCuenta([FromBody] CuentaModel cuenta)
        {
            _context.Cuentas.Add(cuenta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCuentaById), new { Id_Cuenta = cuenta.Id_Cuenta }, cuenta);
        }
    }
}
