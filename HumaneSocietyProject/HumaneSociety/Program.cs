using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    class Program
    {
        static void Main(string[] args)
        {
            Animal test = new Animal();
            test.Name = "TEST";
            Query.AddAnimal(test);
            // PointOfEntry.Run();
        }
    }
}
