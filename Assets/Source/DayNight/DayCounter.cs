using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCounter : MonoBehaviour
{
    [field:SerializeField] public int Current { get; private set; }
    
    public static DayCounter Instance { get; private set; }

    public event Action<int> DayChanged;
    
    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            return;
        }
    }
    
    public void ChangeDay()
    {
        Current++;
        DayChanged?.Invoke(Current);
    }
}
