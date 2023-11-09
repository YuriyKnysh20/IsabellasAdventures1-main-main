using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiEvents
{
    public event Action<int, int> onBerriesChanged;

    public void BerriesChanged(int _berriesCollected, int _berriesToComplete)
    {
        onBerriesChanged?.Invoke(_berriesCollected, _berriesToComplete);
    }
}
