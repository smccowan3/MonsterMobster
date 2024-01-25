using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ClickableObject : MonoBehaviour
{
    public GameObject UI;
    GameObject manager;
    public UnityEngine.Vector3 gridSize;
    GameObject currentPathEnd;


    //the manager for clickable objects
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("ClickableObjectManager");
        if(GetComponent<LineRenderer>() != null)
        {
            
        }
        /*if(UI.GetComponent<Canvas>().worldCamera == null)
        {
            UI.GetComponent<Canvas>().worldCamera = Camera.main;
        }*/
        UI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(GetComponent<MonsterUnit>() != null)
        {
            if (GetComponent<MonsterUnit>().busy)
            {
                GetComponent<NavMeshAgent>().SetDestination(transform.position);
            }
        }

        if(GetComponent<NavMeshAgent>() != null)
        {
            if (GetComponent<NavMeshAgent>().hasPath)
                if(GetComponent<NavMeshAgent>().pathEndPosition != transform.position)
                {

                    {

                        NavMeshAgent agent = GetComponent<NavMeshAgent>();
                        LineRenderer lineRenderer = GetComponent<LineRenderer>();

                        lineRenderer.startWidth = 0.15f;
                        lineRenderer.endWidth = 0.15f;
                        lineRenderer.startColor = Color.yellow;
                        lineRenderer.endColor = Color.yellow;

                        // Enable LineRenderer

                        UnityEngine.Vector3[] pathPoints = agent.path.corners;

                        // Set the number of line points to match the number of path corners
                        lineRenderer.positionCount = pathPoints.Length;
                        lineRenderer.SetPosition(0, transform.position);


                        if (pathPoints.Length < 2)
                        {

                        }
                        else
                        {
                            // Project path points onto the plane
                            for (int i = 1; i < pathPoints.Length; i++)
                            {
                                UnityEngine.Vector3 pointPosition = new UnityEngine.Vector3(pathPoints[i].x, pathPoints[i].y, pathPoints[i].z);
                                lineRenderer.SetPosition(i, pointPosition);
                            }
                        }
                    }


            }
        }

        /*if(transform.Find("Button").gameObject.tag == "FButton")
        {

            UnityEngine.Vector3 characterScreenPosition = Camera.main.WorldToScreenPoint(transform.position);

            // Convert the character's screen position to Canvas local position
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                transform.Find("Button").gameObject.GetComponentInParent<RectTransform>(),
                characterScreenPosition,
                Camera.main,
                out UnityEngine.Vector2 localPoint
            );

            // Set the button's anchored position based on the character's position
            transform.Find("Button").gameObject.GetComponent<RectTransform>().anchoredPosition = localPoint;


        }*/
        
    }

    public void changePathEnd(GameObject endpoint)    
    {
        if(currentPathEnd != null)
        {
            Destroy(currentPathEnd);
            
        }
        GetComponent<NavMeshAgent>().SetDestination(endpoint.transform.position);
        currentPathEnd = endpoint;
       
    }

    IEnumerator DrawLine()
    {
        if(GetComponent<LineRenderer>()!= null)
        {
            while (!GetComponent<NavMeshAgent>().hasPath)
            {
                yield return null;
            }
            
        }
        yield return null;
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
