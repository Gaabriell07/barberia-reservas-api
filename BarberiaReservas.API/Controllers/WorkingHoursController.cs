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
public class WorkingHoursController : ControllerBase
{
    private readonly AppDbContext _context;

    public WorkingHoursController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodosLosHorarios()
    {
        var hours = await _context.WorkingHours
            .OrderBy(w => w.DayOfWeek)
            .ToListAsync();

        var dtos = hours.Select(h => new WorkingHoursResponseDto
        {
            Id = h.Id,
            DayOfWeek = h.DayOfWeek,
            StartTime = h.StartTime,
            EndTime = h.EndTime,
            IsActive = h.IsActive
        });

        return Ok(dtos);
    }

    [HttpGet("{dayOfWeek}")]
    public async Task<IActionResult> ObtenerHorarioPorDia(string dayOfWeek)
    {
        var workingHour = await _context.WorkingHours
            .FirstOrDefaultAsync(w => w.DayOfWeek == dayOfWeek);

        if (workingHour == null)
            return NotFound(new { mensaje = $"Horario no encontrado para {dayOfWeek}" });

        var dto = new WorkingHoursResponseDto
        {
            Id = workingHour.Id,
            DayOfWeek = workingHour.DayOfWeek,
            StartTime = workingHour.StartTime,
            EndTime = workingHour.EndTime,
            IsActive = workingHour.IsActive
        };

        return Ok(dto);
    }

    [HttpPut("{dayOfWeek}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ActualizarHorario(string dayOfWeek, [FromBody] UpdateWorkingHoursDto dto)
    {
        var workingHour = await _context.WorkingHours
            .FirstOrDefaultAsync(w => w.DayOfWeek == dayOfWeek);

        if (workingHour == null)
        {
            workingHour = new WorkingHours
            {
                DayOfWeek = dayOfWeek,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsActive = dto.IsActive
            };
            _context.WorkingHours.Add(workingHour);
        }
        else
        {
            workingHour.StartTime = dto.StartTime;
            workingHour.EndTime = dto.EndTime;
            workingHour.IsActive = dto.IsActive;
            _context.WorkingHours.Update(workingHour);
        }

        await _context.SaveChangesAsync();

        return Ok(new WorkingHoursResponseDto
        {
            Id = workingHour.Id,
            DayOfWeek = workingHour.DayOfWeek,
            StartTime = workingHour.StartTime,
            EndTime = workingHour.EndTime,
            IsActive = workingHour.IsActive
        });
    }

    [HttpDelete("{dayOfWeek}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DesactivarHorario(string dayOfWeek)
    {
        var workingHour = await _context.WorkingHours
            .FirstOrDefaultAsync(w => w.DayOfWeek == dayOfWeek);

        if (workingHour == null)
            return NotFound();

        workingHour.IsActive = false;
        _context.WorkingHours.Update(workingHour);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
