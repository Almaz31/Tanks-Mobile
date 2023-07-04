using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsMobile : MonoBehaviour
{
    public static IsMobile instance;
    public bool isMobile { get ; private set; }
    private void Awake()
    {
        if (instance == null)
            instance = this;
        isMobile = SystemInfo.deviceType == DeviceType.Handheld ? true : false;
    }

}
