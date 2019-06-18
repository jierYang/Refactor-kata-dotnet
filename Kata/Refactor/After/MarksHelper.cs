using System.Collections.Generic;
using System.Linq;

namespace Kata.Refactor.After
{
    public class MarksHelper
    {
        public List<string> GetGoldenMarks(IList<string> marks)
        {
            var golden02Mark = marks.Where(x => x.StartsWith("GD02"));
            
            var invalidKeys = golden02Mark.Where(mark => !marks.Any(x => x.StartsWith("GD01") && mark.Substring(4, 6).Equals(x.Substring(4, 6)))).ToList();

            return marks.Where(m => !invalidKeys.Contains(m)).ToList();
        }

        public bool IsFakeMark(string mark)
        {
            return mark.EndsWith("FAKE");
        }
    }
}