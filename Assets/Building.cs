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
    // Start is called before the first frame update
    void Start()
    {
        
        //Vector3 newLocation = GameObject.Find("MainUI/MiddleLocation").GetComponent<RectTransform>().position;
        //mainUI.GetComponent<RectTransform>().position = newLocation;
        //mainUI.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
        trainUI = mainUI.transform.Find("TrainUI").gameObject;
        nameUI = mainUI.transform.Find("NameText").gameObject;
        nameUI.GetComponent<TextMeshProUGUI>().text = objectName;
        //trainUI.GetComponent<Image>().color = Color.red; //idle color
        //trainUI.transform.Find("Text").gameObject.SetActive(false);
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnCountdown(int spawnObjectIndex)
    {
        if (!currentlySpawning)
        {
            StartCoroutine(BeginCountdown(spawnables[spawnObjectIndex], spawnObjectIndex));
            currentlySpawning = true;
        }
        
    }


    IEnumerator BeginCountdown(GameObject spawnObject, int index)
    {
        float countUp = spawnObject.GetComponent<SpawnableObject>().trainTime;
        string originalValue = costUI.GetComponent<TextMeshProUGUI>().text;
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
        Instantiate(spawnObject, newPos, currentRotation);
        

        currentlySpawning = false;
        //trainUI.GetComponent<Image>().color = Color.red;
        trainUI.GetComponentInChildren<TextMeshProUGUI>().text = "Spawned!";
        yield return new WaitForSeconds(1f);
        trainUI.GetComponentInChildren<TextMeshProUGUI>().text = originalValue;
        spawnablesUI[index].GetComponent<SpawnableUI>().buttonToggle = false;
        yield return null;



    }

}
