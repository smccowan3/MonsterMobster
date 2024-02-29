using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathLinkerAndDeleter : MonoBehaviour
{
    GameObject linkedObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LinkObject(GameObject gameObject)
    {
        linkedObject = gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == linkedObject)
        {
            linkedObject.GetComponent<LineRenderer>().positionCount = 0;
            
            Destroy(gameObject);
        }
    }
}
