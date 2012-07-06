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
                var current = RomanCharacter.Symbols[roman[i]];
                if (current.Value == 1) nextCanBeDecrement = false;
                var next = i == 0 ? null : RomanCharacter.Symbols[roman[i - 1]];
                if (next != null)
                {
                    repeat = IncrementOrResetRepeatCount(repeat, next, current);
                    if (SymbolIsRepeatedTooManyTimes(current, repeat)) return false;
                    if (nextCanBeDecrement)
                    {
                        nextCanBeDecrement = false;
                        if (next.Value < current.Value && next.Value != current.Decrementor.Value)
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

        private static bool SymbolIsRepeatedTooManyTimes(RomanCharacter current, int repeat)
        {
            return repeat > current.MaxSequential;
        }

        private static int IncrementOrResetRepeatCount(int repeat, RomanCharacter next, RomanCharacter current)
        {
            return current.Value == next.Value ? repeat + 1 : 1;
        }
    }
}