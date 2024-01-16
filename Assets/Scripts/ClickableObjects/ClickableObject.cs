using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class ClickableObject : MonoBehaviour
{
    public GameObject UI;
    GameObject manager;
    public Vector3 gridSize;
    GameObject currentPathEnd;


    //the manager for clickable objects
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("ClickableObjectManager");
        if(GetComponent<LineRenderer>() != null)
        {
            GetComponent<LineRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void changePathEnd(GameObject endpoint)    
    {
        if(currentPathEnd != null)
        {
            Destroy(currentPathEnd);
        }
        currentPathEnd = endpoint;
        DrawLine();
    }

    void DrawLine()
    {
        if(GetComponent<LineRenderer>()!= null)
        {

        }
    }


    public void showUI()
    {
        if (UI != null)
        {
            //UI.SetActive(true);
            GameObject.Find("MainUI").GetComponent<UIManager>().reprioritise(UI);
        }
        //then implement some glowing effect
    }
    public void hideUI()
    {
        if (UI != null)
        {
            //UI.SetActive(false);
            GameObject.Find("MainUI").GetComponent<UIManager>().closeThis();
        }
    }

    public void moveDrag()
    {
        if(transform.parent != null)
        {
            manager.GetComponent<ClickableObjectManager>().moveDragObject(transform.parent.gameObject);
        }
        else
        {
            manager.GetComponent<ClickableObjectManager>().moveDragObject(gameObject);
        }
        
    }

    private void OnMouseOver()
    {
        if(transform.parent!= null)
        {
            manager.GetComponent<ClickableObjectManager>().setMouseHovering(transform.parent.gameObject);
        }
        else
        {
            manager.GetComponent<ClickableObjectManager>().setMouseHovering(gameObject);
        }
       
    }
    private void OnMouseExit()
    {
        manager.GetComponent<ClickableObjectManager>().setMouseHovering(null);
    }
}
