namespace Compiler
{
    /// <summary>
    /// A position in a file
    /// </summary>
    public class Position
    {

        int lineNo { get; }
        int position { get; }

        public Position(int lineNo, int position)
        {
            this.lineNo = lineNo;
            this.position = position;
        }

        public override string ToString()
        {
            if (this == BuiltIn)
                return "System defined";
            else
                return $"Line {lineNo}, Column {position}";
        }

        public static Position BuiltIn { get; } = new Position(-1, -1);

    }
}