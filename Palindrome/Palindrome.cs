using System.Collections.Generic;
using System.Linq;

namespace Palindrome
{
    public class Palindrome
    {
        private string Input { get; }
        private int MaxSkipLimit { get; }
        
        private int Index { get; set; }
        private int HowMuchSkipHead { get; set; }
        private int HowMuchSkipBack { get; set; }
        private bool Equality { get; set; }
        private List<string> Possibilities { get; }

        public Palindrome(string input, int maxSkipLimit = 0)
        {
            Input = input;
            MaxSkipLimit = maxSkipLimit;
            Possibilities = new List<string>();
            ResetAll();
        }

        public string GetResult()
        {
            Possibilities.Add(TrimMid());
            ResetAll();
            Possibilities.Add(TrimHead());
            ResetAll();
            Possibilities.Add(TrimBack());
            return GetPossibilities();
        }

        private void ResetAll()
        {
            ResetSkip();
            ResetIndex();
        }
        private void ResetIndex()
        {
            Index = 0;
            Equality = false;
        }

        private void ResetSkip()
        {
            HowMuchSkipHead = 0;
            HowMuchSkipBack = 0;
        }

        private string GetPossibilities()
        {
            if (Possibilities.Any(x => x == PalindromeResult))
                return PalindromeResult;

            var positiveResult= Possibilities.Where(x=>x != NotPossible).OrderBy(x => x.Length).FirstOrDefault();
            return string.IsNullOrEmpty(positiveResult) ? NotPossible : positiveResult;
        }

        private string TrimMid()
        {
            if (GetBackIndex() <= GetHeadIndex())
                return Final();
            
            if (IsEqual())
            {
                Equality = true;
                Index++;
                return TrimMid();
            }

            return Equality ? Final() : NotPossible;
        }

        private string TrimHead()
        {
            if(HowMuchSkipHead >= MaxSkipLimit)
                return NotPossible;
            
            HowMuchSkipHead++;
            if (TrimMid() == PalindromeResult)
                return GetTrimResult();

            ResetIndex();
            return TrimHead();
        }

        private string TrimBack()
        {
            if(HowMuchSkipBack >= MaxSkipLimit)
                return NotPossible;
            
            HowMuchSkipBack++;
            if (TrimMid() == PalindromeResult)
                return GetTrimResult();

            ResetIndex();
            return TrimBack();
        }
        
        private string GetTrimResult()
        {
            if (Input.Length - HowMuchSkipHead < MinimumPalindrome ||
                Input.Length - HowMuchSkipBack < MinimumPalindrome)
                return NotPossible;
            
            return HowMuchSkipHead > 0
                ? Input.Substring(0, HowMuchSkipHead)
                : Input.Substring(Input.Length - HowMuchSkipBack);
        }

        private bool IsEqual() => GetHead() == GetBack();

        private string GetHead() => Input.Substring(GetHeadIndex(), 1);
        private string GetBack() => Input.Substring(GetBackIndex(), 1);

        private string Final()
        {
            Index--;
            var distance = GetDistanceBetweenIndexes();
            if (distance <= MinimumDistanceForPalindrome)
                return PalindromeResult;
            if (distance <= MaxSkipLimit)
                return GetLettersBetweenIndexes(distance);
            if (distance == MaxSkipLimit + 1)
                return GetLettersBetweenIndexes(MaxSkipLimit);

            return NotPossible;
        }

        private string GetLettersBetweenIndexes(int take) => Input.Substring(GetHeadIndex() + 1, take);
        private int GetHeadIndex() => Index + HowMuchSkipHead;
        private int GetBackIndex() => Input.Length - Index - HowMuchSkipBack - 1;
        private int GetDistanceBetweenIndexes() => GetBackIndex() - GetHeadIndex() - 1;

        private const string NotPossible = "not possible";
        private const string PalindromeResult = "palindrome";
        private const int MinimumDistanceForPalindrome = 1;
        private const int MinimumPalindrome = 3;
    }
}