using XMLSerialize.Data.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XMLSerialize.Data.DB
{
    internal class FileManager<T> : IFileManager<T>
    {
        public string XmlTeacher { get; set; } = "teacher.xml";
        public string XmlStudent { get; set; } = "student.xml";

        public void Write(List<T> models, string file)
        {
            FileStream stream = new FileStream(file, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            serializer.Serialize(stream, models);
            stream.Close();
        }

        public List<T> Read(string file)
        {
            using (FileStream stream = new FileStream(file, FileMode.OpenOrCreate))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
                return (List<T>)serializer.Deserialize(stream);
            }
        }
    }
}
