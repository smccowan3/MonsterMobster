using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CustomRainSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public Light light;
    float rainIntensity;
    public AK.Wwise.Event RainPlaylistEvent;
    public AK.Wwise.Event RainMusicPlaylistEvent;
    bool allowLightChanges;
    void Start()
    {
        // Start playing the Playlist Container
        RainPlaylistEvent.Post(gameObject, (uint)AkCallbackType.AK_MusicPlaylistSelect, AudioChanged);
        RainMusicPlaylistEvent.Post(gameObject);
        allowLightChanges = false;
        Invoke("LightChangeON", 2);
    }
    void LightChangeON()
    {
        print("setting light change to on");
        allowLightChanges = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void AudioChanged(object in_cookie, AkCallbackType in_type, object in_info)
    {
        if (in_type == AkCallbackType.AK_MusicPlaylistSelect)
        {
            AkMusicPlaylistCallbackInfo cinfo = in_info as AkMusicPlaylistCallbackInfo;
            if(cinfo != null)
            {
                switch (cinfo.uPlaylistSelection)
                {
                    case 1:
                        rainIntensity = 0.5f;
                        StartCoroutine(lightTransition(0.7f, 3));
                        break;
                    case 2:
                        rainIntensity = 0.05f;
                        StartCoroutine(lightTransition(2, 3));
                        break;
                    case 0:
                        rainIntensity = 0.35f;
                        StartCoroutine(lightTransition(1.5f, 3));
                        break;

                }
                FindAnyObjectByType<RainScript>().RainIntensity = rainIntensity;
            }
           
        }
    }



    IEnumerator lightTransition(float end, float duration)
    {

        if (allowLightChanges)
        {
            float start = light.intensity;
            float currentTime = 0;
            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                light.intensity = Mathf.Lerp(start, end, currentTime / duration);
                yield return null;
            }
        }
       

        yield return null;
    }

}
