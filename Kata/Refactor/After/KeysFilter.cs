namespace Kata.Refactor.After
{
    using System.Collections.Generic;
    using System.Linq;

    namespace Kata.Refactor.Before
    {
        public class KeysFilter
        {
            private ISessionService SessionService { get; set; }

            public List<string> Filter(IList<string> marks, bool isGoldenKey)
            {
                if (marks != null && marks.Count > 0)
                {
                    return new List<string>();
                }

                if (isGoldenKey)
                {
                    var goldenMarks  = GetGoldenMarks(marks);
                    
                    var goldenMarksFilterByKeys = GetMarksFilterByKeys(goldenMarks, FilterKey.CopperKey);

                    return goldenMarksFilterByKeys;
                }

                var silverMarksFilterByKeys = GetMarksFilterByKeys(marks, FilterKey.SilverKey);

                var copperMarksFilterByKeys = GetMarksFilterByKeys(marks, FilterKey.CopperKey);

                var otherMarks = silverMarksFilterByKeys.Concat(copperMarksFilterByKeys).ToList();
                
                return otherMarks;
            }

            private List<string> GetMarksFilterByKeys(IEnumerable<string> marks, FilterKey filter)
            {
                var silverKeys = SessionService.Get<List<string>>(filter.ToString());

                var keys = new List<string>(silverKeys);

                return marks.Where(mark => keys.Contains(mark) || IsFakeMark(mark)).ToList();
            }

            private List<string> GetGoldenMarks(IList<string> marks)
            {
                var golden01Mark = marks.Where(x => x.StartsWith("GD01"));

                var golden02Mark = marks.Where(x => x.StartsWith("GD02"))
                    .Where(x => golden01Mark.Any(m => m.Substring(4, 6).Equals(x.Substring(4, 6)))).ToList();

                var goldenMarks = golden02Mark.Concat(golden01Mark).ToList();
                
                return goldenMarks;
            }

            private bool IsFakeMark(string mark)
            {
                return mark.EndsWith("FAKE");
            }
        }

        public interface ISessionService
        {
            IEnumerable<string> Get<T>(string sessionKey);
        }
    }
}