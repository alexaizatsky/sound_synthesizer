using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiManager : MonoBehaviour
{

    [SerializeField] private OscUI _oscUi;
    [SerializeField] private AdsrUI _adsrUi;
    [SerializeField] private filterUI _filterUi;
    public void Init()
    {
        _oscUi.Init();
        _adsrUi.Init();
        _filterUi.Init();
    }
}
