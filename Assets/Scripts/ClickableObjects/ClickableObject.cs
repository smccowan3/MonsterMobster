using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObject : MonoBehaviour
{
    public GameObject UI;
    GameObject manager;
    public Vector3 gridSize;
     
     //the manager for clickable objects
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("ClickableObjectManager");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void showUI()
    {
        if (UI != null)
        {
            UI.SetActive(true);
        }
        //then implement some glowing effect
    }
    public void hideUI()
    {
        if (UI != null)
        {
            UI.SetActive(false);
        }
    }

    public void moveDrag()
    {
        manager.GetComponent<ClickableObjectManager>().moveDragObject(gameObject);
    }

    private void OnMouseOver()
    {
        manager.GetComponent<ClickableObjectManager>().setMouseHovering(gameObject);
    }
    private void OnMouseExit()
    {
        manager.GetComponent<ClickableObjectManager>().setMouseHovering(null);
    }
}
