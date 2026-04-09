using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetCareApi.Data;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly AppDbContext _context;

    // Inyección de dependencias del DbContext
    public AppointmentsController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/appointments
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var appointments = await _context.Appointments.ToListAsync();
        return Ok(appointments);
    }

    // POST: api/appointments
    [HttpPost]
    public async Task<IActionResult> Create(Appointment appointment)
    {
        // Validación básica
        if (appointment.Date < DateTime.Now)
            return BadRequest("No puedes agendar en el pasado");

        // Validación de negocio: evitar doble reserva
        var exists = await _context.Appointments
            .AnyAsync(a => a.Date == appointment.Date);

        if (exists)
            return BadRequest("Ya existe una cita en ese horario");

        appointment.Status = "Pending";

        _context.Appointments.Add(appointment);
        await _context.SaveChangesAsync();

        return Ok(appointment);
    }

    // PUT: api/appointments/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Appointment updated)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
            return NotFound("Cita no encontrada");

        // Solo actualizamos estado (regla de negocio simple)
        appointment.Status = updated.Status;

        await _context.SaveChangesAsync();

        return Ok(appointment);
    }

    // DELETE: api/appointments/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);

        if (appointment == null)
            return NotFound();

        _context.Appointments.Remove(appointment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}