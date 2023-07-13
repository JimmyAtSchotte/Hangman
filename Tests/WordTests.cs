using Hangman.Core.Types;
using NUnit.Framework;

namespace Tests;

[TestFixture]
public class WordTests
{
    [TestCase('L')]
    [TestCase('l')]
    public void FindIndexesOfChar(char c)
    {
        var word = (Word)"HELLO";
        var indexes = word.FindIndexes(c);
        
        Assert.AreEqual(2, indexes.Count());
        Assert.AreEqual(2, indexes.ElementAt(0));
        Assert.AreEqual(3, indexes.ElementAt(1));

    }
    
    [TestCase('L')]
    [TestCase('l')]
    public void IgnoreCaseOfWord(char c)
    {
        var word = (Word)"hello";
        var indexes = word.FindIndexes(c);
        
        Assert.AreEqual(2, indexes.Count());
        Assert.AreEqual(2, indexes.ElementAt(0));
        Assert.AreEqual(3, indexes.ElementAt(1));

    }
    
    [Test]
    public void UnknownChar()
    {
        var word = (Word)"HELLO";
        var indexes = word.FindIndexes('U');
        
        Assert.AreEqual(0, indexes.Count());
    }
}