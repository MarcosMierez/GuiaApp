using System;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.ViewModel
{
    public class PalestraViewModel
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public DateTime Duracao { get; set; }
        public string PalestranteId { get; set; }
        public string SalaId { get; set; }
        public string TrilhaId { get; set; }
        public string NomePalestrante { get; set; }
        public string NumeroSala { get; set; }
        public string NomeTrilha { get; set; }
        public bool Inscrito { get; set; }
    }
}