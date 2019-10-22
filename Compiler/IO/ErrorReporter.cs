using Compiler.Nodes;
using Compiler.IO;
using System;
using System.Collections.Generic;

namespace Compiler.IO
{
    /// <summary>
    /// An object for reporting errors in the compilation process
    /// </summary>
    public class ErrorReporter
    {
        /// <summary>
        /// Whether or not any errors have been encountered
        /// </summary>
        public bool HasErrors { get; set; }

        private List<ErrorNode> errors = new List<ErrorNode>();

        public void ReportError(ErrorNode error, string message)
        {
            Console.WriteLine("Error found at " + error.ToString());

        }

}

}