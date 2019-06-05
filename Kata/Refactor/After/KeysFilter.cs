namespace Kata.Refactor.After
{
    using System.Collections.Generic;
    using System.Linq;

    namespace Kata.Refactor.Before
    {
        public class KeysFilter
        {
            private ISessionService SessionService { get; set; }
            
            private readonly MarksHelper _marksHelper = new MarksHelper();

            public List<string> Filter(IList<string> marks, bool isGoldenKey)
            {
                if (marks != null && marks.Count > 0)
                {
                    return new List<string>();
                }

                if (isGoldenKey)
                {
                    var goldenMarks = _marksHelper.GetGoldenMarks(marks);
                    
                    var goldenMarksFilterByKeys = GetMarksFilterByKeys(goldenMarks, FilterKey.GoldenKey);

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

                return marks.Where(mark => keys.Contains(mark) || _marksHelper.IsFakeMark(mark)).ToList();
            }
        }
    }
}