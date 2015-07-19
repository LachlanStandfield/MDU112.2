using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDU112_Framework
{
    class Terminal
    {
        static void Main(string[] args)
        {
            Console.WindowTop = 0;
            Console.WindowWidth = 120;
            Console.SetWindowSize(120, 50);
            //Create a new instance of the program class
            Program program = new Program();

            //Run the program
            program.Run();

            //Instead of terminating the programing immediately, wait for a key press to exit
            Console.WriteLine("\r\nProgram terminated. Press any key to exit.");
            Console.ReadKey();
           

        }
    }
}
