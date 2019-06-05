using System.Collections.Generic;
using System.Linq;

namespace Kata.Refactor.After
{
    public class MarksHelper
    {
        public List<string> GetGoldenMarks(IList<string> marks)
        {
            var golden01Mark = marks.Where(x => x.StartsWith("GD01"));

            var golden02Mark = marks.Where(x => x.StartsWith("GD02"))
                .Where(x => golden01Mark.Any(m => m.Substring(4, 6).Equals(x.Substring(4, 6)))).ToList();

            var goldenMarks = golden02Mark.Concat(golden01Mark).ToList();

            return goldenMarks;
        }

        public bool IsFakeMark(string mark)
        {
            return mark.EndsWith("FAKE");
        }
    }
}