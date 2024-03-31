using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class ServiceBuilding : MonoBehaviour
{
    public GameObject[] workers;
    int numWorkers;
    public ResourceManager manager;
    public string resourceObjectName;
    public string jobName;
    public AK.Wwise.Event entranceNoise;
    public GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
       manager = GameObject.Find("MainUI/Services/Resources/Objects/" + resourceObjectName).GetComponent<ResourceManager>();
       workers = new GameObject[100];
       numWorkers = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<MonsterUnit>() != null)
        {
            if(other.GetComponent<MonsterUnit>().job == jobName && FindAnyObjectByType<ClickableObjectManager>().currentlyDraggingObject != gameObject)
            {
                //other.GetComponent<ClickableObject>().changePathEnd(gameObject);
                
                other.GetComponent<NavMeshAgent>().SetDestination(transform.position);
                workers[numWorkers] = (other.gameObject);
                numWorkers++;
                manager.AddToResource(1);
                other.GetComponent<ClickableObject>().enabled = false;
                //destroy path
                Destroy(other.GetComponent<MonsterUnit>().linkedPath);
                other.GetComponent<LineRenderer>().positionCount = 0;
                entranceNoise.Post(gameObject);
                FindAnyObjectByType<VictoryLoseProgressBar>().policemen = numWorkers;
                updateUI();
            }
           
        }
    }




    public void removeWorker()
    {
        if(numWorkers > 0)
        {
            GameObject worker = workers[numWorkers-1];
            worker.GetComponent<NavMeshAgent>().SetDestination(transform.position +new Vector3(0, 0, -10));
            print("removing worker");
            worker.GetComponent<ClickableObject>().enabled = true;

            manager.AddToResource(-1);
          
            workers[numWorkers] = null;
            numWorkers--;
            FindAnyObjectByType<VictoryLoseProgressBar>().policemen = numWorkers;
            updateUI();
        }
       
    }


    void updateUI()
    {
        UI.GetComponent<TextMeshProUGUI>().text = "Workers: " + numWorkers.ToString();
        
    }


}
