using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Database;

namespace Project1.Repository
{
    class DatabaseAccessor
    {
        public static minicstructorContext Instance { get; private set; }

        static DatabaseAccessor()
        {
            Instance = new minicstructorContext();
        }
    }
}
