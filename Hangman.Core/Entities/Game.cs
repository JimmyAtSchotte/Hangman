﻿namespace Hangman.Core.Entities;

public class Game
{
    public Guid Guid { get; set; }
    public string CorrectWord { get; set; }
    
    public List<GuessResult> PreviousGuesses { get; set; }
}