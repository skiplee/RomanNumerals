using RomanNumerals;

namespace RomanNumerals
{
    public static class RomanUtilities
    {
        public static bool RomanValidFormat(this string roman)
        {
            var nextCanBeDecrement = true;
            var max = 0;
            var repeat = 1;
            for (int i = roman.Length - 1; i >= 0; i--)
            {
                var current = RomanNumeralLookup.Instance.RomanCharacters[roman[i].ToString()];
                if (current.Value == 1) nextCanBeDecrement = false;
                var next = i == 0 ? null : RomanNumeralLookup.Instance.RomanCharacters[roman[i - 1].ToString()];
                if (next != null)
                {
                    repeat = current.Value == next.Value ? repeat + 1 : 1;
                    if (repeat > current.MaxSequential) return false;
                    if (nextCanBeDecrement)
                    {
                        nextCanBeDecrement = false;
                        if (next.Value < current.Value && next.Value != current.DecrementValue)
                            return false;
                    }
                    else
                    {
                        if (next.Value < max) return false;
                        if (next.Value < current.Value) return false;
                        if (next.Value == current.Value) nextCanBeDecrement = false;
                    }
                }
                max = current.Value > max ? current.Value : max;
            }
            return true;
        }
    }
}