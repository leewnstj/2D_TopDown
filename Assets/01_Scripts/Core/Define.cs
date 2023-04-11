using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Health,
    Ammo,
    Coin
}

public class Define : MonoBehaviour
{
    protected static Camera _mainCam = null;
    public static Camera MainCam
    {
        get
        {
            if (_mainCam == null) _mainCam = Camera.main;
            return _mainCam;
        }
    }
}
