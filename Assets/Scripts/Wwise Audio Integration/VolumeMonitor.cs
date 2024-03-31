using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeMonitor : MonoBehaviour
{
    // The RTPC name you created in Wwise
    public AK.Wwise.RTPC volumeLevel;

    void Update()
    {
       //print(volumeLevel.GetGlobalValue().ToString());
    }
}
