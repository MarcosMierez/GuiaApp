using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaPalestrasOnline.Models
{
    public class Entidade
    {
        private string id;
        public string ID
        {
            get
            {
                if (string.IsNullOrEmpty(id))
                {
                    id = Guid.NewGuid().ToString("N");
                    return id;
                }
                return id;
            }
            set { id = value; }
        }
    }

}