using Franquias.Application.Interfaces.Repositories;
using Franquias.Domain.Entities;
using Franquias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Franquias.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario> AdicionarAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<Usuario>> ObterTodasAsync()
    {
        return await _context.Usuarios
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return await _context.Usuarios
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<Usuario> AtualizarAsync(Usuario usuario)
    {
        _context.Update(usuario);
        await _context.SaveChangesAsync();

        return usuario;
    }
}