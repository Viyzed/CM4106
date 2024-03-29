﻿using Compiler.IO;
using Compiler.Nodes;
using Compiler.SemanticAnalysis;
using Compiler.SyntacticAnalysis;
using Compiler.Tokenization;
using System;
using System.Collections.Generic;
using System.IO;
using static System.Console;

namespace Compiler
{
    /// <summary>
    /// Compiler for code in a source file
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// The error reporter
        /// </summary>
        public ErrorReporter Reporter { get; }

        /// <summary>
        /// The file reader
        /// </summary>
        public IFileReader Reader { get; }

        /// <summary>
        /// The tokenizer
        /// </summary>
        public Tokenizer Tokenizer { get; }

        ///<summary>
        /// The parser
        /// </summary>
        public Parser Parser { get; }

        ///<summary>
        /// The idendifier
        /// </summary>
        public DeclarationIdentifier Identifier { get; }

        ///<summary>
        /// The type checker
        /// </summary>
        public TypeChecker Checker { get; }

        /// <summary>
        /// Creates a new compiler
        /// </summary>
        /// <param name="inputFile">The file containing the source code</param>
        public Compiler(string inputFile)
        {
            Reporter = new ErrorReporter();
            Reader = new FileReader(inputFile);
            Tokenizer = new Tokenizer(Reader, Reporter);
            Parser = new Parser(Reporter);
            Identifier = new DeclarationIdentifier(Reporter);
            Checker = new TypeChecker(Reporter);
        }

        /// <summary>
        /// Performs the compilation process
        /// </summary>
        public void Compile()
        {
            // Tokenize
            Write("Tokenising...");
            List<Token> tokens = Tokenizer.GetAllTokens();
            if (Reporter.HasErrors) return;
            WriteLine("Done");

            //WriteLine(string.Join("\n", tokens));

            // Parse 
            Write("Parsing...");
            ProgramNode tree = Parser.Parse(tokens);
            if (Reporter.HasErrors) return;
            WriteLine("Done.");

            //Display the Abstract Syntax Tree
            WriteLine(TreePrinter.ToString(tree));

            //Identify
            Write("Identifying...");
            Identifier.PerformIdentification(tree);
            if (Reporter.HasErrors) return;
            WriteLine("Done");

            //Type check
            Write("Type Checking...");
            if (Reporter.HasErrors) return;
            WriteLine("Done");

            WriteLine(TreePrinter.ToString(tree));
        }

        /// <summary>
        /// Writes a message reporting on the success of compilation
        /// </summary>
        private void WriteFinalMessage()
        {
            // Write output to tell the user whether it worked or not here
            Write("Final Messege.");
        }

        /// <summary>
        /// Compiles the code in a file
        /// </summary>
        /// <param name="args">Should be one argument, the input file (*.tri)</param>
        public static void Main(string[] args)
        {
            if (args == null || args.Length != 1 || args[0] == null)
                WriteLine("ERROR: Must call the program with exactly one argument, the input file (*.tri)");
            else if (!File.Exists(args[0]))
                WriteLine($"ERROR: The input file \"{Path.GetFullPath(args[0])}\" does not exist");
            else
            {
                string inputFile = args[0];
                Compiler compiler = new Compiler(inputFile);
                WriteLine("Compiling...");
                compiler.Compile();
                compiler.WriteFinalMessage();
            }
        }
    }
}
