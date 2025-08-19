using System.Data.Entity;
using System.Linq;
using SistemaHotel.Models;

namespace SistemaHotel.Models
{
    public partial class HotelDBEntities1
    {
        public void SeedFAQ()
        {
            if (!this.FAQ.Any())
            {
                this.FAQ.Add(new FAQ {
                    Pregunta = "¿Cómo agrego una nueva reserva?",
                    Respuesta = "Para agregar una nueva reserva, haz clic en el menú 'Reservas' y luego en 'Agregar Reserva'. Completa el formulario y guarda los cambios.",
                    Etiquetas = "reserva,agregar,nueva"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "¿Cómo edito o elimino una habitación?",
                    Respuesta = "Ve al menú 'Habitaciones', selecciona la habitación y elige 'Editar' o 'Eliminar' según lo que necesites.",
                    Etiquetas = "habitacion,editar,eliminar"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "¿Cómo se asignan servicios extras a una reserva?",
                    Respuesta = "Al crear o editar una reserva, puedes seleccionar los servicios extras disponibles para asignarlos.",
                    Etiquetas = "servicios extra,asignar,reserva"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "¿Qué hago si olvido mi contraseña?",
                    Respuesta = "Contacta al administrador del sistema para restablecer tu contraseña.",
                    Etiquetas = "contraseña,olvidar,recuperar"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "¿Cómo genero un reporte de ingresos o de ocupación?",
                    Respuesta = "Accede al 'Dashboard' y utiliza las opciones de exportar o visualizar reportes.",
                    Etiquetas = "reporte,ingresos,ocupacion"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "¿Cómo cierro sesión de forma segura?",
                    Respuesta = "Haz clic en el botón 'Salir' en la esquina superior derecha para cerrar tu sesión de manera segura.",
                    Etiquetas = "cerrar sesión,salir,logout"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "¿Qué significan los diferentes estados de una reserva?",
                    Respuesta = "Los estados pueden ser: Confirmada, Pendiente, Cancelada o Finalizada. Cada uno indica el progreso de la reserva.",
                    Etiquetas = "estados,reserva,significado"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "¿Cómo agrego un nuevo cliente o trabajador?",
                    Respuesta = "Desde el menú 'Clientes' o 'Trabajadores', haz clic en 'Agregar' y completa el formulario correspondiente.",
                    Etiquetas = "cliente,trabajador,agregar"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "¿Cómo se calculan los montos estimados y pagados?",
                    Respuesta = "El monto estimado se calcula según el precio de la habitación y servicios extras seleccionados. El monto pagado es el total abonado por el cliente.",
                    Etiquetas = "monto,estimado,pagado,calculo"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "¿A quién contacto si tengo problemas con el sistema?",
                    Respuesta = "Comunícate con el administrador o soporte técnico del hotel para resolver cualquier inconveniente.",
                    Etiquetas = "soporte,problemas,contacto"
                });
                this.SaveChanges();
            }
        }
    }
}
