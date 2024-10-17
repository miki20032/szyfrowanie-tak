public class HashContext
{
    private Szyfrowanie _hashStrategy;

    public void SetHashStrategy(Szyfrowanie hashStrategy)
    {
        _hashStrategy = hashStrategy;
    }

    public string Hash(string input)
    {
        return _hashStrategy.Hash(input);
    }
}
