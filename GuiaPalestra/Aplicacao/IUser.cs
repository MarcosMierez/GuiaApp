using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Aplicacao
{
    interface IUser
    {
        Usuario Logar(string email, string senha);
        void ParticiparPalestra(string usuarioId, string palestraId,string eventoId,string status);
        void DesistirPalestra(string usuarioId, string palestraId);
    }
}
