using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CatchFishClient
{
    class Program
    {
        static void Main(string[] args)
        {

            RestCalls calls = new RestCalls();
            Console.WriteLine(calls.Read(2));
            Console.ReadLine();
        }
    }
}
