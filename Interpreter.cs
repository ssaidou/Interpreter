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
            dict = new Dictionary<string, AST>();
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


        public AST Factor
        {
            get
            {
                Token token = null;
                AST node;
                int j;

                token = this.Tokens;

                if (token == null)
                    return new AST();

                if (!(Int32.TryParse(token.Value, out j)))
                {
                    if(token.Type == "LPAREN")
                    {
                        node = this.Expr;
                        this.currentToken = this.Tokens;
                        return node;
                    }

                }

                this.currentToken = this.Tokens;

                return new Num(token);
            }
        }

        public AST Term
        {
            get
            {
                AST node = null;

                node = this.Factor;

                if (this.currentToken == null)
                    return node;

                while (this.currentToken.Type.Equals("MUL") || this.currentToken.Type.Equals("DIV"))
                {
                    node = new BinOp(node, this.currentToken, this.Factor);
                    if (this.currentToken == null)
                        break;
                }

                return node;
            }
        }

        public AST Expr
        {
            get
            {
                AST node = null;

                node = this.Term;

                if (this.currentToken == null)
                    return node;

                while (this.currentToken.Type.Equals("PLUS") || this.currentToken.Type.Equals("MINUS"))
                {
                    node = new BinOp(node, this.currentToken, this.Term);

                    if (this.currentToken == null)
                        break;
                }

                return node;
            }
        }


        public int interpret { 
            get
            {
                AST exp = this.Expr;

                return this.visit(exp);
            }
        }

        public int visit(AST node)
        {

            if(node.GetType().Name == "BinOp")
            {
                BinOp d = node as BinOp;
                if (d != null)
                {
                    if (d.Op.Type.Equals("PLUS"))
                    {
                        return this.visit(d.Left) + this.visit(d.Right);
                    }
                    else if (d.Op.Type.Equals("MINUS"))
                    {
                        return this.visit(d.Left) - this.visit(d.Right);
                    }
                    else if (d.Op.Type.Equals("MUL"))
                    {
                        return this.visit(d.Left) * this.visit(d.Right);
                    }
                    else if (d.Op.Type.Equals("DIV"))
                    {
                        return this.visit(d.Left) / this.visit(d.Right);
                    }
                }
            }
            else if (node.GetType().Name == "Num")
            {
                Num d = node as Num;
                if (d != null)
                {
                    return Convert.ToInt32(d.Value);
                }
            }
            else if (node.GetType().Name == "Assign")
            {
                Assign d = node as Assign;
                if (d != null)
                {
                    dict.Add(d.Left.Value, d.Right);
                }
            }
            else if (node.GetType().Name == "Var")
            {
                Var d = node as Var;
                if (d != null)
                {
                    return 15;
                }
            }

            return 0;
        }

        private List<string> tokens;
        private Token currentToken;
        private int position;
        private Dictionary<string, AST> dict;
    }

    public class AST
    {

    }

    public class BinOp: AST
    {
        public BinOp(AST Left, Token Op, AST Right)
        {
            this.Left = Left;
            this.Op = this.Token = Op;
            this.Right = Right;
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}\n" , this.Left.ToString(), this.Op.ToString(), this.Right.ToString());
        }

        public AST Left { get; set; }
        public AST Right { get; set; }
        public Token Op { get; set; }
        public Token Token { get; set; }
    }

    public class Num: AST
    {
        public Num(Token token)
        {
            this.Token = token;
            this.Value = token.Value;
        }

        public override string ToString()
        {
            return this.Token.ToString() + "\n";
        }

        public Token Token { get; set; }
        public string Value { get; set; }
    }

    public class Var : AST
    {
        public Var(Token token)
        {
            this.Token = token;
            this.Value = token.Value;
        }

        public override string ToString()
        {
            return this.Token.ToString() + "\n";
        }

        public Token Token { get; set; }
        public string Value { get; set; }
    }


    public class Assign : AST
    {
        public Assign(Var Left, Token Op, AST Right)
        {
            this.Left = Left;
            this.Op = this.Token = Op;
            this.Right = Right;
        }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}\n", this.Left.ToString(), this.Op.ToString(), this.Right.ToString());
        }

        public Var Left { get; set; }
        public AST Right { get; set; }
        public Token Op { get; set; }
        public Token Token { get; set; }
    }
}
