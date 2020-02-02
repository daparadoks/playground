using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace Palindrome
{
    public class PalindromeTest
    {
        private readonly ITestOutputHelper _output;
        private readonly string _notPossible;
        private readonly string _palindrome;

        public PalindromeTest(ITestOutputHelper output)
        {
            this._output = output;
            _notPossible = "not possible";
            _palindrome = "palindrome";
        }

        [Fact]
        public void ShouldTestPalindromeClass()
        {
            _output.WriteLine("Başla");
            var list = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("kjjjhjjj", "k"),
                new KeyValuePair<string, string>("abcacba", _palindrome), //mid trim
                new KeyValuePair<string, string>("abxyba", "xy"), //mid trim
                new KeyValuePair<string, string>("abxxcba", "xx"), //mid trim
                new KeyValuePair<string, string>("abcxxba", "cx"), //mid trim
                new KeyValuePair<string, string>("kkjjjhjjj", "kk"), //head trim
                new KeyValuePair<string, string>("kjjjhjjj", "k"), //head trim
                new KeyValuePair<string, string>("jjjhjjjss", "ss"), //back trim
                new KeyValuePair<string, string>("jjjhjjjs", "s"), //back trim
                new KeyValuePair<string, string>("mmop", _notPossible), //mix
            };

            var isItClear = true;
            foreach (var item in list)
            {
                var palindrome = new Palindrome(item.Key, 2);
                var result = palindrome.GetResult();
                _output.WriteLine($"Değer: {item.Key}, beklenen: {item.Value}, sonuç: {result}");
                if (isItClear)
                    isItClear = item.Value == result;
            }

            Assert.True(isItClear);
        }
    }
}