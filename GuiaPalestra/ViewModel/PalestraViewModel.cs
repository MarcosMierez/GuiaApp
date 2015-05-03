using System;
using System.Security.AccessControl;

namespace GuiaPalestra.ViewModel
{
    public class PalestraViewModel
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public DateTime HorarioInicial{ get; set; }
        public DateTime HorarioFinal { get; set; }
        public string PalestranteId { get; set; }
        public string SalaId { get; set; }
        public string TrilhaId { get; set; }
        public int Vagas { get; set; }
        public string NomePalestrante { get; set; }
        public string NumeroSala { get; set; }
        public string NomeTrilha { get; set; }
        public bool Inscrito { get; set; }
        public string EventoId { get; set; }
        public DateTime Dia { get; set; }
        public bool Status { get; set; }
    }
}