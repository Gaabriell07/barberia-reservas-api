using BarberiaReservas.Application.Interfaces;
using BarberiaReservas.Domain.Entities;
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
        var services = await _serviceManager.GetAllServicesAsync();
        return Ok(services);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var service = await _serviceManager.GetServiceByIdAsync(id);

        if (service == null)
            return NotFound();

        return Ok(service);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Service service)
    {
        var created = await _serviceManager.CreateServiceAsync(service);

        return CreatedAtAction(nameof(GetById),
            new { id = created.Id },
            created);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Service service)
    {
        if (id != service.Id)
            return BadRequest();

        await _serviceManager.UpdateServiceAsync(service);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _serviceManager.DeleteServiceAsync(id);

        return NoContent();
    }
}