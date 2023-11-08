using System;

public class MiscEvents
{
    public event Action onBerryCollected;

    public void BerryCollected(int count)
    {
        onBerryCollected?.Invoke();
    }
    public event Action onEnemyKilled;

    public void EnemyKilled()
    {
        onEnemyKilled?.Invoke();
    }
}
