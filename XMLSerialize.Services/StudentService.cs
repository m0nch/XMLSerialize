using XMLSerialize.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using XMLSerialize.Data.DB;
using System.Linq;

namespace XMLSerialize.Services
{
    internal class StudentService : IStudentService
    {
        private readonly IFileManager<Student> _fileManager;
        public StudentService(IFileManager<Student> fileManager)
        {
            _fileManager = fileManager;
        }

        public void Add(Student model)
        {
            List<Student> students = _fileManager.Read(_fileManager.XmlStudent);
            students.Add(model);
            _fileManager.Write(students, _fileManager.XmlStudent);
        }

        public Student Get(Guid id)
        {
            return _fileManager.Read(_fileManager.XmlStudent).FirstOrDefault(x => x.Id == id) as Student;
        }

        public List<Student> GetAll()
        {
            return _fileManager.Read(_fileManager.XmlStudent);
        }

        public List<Student> GetAllByTeacher(Guid id)
        {
            string _id = id.ToString();
            return _fileManager.Read(_fileManager.XmlStudent).FindAll(x => x.TeacherId == id);
        }

        public void Remove(Guid id)
        {
            List<Student> students = _fileManager.Read(_fileManager.XmlStudent);
            Student student = students.FirstOrDefault(x => x.Id == id);
            students.Remove(student);
            _fileManager.Write(students, _fileManager.XmlStudent);
        }

        public void Update(Student model)
        {
            List<Student> students = _fileManager.Read(_fileManager.XmlStudent);
            Student student = students.FirstOrDefault(t => t.Id == model.Id);
            int index = students.IndexOf(student);
            students[index] = model;
            _fileManager.Write(students, _fileManager.XmlStudent);
        }
    }
}
