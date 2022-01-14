using XMLSerialize.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerialize.Data.DB
{
    public interface IFileManager<T>
    {
        void Write(List<T> model, string file);
        List<T> Read(string file);
    }
}
