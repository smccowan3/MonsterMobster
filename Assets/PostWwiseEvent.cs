using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostWwiseEvent : MonoBehaviour
{
    public AK.Wwise.Event MyEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void PlaySound()
    {
        MyEvent.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
