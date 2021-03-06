using System;
namespace ThirtyDaysOfTDD.UnitTests
{
    public class StringUtils
    {
        public int FindNumberOfOccurences(string sentenceToScan, string characterToScanFor)
        {
            if (characterToScanFor.Length != 1)
            {
                throw new ArgumentException();
            }

            char charToScan = char.Parse(characterToScanFor);
            int numberOfOccurences = 0;

            for (int i = 0; i < sentenceToScan.Length; i++)
            {
                if (sentenceToScan[i] == charToScan)
                {
                    numberOfOccurences++;
                }
            }

            return numberOfOccurences;
        }
    }
}