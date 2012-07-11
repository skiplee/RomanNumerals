using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using RomanNumerals;

namespace RomanNumeralTests
{
    [TestFixture]
    public class RomanNumeralExtensionTests
    {
        [TestCase(1, "I")]
        [TestCase(3, "III")]
        [TestCase(4, "IV")]
        [TestCase(5, "V")]
        [TestCase(7, "VII")]
        [TestCase(9, "IX")]
        [TestCase(10, "X")]
        [TestCase(46, "XLVI")]
        [TestCase(50, "L")]
        [TestCase(70, "LXX")]
        [TestCase(94, "XCIV")]
        [TestCase(100, "C")]
        [TestCase(890, "DCCCXC")]
        [TestCase(999, "CMXCIX")]
        [TestCase(3000, "MMM")]
        [TestCase(3999, "MMMCMXCIX")]
        public void IntegerExtensionMethodConvertsToRomanNumeral(int arabic, string expectedRoman)
        {
            //act
            var result = arabic.ToRoman();
            //assert
            Assert.That(result, Is.EqualTo(expectedRoman));
        }


        [TestCase(0), ExpectedException(typeof(ArgumentOutOfRangeException))]
        [TestCase(4000)]
        public void ToRomanExceptionOutOfRange(int val)
        {
            var expectError = val.ToRoman();
        }

        [TestCase("IIX"), ExpectedException(typeof(ArgumentException))]
        public void RomanToIntInvalidRomanNumeralException(string roman)
        {
            var expectError = roman.RomanToInt();
        }

        [TestCase("A", false)]
        [TestCase("AX", false)]
        [TestCase("IIX", false)]
        [TestCase("VIX", false)]
        [TestCase("VX", false)]
        [TestCase("XXXX", false)]
        [TestCase("IXX", false)]
        [TestCase("IXC", false)]
        [TestCase("XCI", true)]
        [TestCase("XIXXX", false)]
        [TestCase("XIX", true)]
        [TestCase("IX", true)]
        [TestCase("XXXVI", true)]
        public void RomanValidFormat(string roman, bool expected)
        {
            //act
            var result = roman.RomanValidFormat();
            //assert
            Assert.That(result, Is.EqualTo(expected)); 
        }

        [TestCase("I", 1)]
        [TestCase("III", 3)]
        [TestCase("IV", 4)]
        [TestCase("V", 5)]
        [TestCase("VI", 6)]
        [TestCase("X", 10)]
        [TestCase("XV", 15)]
        [TestCase("XXXVI", 36)]
        [TestCase("C", 100)]
        [TestCase("MC", 1100)]
        public void StringExtensionMethodConvertsToIntegers(string roman, int expectedInt)
        {
            //act
            var result = roman.RomanToInt();
            //assert
            Assert.That(result, Is.EqualTo(expectedInt));
        }
    }
}
