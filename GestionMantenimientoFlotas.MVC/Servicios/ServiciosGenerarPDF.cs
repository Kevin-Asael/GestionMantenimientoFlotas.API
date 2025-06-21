using iTextSharp.text;
using iTextSharp.text.pdf;
using Flotas.Models;
using GestionFlota.API.Consumer;
using System.IO;

namespace GestionMantenimientoFlotas.MVC.Servicios
{
    public static class ServiciosGenerarPDF
    {
        public static byte[] GenerarPdfDeLogsYAlertas(Camion camion)
        {
            // Crear un documento PDF
            var doc = new Document();
            var memoryStream = new MemoryStream();
            var writer = PdfWriter.GetInstance(doc, memoryStream);
            doc.Open();

            // Título del PDF
            doc.Add(new Paragraph($"Reporte de Logs y Alertas para el Camión {camion.Placa}"));
            doc.Add(new Paragraph($"Kilometraje: {camion.KilometrajeActual} km"));
            doc.Add(new Paragraph($"Estado: {camion.Estado}"));
            doc.Add(new Paragraph($"Fecha de generación: {DateTime.UtcNow.ToString()}"));
            doc.Add(new Paragraph("\n"));

            // Añadir los logs de sensor
            doc.Add(new Paragraph("Logs de Sensor:"));
            var logs = Crud<SensorLog>.GetBy("Camion", camion.Id); // Obtener los logs asociados
            foreach (var log in logs)
            {
                doc.Add(new Paragraph($"Fecha: {log.Timestamp}, Kilometraje: {log.Kilometraje}, Estado del Motor: {log.EstadoMotor}"));
            }

            doc.Add(new Paragraph("\n"));

            // Añadir las alertas
            doc.Add(new Paragraph("Alertas:"));
            var alertas = Crud<AlertaLog>.GetBy("Camion", camion.Id); // Obtener las alertas asociadas
            foreach (var alerta in alertas)
            {
                doc.Add(new Paragraph($"Fecha: {alerta.Fecha}, Descripción: {alerta.Descripcion}, Estado: {(alerta.EstaResuelta ? "Resuelta" : "Pendiente")}"));
            }

            // Cerrar el documento PDF
            doc.Close();
            return memoryStream.ToArray();
        }
    }
}
