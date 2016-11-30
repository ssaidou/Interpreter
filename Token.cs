using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication13
{
    public class Token
    {
        public string Type { get; set; }
        public string Value { get; set; }

        //public Token ShallowCopy()
        //{
        //    return (Token)this.MemberwiseClone();
        //}

        public Token(string Type, string Value)
        {
            this.Type = Type;
            this.Value = Value;
        }

        public override string ToString()
        {
            //return base.ToString() + String.Format("Token({0}, {1})", Type, Value);
            return String.Format("Token({0}, {1})", Type, Value);

        }
    }
}
