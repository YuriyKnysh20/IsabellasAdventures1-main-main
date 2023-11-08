using System;

public class MiscEvents
{
    public event Action onCoinCollected;

    public void BerryCollected(int count)
    {
        onCoinCollected?.Invoke();
    }
    public event Action onEnemyKilled;

    public void EnemyKilled()
    {
        onEnemyKilled?.Invoke();
    }
}
