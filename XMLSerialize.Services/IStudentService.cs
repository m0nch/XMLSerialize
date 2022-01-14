using XMLSerialize.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLSerialize.Services
{
    public interface IStudentService
    {
        void Add(Student model);
        void Remove(Guid id);
        void Update(Student model);
        Student Get(Guid id);
        List<Student> GetAll();
        List<Student> GetAllByTeacher(Guid id);
    }
}
