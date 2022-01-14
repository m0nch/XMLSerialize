using XMLSerialize.Data.DB;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLSerialize.Data.Models;

namespace XMLSerialize.Data
{
    public static class DataInjection
    {
        public static IServiceCollection AddData(this IServiceCollection service)
        {
            service.AddSingleton<IFileManager<Teacher>, FileManager<Teacher>>();
            service.AddSingleton<IFileManager<Student>, FileManager<Student>>();
            return service;
        }  
    }
}
