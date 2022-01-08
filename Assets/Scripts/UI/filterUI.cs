using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class filterUI : MonoBehaviour
{
    [SerializeField] private Slider cutoffSlider;
    [SerializeField] private Slider resSlider;

    public void Init()
    {
        dependencyManager.Instance._synthSettings.cutFilter = Mathf.Lerp(40, 12000, cutoffSlider.value);
        dependencyManager.Instance._synthSettings.resFilter = resSlider.value;
    }

    public void OnValueChanges()
    {
        Init();
        dependencyManager.Instance._mySynth.UpdateFilter();
    }
}
