using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.BD;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    [Route("api/auditoria")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        private readonly AppBDContext _context;

        public AuditoriaController(AppBDContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("listar")]

        public async Task<ActionResult<IEnumerable<AuditoriaModel>>> GetAuditoria()
        {
            return await _context.Auditorias.ToListAsync();
        }

        [HttpGet]
        [Route("obtener/{Id_Auditoria}")]

        public async Task<ActionResult<AuditoriaModel>> GetAuditoriaById(int Id_Auditoria)
        {
            var auditoria = await _context.Auditorias.FindAsync(Id_Auditoria);

            if (auditoria == null)
            {
                return NotFound();
            }
            return auditoria;
        }

        [HttpPut]
        [Route("actualizar/{Id_Auditoria}")]

        public async Task<ActionResult> PutAuditoria(int Id_Auditoria, [FromBody] AuditoriaModel auditoria)
        {
            if (Id_Auditoria != auditoria.Id_Auditoria)
            {
                return BadRequest();
            }

            _context.Entry(auditoria).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Auditorias.Any(e => e.Id_Auditoria == Id_Auditoria))
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
        [Route("eliminar/{Id_Auditoria}")]

        public async Task<ActionResult> DeleteAuditoria(int Id_Auditoria)
        {
            var auditoria = await _context.Auditorias.FindAsync(Id_Auditoria);

            if (auditoria == null)
            {
                return NotFound();
            }

            _context.Auditorias.Remove(auditoria);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpPost]
        [Route("crear")]

        public async Task<ActionResult<AuditoriaModel>> PostAuditoria([FromBody] AuditoriaModel auditoria)
        {
            _context.Auditorias.Add(auditoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuditoriaById), new { Id_Auditoria = auditoria.Id_Auditoria }, auditoria);
        }

    }
}
