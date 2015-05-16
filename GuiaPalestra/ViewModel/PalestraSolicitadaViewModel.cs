using System;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GuiaPalestra.ViewModel
{
    public class PalestraSolicitadaViewModel
    {
        public string Titulo { get; set; }
        public string PalestraId { get; set; }
        public string Local { get; set; }
        public string EventoId { get; set; }
        public string CoordenadorId { get; set; }
        public string PalestranteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Pendencia { get; set; }
        public string TrilhaId  { get; set; }
        public string  NomeTrilha { get; set; }
        public string SalaId { get; set; }
        public string NumeroSala { get; set; }
        public DateTime HoraInicial { get; set; }
        public DateTime HoraFinal { get; set; }
        public int Vagas { get; set; }
        public DateTime Dia { get; set; }
    }
}