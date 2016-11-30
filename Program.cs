using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication13
{
    class Program
    {
        static void Main(string[] args)
        {
            Interpreter interpret = new Interpreter();
            //check(ref interpret, "7 + (((3 + 2)))", 2);
            //Console.WriteLine("interpret.Expr");
            //Console.WriteLine(interpret.Expr);

            Num num = new Num(new Token("INTEGER", "0123"));
            BinOp binOp = new BinOp(new Token("integer", "0123"), new Token("PLUS", "+"), new Token("INTEGER", "4567"));
            Console.WriteLine(num);
            Console.WriteLine(binOp);
            //Console.WriteLine(interpret.Tokens);
            //Console.WriteLine(interpret.Tokens);
            //Console.WriteLine(interpret.Tokens);
            //
            //Console.WriteLine(">>" + interpret.Tokens);
            //Console.WriteLine(">>" + interpret.Tokens);
            //Console.WriteLine(">>" + interpret.Tokens);
            //Console.WriteLine(">>" + interpret.Tokens);
            //Console.WriteLine(">>" + interpret.Tokens);
            //Console.WriteLine(">>" + interpret.Tokens);
            //Console.WriteLine(">>" + interpret.Tokens);
            //Console.WriteLine(">>" + interpret.Tokens);

            //Token t = new Token("djdjdjd", "dddddd");
            //Console.WriteLine(t);



        }

        private static void check(ref Interpreter interpret, string inp, double? res)
        {
            double? result = -9999.99;
            try { 
                result = interpret.input(inp);
            }
            catch (Exception) 
            { 
                result = null; 
            }
        }
    }
}
