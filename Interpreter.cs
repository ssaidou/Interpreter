using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Text.RegularExpressions;

namespace ConsoleApplication13
{
    class Interpreter
    {
        public Interpreter()
        {
            this.position = 0;
            this.currentToken = null;
        }

        public double? input(string input)
        {
            List<string> tokens = tokenize(input);
            this.tokens = tokens;
            
            return null;
        }

        private List<string> tokenize(string input)
        {
            input = input + ")";
            List<string> tokens = new List<string>();
            Regex rgxMain = new Regex("=>|[-+*/%=\\(\\)]|[A-Za-z_][A-Za-z0-9_]*|[0-9]*(\\.?[0-9]+)");
            MatchCollection matches = rgxMain.Matches(input);
            foreach (Match m in matches)
            {
                tokens.Add(m.Groups[0].Value);
                //Console.WriteLine(m.Groups[0].Value);
            }

            return tokens;
        }

        public Token Tokens {
            get
            {
                string value;
                if (this.position < this.tokens.Count - 1)
                {
                    value = this.tokens[this.position++];
                    if (value.All(char.IsDigit))
                    {
                        return new Token("INTEGER", value);
                    }
                    else if (value.Equals("+"))
                    {
                        return new Token("PLUS", value);
                    }
                    else if (value.Equals("-"))
                    {
                        return new Token("MINUS", value);
                    }
                    else if (value.Equals("*"))
                    {
                        return new Token("MUL", value);
                    }
                    else if (value.Equals("("))
                    {
                        return new Token("LPAREN", value);
                    }
                    else if (value.Equals(")"))
                    {
                        return new Token("RPAREN", value);
                    }
                    else
                    {
                        return new Token("DIV", value);
                    }
                }   
                else
                    return null;
            }
        }


        public int Factor
        {
            get
            {
                Token token = null;
                int j;

                token = this.Tokens;

                if (token == null)
                    return 0;

                if (!(Int32.TryParse(token.Value, out j)))
                {
                    if(token.Type == "LPAREN")
                    {
                        j = this.Expr;
                        this.currentToken = this.Tokens;
                        return j;
                    }

                }

                this.currentToken = this.Tokens;

                return j;
            }
        }

        public int Term
        {
            get
            {
                int result = 0;

                result = this.Factor;

                if (this.currentToken == null)
                    return result;

                while (this.currentToken.Type.Equals("MUL") || this.currentToken.Type.Equals("DIV"))
                {
                    if (this.currentToken.Value.Equals("*"))
                    {
                        result *= this.Factor;
                    }
                    else if (this.currentToken.Value.Equals("/"))
                    {
                        result /= this.Factor;
                    }
                    if (this.currentToken == null)
                        break;
                }

                return result;
            }
        }

        public int Expr
        {
            get
            {
                int result = 0;

                result = this.Term;

                if (this.currentToken == null)
                    return result;

                while (this.currentToken.Type.Equals("PLUS") || this.currentToken.Type.Equals("MINUS"))
                {
                    if (this.currentToken.Value.Equals("+"))
                    {
                        result += this.Term;
                    }
                    else if (this.currentToken.Value.Equals("-"))
                    {
                        result -= this.Term;
                    }
                    if (this.currentToken == null)
                        break;
                }

                return result;
            }
        }

        private List<string> tokens;
        private Token currentToken = null;
        private int position = 0;
    }

    public class BinOp
    {
        public BinOp(Token left, Token op, Token right)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}", this.left.ToString(), this.op.ToString(), this.right.ToString());
        }

        private Token left, op, right;
    }

    public class Num
    {
        public Num(Token token)
        {
            this.token = token;
        }

        public override string ToString()
        {
            return this.token.ToString();
        }

        Token token;
    }


}
