using System;
using System.Linq;
using NUnit.Framework;

namespace ThirtyDaysOfTDD.UnitTests
{
    [TestFixture]
    public class StringUtilsTests
    {
        [Test]
        public void ShouldBeAbleToCountNumberOfLettersInSimpleSentence()
        {
            // Arrange
            string sentenceToScan = "TDD is awesome!";
            string characterToScanFor = "e";
            int expectedResult = 2;
            StringUtils stringUtils = new StringUtils();

            // Act
            int actualResult = stringUtils.FindNumberOfOccurences(sentenceToScan, characterToScanFor);

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
            StringUtils stringUtils = new StringUtils();

            // Act
            int actualResult = stringUtils.FindNumberOfOccurences(sentenceToScan, characterToScanFor);

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ShouldGetAnArgumentExceptionWhenCharacterToScanForIsLargerThanOneCharacter()
        {
            // Arrange
            string sentenceToScan = "This test should thrown an exception";
            string characterToScanFor = "xx";
            StringUtils stringUtils = new StringUtils();

            // Act && Assert
            Assert.Throws<FormatException>(() => 
            {
                stringUtils.FindNumberOfOccurences(sentenceToScan, characterToScanFor);
            });
        }
    }
}
