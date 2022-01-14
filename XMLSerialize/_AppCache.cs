using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerialize
{
    public class _AppCache : _IAppCache
    {
        public Dictionary<string, object> _ViewBag { get; set; }

        public _AppCache()
        {
            _ViewBag = new Dictionary<string, object>();    
        }
    }    

    public interface _IAppCache
    {
        Dictionary<string, object> _ViewBag { get; set; }
    }
}
