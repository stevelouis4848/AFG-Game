using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class IntRange {

    public int range_Min;
    public int range_Max;

    public IntRange(int min, int max)
    {
        range_Min = min;
        range_Max = max;
    }

    public int Random
    {
        get { return UnityEngine.Random.Range(range_Min, range_Max); }
    }
}
