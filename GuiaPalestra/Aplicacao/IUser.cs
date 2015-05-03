using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuiaPalestra.Models;
using GuiaPalestrasOnline.Models;

namespace GuiaPalestrasOnline.Aplicacao
{
    interface IUser
    {
        Usuario Logar(string email, string senha);
    }
}
