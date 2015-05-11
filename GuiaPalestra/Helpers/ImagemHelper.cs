using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.Helpers;

namespace GuiaPalestra.Helpers
{
    public class ImagemHelper
    {
        public static string Upload(HttpPostedFileBase arquivo, string diretorio)
        {
            var nomeUnico = "";
            if (arquivo == null) return nomeUnico;

            var extensaoDoArquivo = Path.GetExtension(arquivo.FileName).Substring(1);

            nomeUnico = Guid.NewGuid() + "." + extensaoDoArquivo;

            var path = MontaPath(diretorio, nomeUnico);

            var imagem = new WebImage(arquivo.InputStream);
            imagem.Resize(100, 100);
            imagem.Save(path);

            return nomeUnico;
        }
        private static string MontaPath(string diretorio, string fileName)
        {
            var pathDiretorio = HttpContext.Current.Server.MapPath("~/Content/Uploads/" + diretorio);
            var dir = new DirectoryInfo(pathDiretorio);

            if (!dir.Exists)
                Directory.CreateDirectory(pathDiretorio);

            return Path.Combine(pathDiretorio, fileName);
        }
        public static void ExcluirArquivo(string nome, string diretorio)
        {
            if (string.IsNullOrEmpty(nome))
                return;
            var arquivos = Directory.GetFiles(MontaPath(diretorio, ""));
            foreach (var fi in arquivos.Select(arquivo => new FileInfo(arquivo)).Where(fi => fi.Name.Contains(nome)))
            {
                fi.Delete();
            }
        }
        public static bool validate(HttpPostedFileBase arquivo)
        {
            var extenssoesPermitidas = new[] { "jpg", "jpeg", "png" };
            var extenssaodoArquivo = Path.GetExtension(arquivo.FileName).Substring(1);
            if (!extenssoesPermitidas.Contains(extenssaodoArquivo.ToLower()))
                return false;
            if (arquivo.ContentLength > (1024 * 1024))
                return false;
            return true;
        }
    }
}