using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaPalestrasOnline.ViewModel
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
    }
}