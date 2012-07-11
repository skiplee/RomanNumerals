using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RomanNumerals
{
    public static class RomanNumeralExtensions
    {
        public static string ToRoman(this int number)
        {
            if (number <= 0)  throw new ArgumentOutOfRangeException("number", "Must be greater than zero.");
            if (number >= 4000) throw new ArgumentOutOfRangeException("number", "Must be less than or equal to 4000");

            var roman = String.Empty;
            roman += RomanNumeralForNumber(number, RomanCharacter.M); 
            roman += RomanNumeralForNumber(number, RomanCharacter.C);
            roman += RomanNumeralForNumber(number, RomanCharacter.X);
            roman += RomanNumeralForNumber(number, RomanCharacter.I);
            return roman;
        }

        private static string RomanNumeralForNumber(int number, RomanCharacter romanCharacter)
        {
            int digit = PlaceValueForRomanNumeral(number, romanCharacter);
            if (digit == 9)
                return String.Concat(romanCharacter.Character, romanCharacter.TenOfThese.Character);
            if (digit == 4)
                return String.Concat(romanCharacter.Character, romanCharacter.FiveOfThese.Character);
            string roman = "";
            if (digit >= 5)
            {
                roman = romanCharacter.FiveOfThese.Character.ToString();
                digit -= 5;
            }
            roman += new string(romanCharacter.Character, digit);
            return roman;
        }

        /// <summary>
        /// Return the digit for a place value; for instance, for Roman Numeral C (100)
        ///   and number 3214, return 2 (the hundred's place)
        /// </summary>
        /// <param name="number"></param>
        /// <param name="roman"></param>
        /// <returns></returns>
        private static int PlaceValueForRomanNumeral(int number, RomanCharacter roman)
        {
            int digit = number%(roman.Value*10)/roman.Value;
            if (digit < 0 || digit > 10) 
                throw new ArithmeticException("digit must be between 0 and 9 inclusive");
            return digit;
        }
        public static int RomanToInt(this string roman)
        {
            if (!roman.RomanValidFormat()) throw new ArgumentException(String.Format("Invalid RomanNumeral format: {0}", roman));
            int arabicValue = 0;
            RomanCharacter prevCharacter = null ;
            // work from right to left; sum (or subtract) as you go
            for (int i = roman.Length - 1; i >= 0; i--)
            {
                //var current = RomanNumeralLookup.Instance.RomanCharacters[roman[i].ToString()];
                var current = RomanCharacter.Symbols[roman[i]];
                if (prevCharacter == null) prevCharacter = current;
                var sign = ShouldAddOrSubtractTheCurrentValue(current, prevCharacter);
                arabicValue += sign * current.Value;
                prevCharacter = current;
            }
            return arabicValue;
        }

        private static int ShouldAddOrSubtractTheCurrentValue(RomanCharacter current, RomanCharacter prevCharacter)
        {
            return prevCharacter.Decrementor == current ? -1 : 1;
        }

        public static bool RomanValidFormat(this string roman)
        {
            var nextCanBeDecrement = true; var maxSoFar = RomanCharacter.Symbols[roman[roman.Length - 1]];
            var repeat = 1;
            for (int i = roman.Length - 1; i >= 0; i--)
            {
                var current = RomanCharacter.Symbols[roman[i]];
                if (current.Value == 1) nextCanBeDecrement = false;
                maxSoFar = current.Value > maxSoFar.Value ? current : maxSoFar;
                var next = i == 0 ? null : RomanCharacter.Symbols[roman[i - 1]];
                if (next != null)
                {
                    repeat = IncrementOrResetRepeatCount(repeat, next, current);
                    if (SymbolIsRepeatedTooManyTimes(current, repeat)) return false;
                    if (nextCanBeDecrement)
                    {
                        if (TheNextValueIsAnInvalidDecrement(current, next))
                            return false;
                    }
                    else
                    {
                        if (TheNextValueIsInvalid(current, next, maxSoFar)) return false;
                    }
                    nextCanBeDecrement = CanTheNextValueBeADecrement(current, next, nextCanBeDecrement);

                }
            }
            return true;
        }

        private static bool TheNextValueIsAnInvalidDecrement(RomanCharacter current, RomanCharacter next)
        {
            return next.Value < current.Value && next.Value != current.Decrementor.Value;
        }

        private static bool TheNextValueIsInvalid(RomanCharacter current, RomanCharacter next, RomanCharacter maxSoFar)
        {
            return next.Value < maxSoFar.Value || next.Value < current.Value;
        }
        private static bool CanTheNextValueBeADecrement(RomanCharacter current, RomanCharacter next, bool nextCanBeDecrement)
        {
            if (nextCanBeDecrement || next.Value == current.Value)
                return false;
            //if (next.Value == current.Decrementor.Value)
            //    return true;
            return false;
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
