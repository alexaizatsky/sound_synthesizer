using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class synthSettings : MonoBehaviour
{
    [HideInInspector] public int currentOctave;
    [HideInInspector] public soundGenerator.Type osc1Type;
    [HideInInspector] public soundGenerator.Type osc2Type;
    [HideInInspector] public float osc1Amp;
    [HideInInspector] public float osc2Amp;
    [HideInInspector] public float noiseAmp;
    [HideInInspector] public float atackParam;
    [HideInInspector] public float sustainParam;
    [HideInInspector] public float releaseParam;
    [HideInInspector] public float cutFilter;
    [HideInInspector] public float resFilter;
}
