using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class dependencyManager : MonoBehaviour
{
    public static dependencyManager Instance;
    public mySynth _mySynth;
    public uiManager _uiManager;
    public synthSettings _synthSettings;
    public UnityEngine.Events.UnityEvent StartInit;
    private void Awake()
    {
        Instance = this;
        _uiManager.Init();
        _mySynth.Init();
    }
}
