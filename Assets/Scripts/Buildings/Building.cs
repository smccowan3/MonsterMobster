using System.Collections;
using System.Collections.Generic;
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Building : MonoBehaviour
{
    [SerializeField] GameObject mainUI; //this is the building UI associated with this
    [SerializeField] List<GameObject> spawnables= new List<GameObject>(); //put spawnable stuff here
    private List<GameObject> spawnablesUI;
    public GameObject costUI;
    bool currentlySpawning;
    GameObject trainUI;
    GameObject nameUI;
    [SerializeField] string objectName;
    public int shelterSupplied;
    public List<AK.Wwise.Event> PlacementEvents;
    public bool spawner;
    public int buildingCost;
    // Start is called before the first frame update
    void Start()
    {
        if (spawner)
        {
            trainUI = mainUI.transform.Find("TrainUI").gameObject;
            nameUI = mainUI.transform.Find("NameText").gameObject;
            nameUI.GetComponent<TextMeshProUGUI>().text = objectName;
            costUI = mainUI.transform.Find("TrainUI/Cost").gameObject;
            spawnablesUI = new List<GameObject>();
            for (int i = 0; i < spawnables.Count; i++)
            {
                spawnablesUI.Add(mainUI.transform.Find("Spawnables").GetChild(i).gameObject);
                spawnablesUI[i].SetActive(true);
                spawnablesUI[i].GetComponent<SpawnableUI>().building = this;
                spawnablesUI[i].GetComponent<SpawnableUI>().spawnIndex = i;
                spawnablesUI[i].GetComponent<SpawnableUI>().associatedSpawn = spawnables[i].GetComponent<SpawnableObject>();
                //Texture2D previewTexture = AssetPreview.GetAssetPreview(spawnables[i]);
                spawnablesUI[i].GetComponent<Image>().sprite = spawnables[i].transform.Find("Thumbnail").GetComponent<SpriteRenderer>().sprite;
            }
            mainUI.transform.SetParent(GameObject.Find("MainUI").transform, false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCountdown(int spawnObjectIndex)
    {
        if (!currentlySpawning)
        {
            bool spawnable = FindAnyObjectByType<ShelterManage>().CheckIfShelterSufficient(spawnables[spawnObjectIndex].GetComponent<MonsterUnit>().shelterCost);
            if(spawnable)
            {
                FindAnyObjectByType<ShelterManage>().AddSubtractShelter(-spawnables[spawnObjectIndex].GetComponent<MonsterUnit>().shelterCost);
                StartCoroutine(BeginCountdown(spawnables[spawnObjectIndex], spawnObjectIndex));
                currentlySpawning = true;
            }
            else
            {
                FindAnyObjectByType<ShelterManage>().DisplayErrorUI(spawnables[spawnObjectIndex].GetComponent<MonsterUnit>().shelterCost);
            }
            
        }
        
    }


    IEnumerator BeginCountdown(GameObject spawnObject, int index)
    {
        float countUp = spawnObject.GetComponent<SpawnableObject>().trainTime;
        string originalValue = costUI.GetComponent<TextMeshProUGUI>().text;
        print("starting countdown");
        trainUI.GetComponentInChildren<TextMeshProUGUI>().text = Math.Round(countUp,2).ToString();
        costUI.SetActive(true);
        
        //trainUI.GetComponent<Image>().color = Color.green;
        while (countUp > 0)
        {
           
            yield return new WaitForSeconds(1f);
            countUp -= 1f;
            trainUI.GetComponentInChildren<TextMeshProUGUI>().text = Math.Round(countUp, 2).ToString();
        }

        //then spawn object
        Vector3 currentPos = transform.position;
        Quaternion currentRotation = transform.rotation;

        Vector3 newPos = currentPos + transform.forward*5;
        GameObject spawned = Instantiate(spawnObject, newPos, currentRotation);
        FindAnyObjectByType<VictoryLoseProgressBar>().ChangePopulation(1);
        spawned.GetComponent<MonsterUnit>().OnSpawnNoise();

        currentlySpawning = false;
        //trainUI.GetComponent<Image>().color = Color.red;
        trainUI.GetComponentInChildren<TextMeshProUGUI>().text = "Spawned!";
        yield return new WaitForSeconds(1f);
        trainUI.GetComponentInChildren<TextMeshProUGUI>().text = originalValue;
        spawnablesUI[index].GetComponent<SpawnableUI>().buttonToggle = false;
        yield return null;



    }

    public void OnPlace()
    {
        int randomSound = UnityEngine.Random.Range(0, PlacementEvents.Count);
        PlacementEvents[randomSound].Post(gameObject);
    }

}
