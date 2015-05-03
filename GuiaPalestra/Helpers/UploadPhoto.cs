using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuiaPalestra.Helpers
{
    public class UploadPhoto
    {
        public static string UploadPhotos(HttpPostedFileBase photo, string diretorio, string photoBD)
        {
            if (ImagemHelper.validate(photo) == true && photo != null)
            {
                string photoPath = ImagemHelper.Upload(photo, diretorio);
                ImagemHelper.ExcluirArquivo(photoBD, diretorio);
                return photoPath;
            }
            return "GenericPhoto";
        }
    }
}