using XMLSerialize.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerialize.Services
{
    public interface ITeacherService
    {
        void Add(Teacher model);
        void Remove(Guid id);
        void Update(Teacher model);
        Teacher Get(Guid id);
        List<Teacher> GetAll();
    }
}
