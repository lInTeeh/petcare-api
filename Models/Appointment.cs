// Representa una cita dentro del sistema
public class Appointment
{
    public int Id { get; set; }

    // Nombre de la mascota
    public required string PetName { get; set; }

    // Dueño de la mascota
    public required string OwnerName { get; set; }

    // Fecha y hora de la cita
    public  DateTime Date { get; set; }

    // Tipo de servicio (baño, corte, etc.)
    public required string Service { get; set; }

    // Estado de la cita
    public required string Status { get; set; } // Pending, Completed, Cancelled
}