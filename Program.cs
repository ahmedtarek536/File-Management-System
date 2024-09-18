using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var CurrentPath = Path.GetFullPath(Directory.GetDirectoryRoot(Directory.GetCurrentDirectory()));
            // CurrentPath = "D:\";
            while (true)
            {
                Console.WriteLine();
                Console.Write($"{CurrentPath}>> ");
                var input = Console.ReadLine().Trim();
                Expression expr = AnaylseToken.GetTokens(input);
               
                
               // Console.WriteLine($"Action: {expr.Action},  FirstToken: {expr.FirstToken},  SecondToken: {expr.SecondToken}");

                var path = expr.FirstToken == null ? CurrentPath : Path.GetFullPath(Path.Combine(CurrentPath, expr.FirstToken));

                if (expr.Action == Operation.List)
                {
                    foreach (var entry in Directory.GetDirectories(CurrentPath))
                        Console.WriteLine($"\t[Dir] {entry}");
                    foreach (var entry in Directory.GetFiles(CurrentPath))
                        Console.WriteLine($"\t[File] {entry}");

                }
                else if (expr.Action == Operation.View)
                {
                    
                    if(File.Exists(path))
                    {
                      var content = File.ReadAllText(path);
                      Console.ForegroundColor = ConsoleColor.Green;
                      Console.WriteLine(content);
                      Console.ForegroundColor = ConsoleColor.White;

                    }else Console.WriteLine("you can only show view for files and enter valid path");
                }
                else if (expr.Action == Operation.Info)
                {
                    
                    Console.WriteLine();
                    if (Directory.Exists(path))
                    {
                        var info = new DirectoryInfo(path);
                        Console.WriteLine("Name: " + info.Name);
                        Console.WriteLine("Type: Directory");
                        Console.WriteLine($"Create At {info.CreationTime}");
                        Console.WriteLine($"Modified At {info.LastWriteTime}");
                    }
                    else if (File.Exists(path))
                    {
                        var info = new FileInfo(path);
                        Console.WriteLine("Name: " + info.Name);
                        Console.WriteLine("Type: Directory");
                        Console.WriteLine($"Create At {info.CreationTime}");
                        Console.WriteLine($"Modified At {info.LastWriteTime}");
                        Console.WriteLine($"Size in {info.Length}");
                    }

                }
                else if(expr.Action == Operation.CreateDirectory)
                {
                    if (expr.FirstToken == null) { Console.WriteLine("please enter path for your files"); }
                    else
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                else if (expr.Action == Operation.CreateFile)
                {
                    if(expr.FirstToken== null) { Console.WriteLine("please enter path for your files"); }
                    else
                    {
                      var file = File.Create(path);
                      file.Close();
                    }

                }
                else if (expr.Action == Operation.Remove)
                {
                    if(expr.FirstToken== null) { Console.WriteLine("please enter path for your files"); }
                    else
                    {
                       
                       if (Directory.Exists(path)) Directory.Delete(path , true);
                       else if (File.Exists(path))
                       {
                           File.Delete(path);
                       }
                    }
                }
                else if (expr.Action == Operation.Write)
                {
                    if(expr.SecondToken == null || expr.FirstToken == null ) Console.WriteLine("not vaild code");
                    else File.WriteAllText(path, expr.SecondToken);
                }
                else if (expr.Action == Operation.Append)
                {
                    if (expr.SecondToken == null || expr.FirstToken == null) Console.WriteLine("not vaild code");
                    else File.AppendAllText(path, expr.SecondToken);
                }
                else if (expr.Action == Operation.Move)
                {
                    
                    if (expr.FirstToken != null && Directory.Exists(Path.GetFullPath(Path.Combine(CurrentPath, expr.FirstToken))))
                    {
                        CurrentPath = Path.GetFullPath(Path.Combine(CurrentPath, expr.FirstToken));
                    }
                    else Console.WriteLine("Error: This path not found");
                }
                else if (expr.Action == Operation.Exit) break;

            }


            Console.ReadLine();
        }
    }
}
