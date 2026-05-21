using BarberiaReservas.Application.DTOs;
using BarberiaReservas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BarberiaReservas.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public ServicesController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var services = await _serviceManager.GetAllAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var service = await _serviceManager.GetByIdAsync(id);

        if (service == null)
            return NotFound();

        return Ok(service);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateServiceDto dto)
    {
        var createdService = await _serviceManager.CreateAsync(dto);

        return Ok(createdService);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateServiceDto dto)
    {
        var updated = await _serviceManager.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _serviceManager.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}