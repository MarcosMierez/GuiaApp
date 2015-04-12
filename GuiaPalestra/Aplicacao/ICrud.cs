using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuiaPalestrasOnline.Aplicacao
{
    public interface ICrud<T> where T:class 
    {
        void Save(T entidade);
        void Update(T entidade);
        void Delete(string Id);
        T GetByID(string Id);
        IEnumerable<T> GetAll();
    }
}
