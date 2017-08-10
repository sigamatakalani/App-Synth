using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestAppsynth1
{
    [TestClass]
    public class UnitTest1
    {
        [TestClass]
        public class RomanNumeralConverterUpperAndLowerBoundsUnitTests
        {

            [TestMethod]
            [ExpectedException(typeof(IndexOutOfRangeException))]
            public void Start_TestMethod()
            {
                var converter = new RomanNumeralConverter();
                converter.ConvertRomanNumeral(3001);
            }

            [TestMethod]
            [ExpectedException(typeof(IndexOutOfRangeException))]
            public void connectTwoNodesDraw_TestMethod()
            {
                var converter = new RomanNumeralConverter();
                converter.ConvertRomanNumeral(3001);
            }

            [TestMethod]
            [ExpectedException(typeof(IndexOutOfRangeException))]
            public void Update_TestMethod()
            {
                var converter = new RomanNumeralConverter();
                converter.ConvertRomanNumeral(3001);
            }

            [TestMethod]
            [ExpectedException(typeof(IndexOutOfRangeException))]
            public void createGraph()
            {
                var converter = new RomanNumeralConverter();
                converter.ConvertRomanNumeral(3001);
            }
        }
    }
}
