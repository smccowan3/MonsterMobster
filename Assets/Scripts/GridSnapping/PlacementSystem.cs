using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField] GameObject mouseIndicator, cellIndicator;
    [SerializeField] GridManager gridManager;
    [SerializeField] Grid grid;
    [SerializeField] ClickableObjectManager clickableObjectManager;

    private void Start()
    {
        float gridSize = grid.cellSize.x;
        Vector3 newscale = new Vector3(gridSize, gridSize, 1.0f);
        cellIndicator.transform.localScale = newscale;
        clickableObjectManager = GameObject.Find("ClickableObjectManager").GetComponent<ClickableObjectManager>();
    }

    private void Update()
    {
        Vector3 mousePosition = gridManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if (mouseIndicator != null)
        {
            mouseIndicator.transform.position = mousePosition;
        }
        GameObject mouseHoveringObject = clickableObjectManager.getMouseHovering();
        if(mouseHoveringObject != null)
        {
           
            Vector3 newPosition = mouseHoveringObject.transform.parent.position;

            //right now 1.25 is for 3 grid pixels
            float xScaleF = mouseHoveringObject.GetComponent<ClickableObject>().gridSize.x / 3 * 1.25f;
            float zScaleF = mouseHoveringObject.GetComponent<ClickableObject>().gridSize.z / 3 * 1.25f;

            Vector3 newScale = new Vector3(xScaleF, zScaleF, 1);
            cellIndicator.transform.localScale = newScale;
            newPosition.y = 0.1f;
            cellIndicator.transform.position = newPosition;

        }
        else
        {
            
            Vector3 newPosition = grid.CellToWorld(gridPosition);
            newPosition.y = 0.1f;
            cellIndicator.transform.position = newPosition;

            cellIndicator.transform.localScale = new Vector3(1,1,1);
        }

 

        Quaternion rotation = Quaternion.Euler(90, Camera.main.transform.eulerAngles.y, 0);
        cellIndicator.transform.rotation = rotation;

    }
}
