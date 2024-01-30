using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickableObjectManager : MonoBehaviour
{
    GameObject currentClickedObject;
    public GameObject currentlyDraggingObject;
    public float liftHeight;
    public float moveSpeed = 5f;
    public float smoothness = 0.5f;
    public GameObject mouseHoveringObject;
    public GridManager gridManager;
    public Grid grid;
    public GameObject movementIndicator;
    
    // Start is called before the first frame update
    void Start()
    {
        gridManager = GameObject.Find("BuildingSystem/MouseInputManager").GetComponent<GridManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {

                if (hit.collider.gameObject.GetComponent<ClickableObject>() != null)
                {
                    
                    changeClickedObject(hit.collider.gameObject);
                }
                else
                {
                    //changeClickedObject(null);
                }
            }

        }

        if (Input.GetMouseButton(1))
        {
            if (currentClickedObject != null)
            {
                if (currentClickedObject.GetComponent<NavMeshAgent>() != null)
                {
                    print("Setting path");
                    //currentClickedObject.GetComponent<NavMeshAgent>().SetDestination(GameObject.Find("MouseIndicator").transform.position);
                    GameObject endpoint = Instantiate(movementIndicator,GameObject.Find("MouseIndicator").transform.position, GameObject.Find("MouseIndicator").transform.rotation);
                    currentClickedObject.GetComponent<ClickableObject>().changePathEnd(endpoint);
                    endpoint.GetComponent<PathLinkerAndDeleter>().LinkObject(currentClickedObject);
                    changeClickedObject(null);
                }
            }
        }


        if(currentlyDraggingObject != null)
        {
            Vector3 mousePos = gridManager.GetSelectedMapPosition();
           
            Vector3Int gridPosition = grid.WorldToCell(mousePos);
            Vector3 newPosition = grid.CellToWorld(gridPosition);
            newPosition.y = 0.1f;
            // Move the object towards the mouse position
            currentlyDraggingObject.transform.position = Vector3.Lerp(currentlyDraggingObject.transform.position, newPosition, moveSpeed*smoothness*Time.deltaTime);

            if (Input.GetMouseButtonDown(0))
            {
                //we have placed the object
                if(currentlyDraggingObject.GetComponentInParent<Building>() != null)
                {
                    print("play building sound");
                    currentlyDraggingObject.GetComponentInParent<Building>().OnPlace();
                }
                currentlyDraggingObject = null; //place object here
                changeClickedObject(null);
            }
            if(Input.GetKeyDown(KeyCode.R))
            {
                currentlyDraggingObject.transform.Rotate(0, 90, 0);
            }

        }

    }

    public void setMouseHovering(GameObject newObject)
    {
        mouseHoveringObject = newObject;
    }


    public GameObject getMouseHovering()
    {
        return mouseHoveringObject;
    }

    public void changeClickedObject(GameObject newObject)
    {
        if(currentClickedObject != null)
        {
            currentClickedObject.GetComponent<ClickableObject>().hideUI();
           
        }
       
        currentClickedObject = newObject;
        
        if (newObject != null)
        {
            if (currentClickedObject.GetComponentInParent<Building>())
            {
                currentClickedObject.GetComponentInParent<Building>().OnPlace();
            }
            if(currentClickedObject.GetComponent<MonsterUnit>()!= null)
            {
                currentClickedObject.GetComponent<MonsterUnit>().OnClickVoice();
            }

            newObject.GetComponent<ClickableObject>().showUI();
        }
        
    }

    public void moveDragObject(GameObject newObject)
    {
        currentlyDraggingObject = newObject;

        /*if (currentlyDraggingObject == null)
        {
            
        }*/
    }


}
