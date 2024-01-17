using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject prioritisedUI;
    GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = transform.Find("Main/MainMenu").gameObject;
        prioritisedUI = mainMenu;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reprioritise(GameObject newUI)
    {
        if (prioritisedUI != null)
        {
            prioritisedUI.SetActive(false);
        }
        print("showing" + newUI.name);
        newUI.SetActive(true);
        prioritisedUI = newUI;
    }

    public void closeThis()
    {
        if(prioritisedUI != null)
        {
            reprioritise(mainMenu);

        }

       
    }

    public void ShowMainBuildingSpawnUI()
    {
        reprioritise(transform.Find("Main/Residential Buildings").gameObject);
    }

}
