using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SeatingStudents
{
    public class SeatingStudentsTest
    {
        [Fact]
        public void Should_Test_Example_Pattern()
        {
            var input = new int[] {8, 1, 8};
            var expected = 6;
            var result = SeatingStudents(input);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Should_Test_Example2()
        {
            var input = new int[] {6, 4};
            var expected = 4;
            var result = SeatingStudents(input);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Should_Test_Example3()
        {
            var input = new int[] {12, 2, 6, 7, 11};
            var expected = 6;
            var result = SeatingStudents(input);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Should_Test_Invalid_Array()
        {
            var input = new int[] {0, 2, 6, 7, 11};
            var expected = 0;
            var result = SeatingStudents(input);
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void Should_Test_Invalid_Array2()
        {
            var input = new int[] {4, 5,10,20,30};
            var expected = 4;
            var result = SeatingStudents(input);
            Assert.Equal(expected, result);
        }
        
        public static int SeatingStudents(int[] arr)
        {
            var length = arr.FirstOrDefault();
            if (length <= 0)
                return 0;
            
            var emptySlots = arr.Skip(1).ToList();
            var studentsWithLocation = GetDeskList(length);
            return CountNextToEachOther(studentsWithLocation, emptySlots);
        }

        private static int CountNextToEachOther(List<KeyValuePair<int,KeyValuePair<int,int>>> studentsWithLocation, List<int> emptySlots)
        {
            if (studentsWithLocation == null)
                return 0;
            
            var count = 0;
            
            foreach (var studentLocation in studentsWithLocation)
            {
                var studentRow = studentLocation.Value.Key;
                var studentColumn = studentLocation.Value.Value;
                
                if(emptySlots.Contains(studentLocation.Key))
                    continue;

                var atBack = studentsWithLocation.FirstOrDefault(s =>
                    s.Value.Key == studentRow + 1 && s.Value.Value == studentColumn).Key;
                if (atBack > 0 && !emptySlots.Contains(atBack))
                    count++;
                
                if(studentColumn == 2)
                    continue;
                
                var atRight = studentsWithLocation.FirstOrDefault(s =>
                        s.Value.Key == studentLocation.Value.Key && s.Value.Value == studentLocation.Value.Value + 1).Key;
                if (atRight > 0 && !emptySlots.Contains(atRight))
                    count++;
            }

            return count;
        }

        private static List<KeyValuePair<int, KeyValuePair<int, int>>> GetDeskList(int length)
        {
            var studentsWithLocation = new List<KeyValuePair<int, KeyValuePair<int, int>>>();
            var row = 1;
            var column = 1;

            for (var i = 0; i < length; i++)
            {
                var location = new KeyValuePair<int, int>(row, column);
                studentsWithLocation.Add(new KeyValuePair<int, KeyValuePair<int, int>>(i + 1, location));
                if (column == 2)
                {
                    row++;
                    column = 1;
                }
                else
                    column++;
            }

            return studentsWithLocation;
        }
    }
}