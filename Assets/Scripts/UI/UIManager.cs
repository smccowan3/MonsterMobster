using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject prioritisedUI;
    public GameObject mainPrioritisedUI;
    GameObject mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu = transform.Find("Main/MainMenu").gameObject;
        mainPrioritisedUI = mainMenu;
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
        
        if(newUI != null)
        {
            newUI.SetActive(true);
        }
        
        prioritisedUI = newUI;
    }

    public void reprioritiseMain(GameObject newUI)
    {
        if (mainPrioritisedUI != null)
        {
            mainPrioritisedUI.SetActive(false);
        }
        print("showing" + newUI.name);
        newUI.SetActive(true);
        mainPrioritisedUI = newUI;
    }

    public void closeThis()
    {
        if(prioritisedUI != null)
        {
            reprioritise(null);

        }

       
    }

    public void closeThisMain()
    {
        if (mainPrioritisedUI != null)
        {
            reprioritiseMain(mainMenu);

        }
    }

    public void ShowMainBuildingSpawnUI()
    {
        reprioritiseMain(transform.Find("Main/Residential Buildings").gameObject);
    }

}
