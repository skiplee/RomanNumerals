using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RomanNumerals
{
    public static class Converter
    {
        public static string ToRoman(this int number)
        {
            if (number <= 0)  throw new ArgumentOutOfRangeException("number", "Must be greater than zero.");
            if (number >= 4000) throw new ArgumentOutOfRangeException("number", "Must be less than or equal to 4000");

            string roman = "";
            roman += RomanNumeralForPlace(1000, number, 'M', ' ', ' '); // no roman numerals for higher values
            roman += RomanNumeralForPlace(100 , number, 'C', 'D', 'M');
            roman += RomanNumeralForPlace(10  , number, 'X', 'L', 'C');
            roman += RomanNumeralForPlace(1   , number, 'I', 'V', 'X');
            return roman;
        }

        private static string RomanNumeralForPlace(int place, int number, char oner, char fiver, char tener)
        {
            string roman = "";
            var placeValue = (number/place)%10;
            if (placeValue == 9)
                return string.Concat(oner , tener);
            if (placeValue == 4)
                return string.Concat(oner , fiver);
            if (placeValue >=5)
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
            int arabic = 0;
            RomanCharacter prevValue = null ;
            // work from right to left; sum (or subtract) as you go
            for (int i = roman.Length - 1; i >= 0; i--)
            {
                var current = RomanNumeralLookup.Instance.RomanCharacters[roman[i].ToString()];
                if (prevValue == null) prevValue = current;
                var sign = prevValue.DecrementValue == current.Value ? -1 : 1;
                arabic += sign * current.Value;
                prevValue = current;
            }
            return arabic;
        }
    }
}
