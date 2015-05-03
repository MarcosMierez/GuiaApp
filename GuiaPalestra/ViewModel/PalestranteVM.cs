﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaPalestra.ViewModel
{
    public class PalestranteVM
    {
        public string ID { get; set; }
        public string Nome { get; set; }
        public HttpPostedFileBase Photo { get; set; }
        public string PhotoPath { get; set; }
        public string Email { get; set; }
        public string TwitterPalestrante { get; set; }
    }
}