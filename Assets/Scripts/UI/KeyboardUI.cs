using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardUI : MonoBehaviour
{
    
    public void KeyDown(int _key)
    {
        dependencyManager.Instance._mySynth.StartPlay(_key, dependencyManager.Instance._synthSettings.currentOctave);
    }

    public void KeyUp(int _key)
    {
        dependencyManager.Instance._mySynth.StopPlay();
    }

    public void IncreaseOctave()
    {
        if (dependencyManager.Instance._synthSettings.currentOctave < 5)
            dependencyManager.Instance._synthSettings.currentOctave++;
    }

    public void ReduceOctave()
    {
        if (dependencyManager.Instance._synthSettings.currentOctave > 0)
            dependencyManager.Instance._synthSettings.currentOctave--;
    }

    
}
