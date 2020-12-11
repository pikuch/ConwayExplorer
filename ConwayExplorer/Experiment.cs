using System.Text;

namespace ConwayExplorer
{
    internal class Experiment
    {
        public uint Hash { get; set; }
        public GameBoard Pattern { get; set; }
        public bool Tested { get; set; }
        public int TestLimit { get; set; }
        public bool LoopFound { get; set; }
        public int LoopStart { get; set; }
        public int LoopLength { get; set; }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder(70);
            output.Append($"{Hash:D10} ");
            if (Tested)
            {
                output.Append($"tested up to {TestLimit}, ");
                if (LoopFound)
                {
                    output.Append($"a {LoopLength} long loop found after {LoopStart} gens");
                }
                else
                {
                    output.Append($"didn't find loops");
                }
            }
            else
            {
                output.Append("not tested yet");
            }
            return output.ToString();
        }
    }
}