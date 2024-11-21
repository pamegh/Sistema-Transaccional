using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.BD;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    [Route("api/transaccion")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        private readonly AppBDContext _context;
        public TransaccionController(AppBDContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("listar")]

        public async Task<ActionResult<IEnumerable<TransaccionModel>>> GetTransaccion()
        {
            return await _context.Transacciones.ToListAsync();
        }

        [HttpGet]
        [Route("obtener/{Id_Transaccion}")]

        public async Task<ActionResult<TransaccionModel>> GetTransaccionById(int Id_Transaccion)
        {
            var transaccion = await _context.Transacciones.FindAsync(Id_Transaccion);

            if (transaccion == null)
            {
                return NotFound();
            }
            return transaccion;
        }

        [HttpGet]
        [Route("actualizar/{Id_Transaccion}")]

        public async Task<ActionResult> PutTransaccion(int Id_Transaccion, [FromBody] TransaccionModel transaccion)
        {
            if (Id_Transaccion != transaccion.Id_Transaccion)
            {
                return BadRequest();
            }

            _context.Entry(transaccion).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Transacciones.Any(e => e.Id_Transaccion == Id_Transaccion))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpGet]
        [Route("eliminar/{Id_Transaccion}")]

        public async Task<ActionResult> DeleteTransaccion(int Id_Transaccion)
        {
            var transaccion = await _context.Transacciones.FindAsync(Id_Transaccion);

            if (transaccion == null)
            {
                return NotFound();
            }

            _context.Transacciones.Remove(transaccion);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpPost]
        [Route("crear")]

        public async Task<ActionResult<TransaccionModel>> PostTransaccion([FromBody] TransaccionModel transaccion)
        {
            _context.Transacciones.Add(transaccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTransaccionById), new { Id_Transaccion = transaccion.Id_Transaccion }, transaccion);
        }
    }
}
