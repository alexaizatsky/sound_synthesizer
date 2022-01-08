using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyUI : MonoBehaviour
{
    [SerializeField] private int myKey;
    [SerializeField] private KeyboardUI _keyboardUi;

    public void ButtonDown()
    {
        _keyboardUi.KeyDown(myKey);
    }

    public void ButtonUp()
    {
        _keyboardUi.KeyUp(myKey);
    }
}
