namespace Hangman.Core.Types;

public class Word
{
    private readonly IDictionary<char, int[]> _charIndexes;
    private readonly string _word;

    public Word(string word) 
    {
        _word = word.ToUpperInvariant();
        _charIndexes = ToCharIndexesDictionary(_word);
    }

    public int Length => _word.Length;

    private static IDictionary<char, int[]> ToCharIndexesDictionary(string word)
    {
        var dictionary = new Dictionary<char, List<int>>();

        for (var i = 0; i < word.Length; i++)
        {
            var c = word[i];
            
            if (dictionary.ContainsKey(c))
                dictionary[c].Add(i);
            else
                dictionary.Add(c, new List<int>()
                {
                    i
                });
        }

        return dictionary.ToDictionary(x => x.Key, x => x.Value.ToArray());
    }
    
    public static explicit operator Word(string value)
    {
        return new Word(value);
    }

    public IEnumerable<int> FindIndexes(char c)
    {
        return _charIndexes.TryGetValue(char.ToUpperInvariant(c), out var indexes) ? indexes : Enumerable.Empty<int>();
    }

    public char[] ToCharArray() => _word.ToArray();

    public bool ContainsChar(char c) => _charIndexes.ContainsKey(char.ToUpperInvariant(c));
}