namespace Phone.Api.Services
{
    public class PhoneCombinationService
    {
        private static readonly Dictionary<char, string> PhoneMap = new()
        {
            { '2', "abc" }, { '3', "def" }, { '4', "ghi" },
            { '5', "jkl" }, { '6', "mno" }, { '7', "pqrs" },
            { '8', "tuv" }, { '9', "wxyz" }
        };

        public List<string> GetLetterCombinations(string digits)
        {
            if (string.IsNullOrEmpty(digits)) return new List<string>();

            List<string> result = new();
            Backtrack(result, digits, 0, "");
            return result;
        }

        private void Backtrack(List<string> result, string digits, int index, string current)
        {
            if (index == digits.Length)
            {
                result.Add(current);
                return;
            }

            foreach (char letter in PhoneMap[digits[index]])
            {
                Backtrack(result, digits, index + 1, current + letter);
            }
        }
    }
}
