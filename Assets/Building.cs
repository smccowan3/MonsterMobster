using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{
    [SerializeField] GameObject mainUI; //this is the building UI associated with this
    [SerializeField] List<GameObject> spawnables= new List<GameObject>(); //put spawnable stuff here
    private List<GameObject> spawnablesUI;
    public GameObject costUI;
    bool currentlySpawning;
    GameObject trainUI;

    // Start is called before the first frame update
    void Start()
    {
        trainUI = mainUI.transform.Find("TrainUI").gameObject;
        trainUI.GetComponent<Image>().color = Color.red; //idle color
        trainUI.transform.Find("Text").gameObject.SetActive(false);
        costUI = mainUI.transform.Find("Cost").gameObject;
        spawnablesUI = new List<GameObject>();
        for (int i = 0; i < spawnables.Count; i++)
        {
            spawnablesUI.Add(mainUI.transform.Find("Spawnables").GetChild(i).gameObject);
            spawnablesUI[i].SetActive(true);
            spawnablesUI[i].GetComponent<SpawnableUI>().building = this;
            spawnablesUI[i].GetComponent<SpawnableUI>().spawnIndex = i;
            spawnablesUI[i].GetComponent<SpawnableUI>().associatedSpawn = spawnables[i].GetComponent<SpawnableObject>();
            Texture2D previewTexture = AssetPreview.GetAssetPreview(spawnables[i]);
            spawnablesUI[i].GetComponent<Image>().sprite = Sprite.Create(previewTexture, new Rect(0,0, previewTexture.width, previewTexture.height), Vector2.zero);
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
            StartCoroutine(BeginCountdown(spawnables[spawnObjectIndex], spawnObjectIndex));
            currentlySpawning = true;
        }
        
    }


    IEnumerator BeginCountdown(GameObject spawnObject, int index)
    {
        float countUp = spawnObject.GetComponent<SpawnableObject>().trainTime;
        trainUI.transform.Find("Text").GetComponent<Text>().text = countUp.ToString();
        trainUI.transform.Find("Text").gameObject.SetActive(true); 
        trainUI.GetComponent<Image>().color = Color.green;
        while (countUp > 0)
        {
           
            yield return new WaitForSeconds(0.05f);
            countUp -= 0.05f;
            trainUI.transform.Find("Text").GetComponent<Text>().text = countUp.ToString();
        }

        //then spawn object
        Vector3 currentPos = transform.position;
        Quaternion currentRotation = transform.rotation;

        Vector3 newPos = currentPos + transform.forward*5;
        Instantiate(spawnObject, newPos, currentRotation);
        trainUI.transform.Find("Text").gameObject.SetActive(false);

        currentlySpawning = false;
        trainUI.GetComponent<Image>().color = Color.red;
        costUI.SetActive(false);
        spawnablesUI[index].GetComponent<SpawnableUI>().buttonToggle = false;
        yield return null;



    }

}
