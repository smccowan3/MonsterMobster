using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject prioritisedUI;
    // Start is called before the first frame update
    void Start()
    {
        //prioritisedUI = GameObject.Find("MainSpawnUI");
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

        newUI.SetActive(true);
        prioritisedUI = newUI;
    }

    public void closeThis()
    {
        if(prioritisedUI != null)
        {
            prioritisedUI.SetActive(false);
            prioritisedUI = null;
        }
       
    }

    public void ShowMainBuildingSpawnUI()
    {
        reprioritise(transform.Find("MainSpawnUI").gameObject);
    }

}
