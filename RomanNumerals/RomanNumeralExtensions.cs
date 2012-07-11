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

            string roman = "";
            roman += RomanNumeralForPlace(1000, number, RomanCharacter.M); 
            roman += RomanNumeralForPlace(100 , number, RomanCharacter.C);
            roman += RomanNumeralForPlace(10  , number, RomanCharacter.X);
            roman += RomanNumeralForPlace(1   , number, RomanCharacter.I);
            return roman;
        }


        //clean up to not take place
        private static string RomanNumeralForPlace(int place, int number, RomanCharacter romanCharacter)
        {
            string roman = "";
            var placeValue = (number / place) % 10;

            if (placeValue == 9)
                return String.Concat(romanCharacter.Character, romanCharacter.TenOfThese.Character);
            if (placeValue == 4)
                return String.Concat(romanCharacter.Character, romanCharacter.FiveOfThese.Character);
            if (placeValue >= 5)
            {
                roman = romanCharacter.FiveOfThese.Character.ToString();
                placeValue -= 5;
            }
            roman += new string(romanCharacter.Character, placeValue);
            return roman;
        }

        private static string RomanNumeralForPlacex(int place, int number, char oner, char fiver, char tener)
        {
            string roman = "";
            var placeValue = (number / place) % 10;
            if (placeValue == 9)
                return String.Concat(oner, tener);
            if (placeValue == 4)
                return String.Concat(oner, fiver);
            if (placeValue >= 5)
            {
                roman = fiver.ToString();
                placeValue -= 5;
            }
            roman += new string(oner, placeValue);
            return roman;
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
            var nextCanBeDecrement = true;
            var maxSoFar = RomanCharacter.Symbols[roman[roman.Length - 1]];
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
                        if (TheNextValueIsAnInvalidDecrement(current, next))
                            return false;
                    }
                    else
                    {
                        if (TheNextValueIsValid(current, next, maxSoFar)) return false;
                    }
                    nextCanBeDecrement = CanTheNextValueBeADecrement(current, next, nextCanBeDecrement);
                }
                maxSoFar = current.Value > maxSoFar.Value ? current : maxSoFar;
            }
            return true;
        }

        private static bool CanTheNextValueBeADecrement(RomanCharacter current, RomanCharacter next, bool nextCanBeDecrement)
        {
            if (nextCanBeDecrement || next.Value == current.Value)
                nextCanBeDecrement = false;
            return nextCanBeDecrement;
        }

        private static bool TheNextValueIsValid(RomanCharacter current, RomanCharacter next, RomanCharacter maxSoFar)
        {
            return next.Value < maxSoFar.Value || next.Value < current.Value;
        }

        private static bool TheNextValueIsAnInvalidDecrement(RomanCharacter current, RomanCharacter next)
        {
            return next.Value < current.Value && next.Value != current.Decrementor.Value;
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
