using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Stat {

    [SerializeField] public BarScript bar;

    [SerializeField] private float maxVal;
    public float MaxVal {
        get {
            return maxVal;
        }

        set {
            maxVal = value;
            bar.MaxValue = maxVal;
        }
    }

    [SerializeField] private float currentVal;
    public float CurrentVal {
        get {
            return currentVal;
        }

        set {
            currentVal = value;
            bar.Value = currentVal;
        }
    }

    public void Initialize() {
        this.MaxVal = maxVal;
        this.CurrentVal = currentVal;
    }
}
