using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Business.Models;
using Project1.Repository.Models;

namespace Project1.Business
{
    public class ClassManager : IClassManager
    {
        private readonly IClassRepository classRepository;

        public ClassManager(IClassRepository Repository)
        {
            classRepository = Repository;
        }

        public ClassModel[] Classes
        {
            get
            {
                return classRepository.GetAllClasses()
                    .Select(c => new ClassModel()
                    {
                        ClassId = c.ClassId,
                        ClassDescription = c.ClassDescription,
                        ClassName = c.ClassName,
                        ClassPrice = c.ClassPrice
                    })
                    .ToArray();
            }
        }

        public ClassModel Class(int ClassId)
        {
            var classRP = classRepository.GetClass(ClassId);

            return new ClassModel()
            {
                ClassId = classRP.ClassId,
                ClassDescription = classRP.ClassDescription,
                ClassName = classRP.ClassName,
                ClassPrice = classRP.ClassPrice
            };
        }
    }
}
