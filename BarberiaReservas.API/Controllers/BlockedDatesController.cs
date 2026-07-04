using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Domain.Entities;
using BarberiaReservas.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarberiaReservas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class BlockedDatesController : ControllerBase
{
    private readonly AppDbContext _context;

    public BlockedDatesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ObtenerTodasLasFechasBoqueadas(
        [FromQuery] DateTime? desde,
        [FromQuery] DateTime? hasta)
    {
        var query = _context.BlockedDates.AsQueryable();

        if (desde.HasValue)
            query = query.Where(b => b.Date >= desde.Value);

        if (hasta.HasValue)
            query = query.Where(b => b.Date <= hasta.Value);

        var blockedDates = await query
            .OrderBy(b => b.Date)
            .ToListAsync();

        var dtos = blockedDates.Select(b => new BlockedDateResponseDto
        {
            Id = b.Id,
            Date = b.Date,
            Reason = b.Reason,
            CreatedAt = b.CreatedAt
        });

        return Ok(dtos);
    }

    [HttpPost]
    public async Task<IActionResult> CrearFechaBoqueada([FromBody] CreateBlockedDateDto dto)
    {
        if (dto.Date < DateTime.Now.Date)
            return BadRequest(new { mensaje = "No se puede bloquear una fecha pasada" });

        var blockedDate = new BlockedDate
        {
            Date = dto.Date,
            Reason = dto.Reason,
            CreatedAt = DateTime.UtcNow
        };

        _context.BlockedDates.Add(blockedDate);
        await _context.SaveChangesAsync();

        var responseDto = new BlockedDateResponseDto
        {
            Id = blockedDate.Id,
            Date = blockedDate.Date,
            Reason = blockedDate.Reason,
            CreatedAt = blockedDate.CreatedAt
        };

        return CreatedAtAction(nameof(ObtenerFechaBoqueadaPorId), new { id = blockedDate.Id }, responseDto);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> ObtenerFechaBoqueadaPorId(int id)
    {
        var blockedDate = await _context.BlockedDates.FindAsync(id);
        if (blockedDate == null)
            return NotFound();

        var dto = new BlockedDateResponseDto
        {
            Id = blockedDate.Id,
            Date = blockedDate.Date,
            Reason = blockedDate.Reason,
            CreatedAt = blockedDate.CreatedAt
        };

        return Ok(dto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> EliminarFechaBoqueada(int id)
    {
        var blockedDate = await _context.BlockedDates.FindAsync(id);
        if (blockedDate == null)
            return NotFound();

        _context.BlockedDates.Remove(blockedDate);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
