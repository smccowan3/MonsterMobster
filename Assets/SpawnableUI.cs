using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class SpawnableUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Building building;
    public SpawnableObject associatedSpawn;
    public int spawnIndex;
    public bool buttonToggle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void turnUIOn()
    {
        building.costUI.GetComponent<Text>().text = associatedSpawn.name + ": ¥" + associatedSpawn.cost.ToString();
        building.costUI.SetActive(true);
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
        turnUIOn();
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!buttonToggle) building.costUI.SetActive(false);
    }
    public void toggleOn()
    {
        buttonToggle = !buttonToggle;
        if (buttonToggle)
        {
            turnUIOn();
        }
        building.SpawnCountdown(spawnIndex);
        
    }

}
