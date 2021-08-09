using Project1.Business.Models;

namespace Project1.Business
{
    public interface IClassManager
    {
        public ClassModel[] Classes { get; }

        public ClassModel Class(int ClassId);
    }
}