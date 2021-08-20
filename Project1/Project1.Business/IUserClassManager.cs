using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Business
{
    public interface IUserClassManager
    {
        public void RegisterForClass(int UserId, int ClassId);
    }
}
