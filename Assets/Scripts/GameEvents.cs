using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents 
{
    public static event Action OnLevelComplete;

    public static void TriggerLevelComplete()
    {
        OnLevelComplete?.Invoke();
    }
    
}
