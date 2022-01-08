using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsrUI : MonoBehaviour
{
    [SerializeField] private Slider atackSlider;
    [SerializeField] private Slider sustainSlider;
    [SerializeField] private Slider releaseSlider;

    public void Init()
    {
        dependencyManager.Instance._synthSettings.atackParam = Mathf.Lerp(0.0001f, 1, atackSlider.value);
        //dependencyManager.Instance._synthSettings.sustainParam = Mathf.Lerp(0.0001f, 1, sustainSlider.value);
        dependencyManager.Instance._synthSettings.sustainParam = Mathf.Lerp(0.00004f, .000005f, sustainSlider.value);
        dependencyManager.Instance._synthSettings.releaseParam = Mathf.Lerp(0.00007f, .00001f, releaseSlider.value);
    }

    public void OnValueChange()
    {
        Init();
        dependencyManager.Instance._mySynth.UpdateAdsr();
    }
}
