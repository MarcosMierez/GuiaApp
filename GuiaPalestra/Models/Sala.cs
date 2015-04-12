using GuiaPalestrasOnline.Models;

namespace GuiaPalestra.Models
{
    public class Sala:Entidade
    {
        public string NumeroSala { get; set; }
        public string Descricao { get; set; }
        public int Vagas { get; set; }
        public string EventoId { get; set; }
    }
}