using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sprache;

namespace ChystLabs
{
	class Program
	{
        public static void Main(string[] args)
        {
            while (true)
            {
                System.Math.Pow(3, 6);
                string str = "sqrt(x + y)";
                FunctionParser parser = new FunctionParser(str);

                var f = parser.ToFunc<double, double, double>("x", "y");
                var x = f(3, 6);
            }
        }
    }
}
