using System;

public class MiscEvents
{
    public event Action onCoinCollected;

    public void CoinCollected()
    {
        if (onCoinCollected != null) onCoinCollected();
    }
}
