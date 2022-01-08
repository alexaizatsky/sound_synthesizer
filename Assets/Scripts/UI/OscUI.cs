using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscUI : MonoBehaviour
{
    [SerializeField] private Dropdown osc1Dropdown;
    [SerializeField] private Dropdown osc2Dropdown;
    [SerializeField] private Slider osc1AmpSlider;
    [SerializeField] private Slider osc2AmpSlider;
    [SerializeField] private Slider noiseAmpSlider;

    public void Init()
    {
        dependencyManager.Instance._synthSettings.osc1Type = (soundGenerator.Type) osc1Dropdown.value;
        dependencyManager.Instance._synthSettings.osc2Type = (soundGenerator.Type) osc2Dropdown.value;
        dependencyManager.Instance._synthSettings.osc1Amp = osc1AmpSlider.value;
        dependencyManager.Instance._synthSettings.osc2Amp = osc2AmpSlider.value;
        dependencyManager.Instance._synthSettings.noiseAmp = noiseAmpSlider.value;
    }

    public void OscValueChange()
    {
        Init();
        dependencyManager.Instance._mySynth.UpdateOSC();
    }
    
}
