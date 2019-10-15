namespace Compiler
{
    /// <summary>
    /// A position in a file
    /// </summary>
    public class Position
    {

        int lineNo { get; } = 0;
        int position { get; } = 0;

        public Position(int lineNo, int position)
        {
            this.lineNo = lineNo;
            this.position = position;
        }

        public override string ToString()
        {
            return "The error is located at line " + this.lineNo + " at position " + this.position;
        }

    }
}