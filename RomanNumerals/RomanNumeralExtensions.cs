using System;

namespace RomanNumerals
{
    public static class RomanNumeralExtensions
    {
        public static int RomanToInt(this string roman)
        {
            int result = ConvertRomanToInt(roman);
            ValidateRomanToIntConversion(roman, result);
            return result;
        }

        private static int ConvertRomanToInt(string roman)
        {
            Console.WriteLine("Running string to int conversion");
            int arabicValue = 0;
            RomanCharacter prevCharacter = null;
            // work from right to left; sum (or subtract) as you go
            for (int i = roman.Length - 1; i >= 0; i--)
            {
                var current = GetRomanCharacter(roman[i]);
                if (prevCharacter == null) prevCharacter = current;
                var sign = ShouldAddOrSubtractTheCurrentValue(current, prevCharacter);
                arabicValue += sign*current.Value;
                prevCharacter = current;
            }
            return arabicValue;
        }

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

        public static bool RomanValidFormat(this string roman)
        {
            try
            {
                var converted = ConvertRomanToInt(roman);
                return ValidateRomanToIntConversion(roman, converted);
            }
            catch
            {
                return false;
            }
        }

        private static bool ValidateRomanToIntConversion(string expected, int number)
        {
            if (number.ToRoman() == expected)
                return true;
            throw new ArgumentException("Invalid RomanNumeral");
        }

        public static bool RomanValidFormatOld(this string roman)
        {
            int countOfCurrentRepeat = 1;
            RomanCharacter current = GetRomanCharacter(roman[roman.Length - 1]);
            RomanCharacter previous = null;
            if (current == null)
                return false;
            var maxSoFar = current;
            for (int i = roman.Length - 2; i >= 0; i--)
            {
                RomanCharacter next = GetRomanCharacter(roman[i]);
                if (next == null)
                    return false;
                if (!IsCurrentValueValid(current, next, previous, maxSoFar, countOfCurrentRepeat))
                    return false;
                countOfCurrentRepeat = IncrementOrResetRepeatCount(countOfCurrentRepeat, current, next);
                if (countOfCurrentRepeat > current.MaxSequential)
                    return false;
                Console.WriteLine(countOfCurrentRepeat);
                maxSoFar = current.Value > maxSoFar.Value ? current : maxSoFar;
                previous = current;
                current = next;
            }
            return true;
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
        private static int PlaceValueForRomanNumeral(int number, RomanCharacter roman)
        {
            int digit = number%(roman.Value*10)/roman.Value;
            if (digit < 0 || digit > 10) 
                throw new ArithmeticException("digit must be between 0 and 9 inclusive");
            return digit;
        }
        private static int ShouldAddOrSubtractTheCurrentValue(RomanCharacter current, RomanCharacter prevCharacter)
        {
            if (prevCharacter == null)
                return 1;
            return prevCharacter.Decrementor == current ? -1 : 1;
        }

        private static RomanCharacter GetRomanCharacter(char roman)
        {
            if (RomanCharacter.Symbols.ContainsKey(roman)) return RomanCharacter.Symbols[roman];
            return null;
            //else throw new ArgumentOutOfRangeException(String.Format("roman numerals cannot contain the value '{0}'", roman.ToString()));
        }

        private static bool IsCurrentValueValid(RomanCharacter current, RomanCharacter next, RomanCharacter previous, RomanCharacter maxSoFar, int repeat)
        {
            if (NextIsValidDecrement(current, next, previous, repeat))
                return true;
            if (NextIsValidRegular(current, next, maxSoFar))
                return true;
            return false;
        }

        private static bool NextIsValidRegular(RomanCharacter current, RomanCharacter next, RomanCharacter maxSoFar)
        {
            return next.Value >= current.Value && next.Value >= maxSoFar.Value;
        }

        private static bool NextIsValidDecrement(RomanCharacter current, RomanCharacter next, RomanCharacter previous, int repeat)
        {
            var prevValue = previous == null ? 0 : previous.Value;
            bool currentDecrements = current.Value < prevValue;
            bool nextDecrements = next.Value < current.Value;
            if (nextDecrements && currentDecrements)
                return false;
            bool nextIsValidDecrement = 
                nextDecrements
                && next.Value == current.Decrementor.Value
                && repeat == 1
                ;
            return nextIsValidDecrement;

        }

        private static int IncrementOrResetRepeatCount(int repeat, RomanCharacter current, RomanCharacter next)
        {
            return current.Value == next.Value ? repeat + 1 : 1;
        }
    }
}
