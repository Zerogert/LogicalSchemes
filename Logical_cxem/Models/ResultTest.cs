using System;

namespace Logical_cxem.Models
{
    internal class ResultTest
    {
        public ResultTest(Guid id, string name, int countTry, int result, string testName, Guid testId)
        {
            ID = id;
            Name = name;
            countTry = CountTry;
            Result = result;
            TestName = testName;
            TestID = testId;
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public int CountTry { get; set; }
        public int Result { get; set; }
        public string TestName { get; set; }
        public Guid TestID { get; set; }
    }
}