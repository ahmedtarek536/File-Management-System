using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace File_Management_System
{
    public static class AnaylseToken 
    {
        public static Expression GetTokens(string input)
        {
            input = input.Trim();
            Expression expr = new Expression();
            bool ActionComplete = false;
            bool FirstTokenComplete = false;
            string token = "";
            foreach (char val in input)
            {
                if (char.IsLetter(val) && !ActionComplete)
                {
                    token += val;
                }
                else if (val == ' ' && !ActionComplete)
                {
                    expr.Action = DetermineAction(token);
                    ActionComplete = true;
                    token = "";
                }
                else if(val == '"' && !FirstTokenComplete)
                {
                    expr.FirstToken = token.Trim();
                    token = "";
                    FirstTokenComplete = true;
                }
                else if (val == '"' && FirstTokenComplete)
                {
                    expr.SecondToken = token.Trim();
                    token = "";

                }
                else
                        {
                    token += val;
                }
            }
            if(!ActionComplete) expr.Action = DetermineAction(token.Trim());
            else if(!FirstTokenComplete) expr.FirstToken = token.Trim();
            return expr;
        }
        private static Operation DetermineAction(string token)
        {
            switch (token) {
                case "list":
                case "dir":
                    return Operation.List;
                case "mkdir":
                    return Operation.CreateDirectory;
                case "mkfile":
                    return Operation.CreateFile;
                case "remove":
                case "delete":
                    return Operation.Remove;
                case "view":
                case "print":
                    return Operation.View;
                case "info":
                    return Operation.Info;
                case "write":
                    return Operation.Write;
                case "append":
                    return Operation.Append;
                case "cd":
                    return Operation.Move;
                case "rename":
                    return Operation.Rename;
                case "mv":
                    return Operation.Copy;
                case "exit":
                    return Operation.Exit;
                default:
                    return Operation.Non;
            }
        }
    }
}
