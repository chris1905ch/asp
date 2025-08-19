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
                    Pregunta = "�C�mo agrego una nueva reserva?",
                    Respuesta = "Para agregar una nueva reserva, haz clic en el men� 'Reservas' y luego en 'Agregar Reserva'. Completa el formulario y guarda los cambios.",
                    Etiquetas = "reserva,agregar,nueva"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "�C�mo edito o elimino una habitaci�n?",
                    Respuesta = "Ve al men� 'Habitaciones', selecciona la habitaci�n y elige 'Editar' o 'Eliminar' seg�n lo que necesites.",
                    Etiquetas = "habitacion,editar,eliminar"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "�C�mo se asignan servicios extras a una reserva?",
                    Respuesta = "Al crear o editar una reserva, puedes seleccionar los servicios extras disponibles para asignarlos.",
                    Etiquetas = "servicios extra,asignar,reserva"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "�Qu� hago si olvido mi contrase�a?",
                    Respuesta = "Contacta al administrador del sistema para restablecer tu contrase�a.",
                    Etiquetas = "contrase�a,olvidar,recuperar"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "�C�mo genero un reporte de ingresos o de ocupaci�n?",
                    Respuesta = "Accede al 'Dashboard' y utiliza las opciones de exportar o visualizar reportes.",
                    Etiquetas = "reporte,ingresos,ocupacion"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "�C�mo cierro sesi�n de forma segura?",
                    Respuesta = "Haz clic en el bot�n 'Salir' en la esquina superior derecha para cerrar tu sesi�n de manera segura.",
                    Etiquetas = "cerrar sesi�n,salir,logout"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "�Qu� significan los diferentes estados de una reserva?",
                    Respuesta = "Los estados pueden ser: Confirmada, Pendiente, Cancelada o Finalizada. Cada uno indica el progreso de la reserva.",
                    Etiquetas = "estados,reserva,significado"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "�C�mo agrego un nuevo cliente o trabajador?",
                    Respuesta = "Desde el men� 'Clientes' o 'Trabajadores', haz clic en 'Agregar' y completa el formulario correspondiente.",
                    Etiquetas = "cliente,trabajador,agregar"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "�C�mo se calculan los montos estimados y pagados?",
                    Respuesta = "El monto estimado se calcula seg�n el precio de la habitaci�n y servicios extras seleccionados. El monto pagado es el total abonado por el cliente.",
                    Etiquetas = "monto,estimado,pagado,calculo"
                });
                this.FAQ.Add(new FAQ {
                    Pregunta = "�A qui�n contacto si tengo problemas con el sistema?",
                    Respuesta = "Comun�cate con el administrador o soporte t�cnico del hotel para resolver cualquier inconveniente.",
                    Etiquetas = "soporte,problemas,contacto"
                });
                this.SaveChanges();
            }
        }
    }
}
