using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public List<AK.Wwise.Event> MyEvents;
    public AK.Wwise.Event MyEvents2;
    public List<AK.Wwise.Event> MyEvents3;
    public List<AK.Wwise.Event> MyEvents4;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PlaySound()
    {
        int randomInt = Random.Range(0, MyEvents.Count);
        MyEvents[randomInt].Post(gameObject);
    }

    public void PlaySound2()
    {
        //int randomInt = Random.Range(0, MyEvents.Count);
        MyEvents2.Post(gameObject);
    }

    public void PlaySound2Pass2Parent()
    {
        if (GetComponentInParent<ClickableObject>() != null)
        {
            MyEvents2.Post(GetComponentInParent<ClickableObject>().gameObject);
        }
        
    }

    public void PlaySpecificSound(AK.Wwise.Event myEvent)
    {
        myEvent.Post(gameObject);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
