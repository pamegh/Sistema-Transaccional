using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proyecto.BD;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly AppBDContext _context;

        public UsuarioController(AppBDContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("listar")]

        public async Task<ActionResult<IEnumerable<UsuarioModel>>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        [HttpGet]
        [Route("obtener/{Id_Usuario}")]

        public async Task<ActionResult<UsuarioModel>> GetUsuarioById(int Id_Usuario)
        {
            var usuario = await _context.Usuarios.FindAsync(Id_Usuario);

            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPut]
        [Route("actualizar/{Id_Usuario}")]

        public async Task<ActionResult> PutUsuario(int Id_Usuario, [FromBody] UsuarioModel usuario)
        {
            if (Id_Usuario != usuario.Id_Usuario)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Usuarios.Any(e => e.Id_Usuario == Id_Usuario))
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
        [Route("eliminar/{Id_Usuario}")]

        public async Task<ActionResult> DeleteUsuario(int Id_Usuario) { 
        var usuario = await _context.Usuarios.FindAsync(Id_Usuario);
        
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();

        }

        [HttpPost]
        [Route("crear")]

        public async Task<ActionResult<UsuarioModel>> PostUsuario([FromBody] UsuarioModel usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuarioById), new { Id_Usuario = usuario.Id_Usuario}, usuario);
        }
    }
}
