using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Repository.Models;

namespace Project1.Repository
{
    public class ClassRepository : IClassRepository
    {
        public Class[] GetAllClasses()
        {
            Class[] classes = DatabaseAccessor.Instance.Classes
                .Select(t => new Class
                {
                    ClassId = t.ClassId,
                    ClassDescription = t.ClassDescription,
                    ClassName = t.ClassName,
                    ClassPrice = t.ClassPrice
                })
                .ToArray<Class>();

            return classes;
        }

        public Class GetClass(int ClassId)
        {
            Class classes = (Class)DatabaseAccessor.Instance.Classes
                .Where(t => t.ClassId == ClassId)
                .Select(t => new Class
                {
                    ClassId = t.ClassId,
                    ClassDescription = t.ClassDescription,
                    ClassName = t.ClassName,
                    ClassPrice = t.ClassPrice
                });

            return classes;
        }
    }
}
