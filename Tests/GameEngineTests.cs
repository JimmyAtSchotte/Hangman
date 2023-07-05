using NUnit.Framework;

namespace Tests;

[TestFixture]
public class GameEngineTests
{
    [Test]
    public void CorrectWordGuess()
    {
        var game = new GameEngine();
        var guessResult = game.Guess('a');
        
        Assert.IsTrue(guessResult);
    }
}

public class GameEngine
{
    public bool Guess(char c)
    {
        return true;
    }
}