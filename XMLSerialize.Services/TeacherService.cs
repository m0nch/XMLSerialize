using XMLSerialize.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using XMLSerialize.Data.DB;
using System.Linq;

namespace XMLSerialize.Services
{
    internal class TeacherService : ITeacherService
    {
        private const string XmlTeacher = "teacher.xml";

        private readonly IFileManager<Teacher> _fileManager;

        public TeacherService(IFileManager<Teacher> fileManager)
        {
            _fileManager = fileManager;
        }
        public void Add(Teacher model)
        {
            List<Teacher> teachers = _fileManager.Read(XmlTeacher);
            teachers.Add(model);
            _fileManager.Write(teachers, XmlTeacher);
        }
        public Teacher Get(Guid id)
        {
            Teacher teacher = _fileManager.Read(XmlTeacher).FirstOrDefault(x => x.Id == id);
            return teacher;
        }
        public List<Teacher> GetAll()
        {
            return _fileManager.Read(XmlTeacher);
        }
        public void Remove(Guid id)
        {
            List<Teacher> teachers = _fileManager.Read(XmlTeacher);
            Teacher teacher = teachers.FirstOrDefault(x => x.Id == id);
            teachers.Remove(teacher);
            _fileManager.Write(teachers, XmlTeacher);
        }
        public void Update(Teacher model)
        {
            List<Teacher> teachers = _fileManager.Read(XmlTeacher);
            Teacher teacher = teachers.FirstOrDefault(t => t.Id == model.Id);
            int index = teachers.IndexOf(teacher);
            teachers[index] = model;
            _fileManager.Write(teachers, XmlTeacher);
        }
    }
}
