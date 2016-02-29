using System;
using System.Linq;
using NUnit.Framework;

namespace ThirtyDaysOfTDD.UnitTests
{
    [TestFixture]
    public class StringUtilsTests
    {
        private StringUtils _stringUtils;

        [TestFixtureSetUp]
        public void SetupStringUtils()
        {
            _stringUtils = new StringUtils();
        }

        [Test]
        public void ShouldBeAbleToCountNumberOfLettersInSimpleSentence()
        {
            // Arrange
            string sentenceToScan = "TDD is awesome!";
            string characterToScanFor = "e";
            int expectedResult = 2;

            // Act
            int actualResult = _stringUtils.FindNumberOfOccurences(sentenceToScan, characterToScanFor);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ShouldBeAbleToCountNumberOfLettersInAComplexSentence()
        {
            // Arrange
            string sentenceToScan = "Once is unique, twice is a coincidence, three times is a pattern.";
            string characterToScanFor = "n";
            int expectedResult = 5;

            // Act
            int actualResult = _stringUtils.FindNumberOfOccurences(sentenceToScan, characterToScanFor);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ShouldGetAnArgumentExceptionWhenCharacterToScanForIsLargerThanOneCharacter()
        {
            // Arrange
            string sentenceToScan = "This test should thrown an exception";
            string characterToScanFor = "xx";

            // Act && Assert
            Assert.Throws<ArgumentException>(() => 
            {
                _stringUtils.FindNumberOfOccurences(sentenceToScan, characterToScanFor);
            });
        }
    }
}
