using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class SpawnBuilding : MonoBehaviour
{
    [SerializeField] GameObject building;
    // Start is called before the first frame update
    void Start()
    {
        Texture2D previewTexture = AssetPreview.GetAssetPreview(building);
        GetComponent<Image>().sprite = Sprite.Create(previewTexture, new Rect(0, 0, previewTexture.width, previewTexture.height), Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spawnBuilding()
    {
        GameObject bldg = Instantiate(building, Input.mousePosition, new Quaternion (0,0,0,0));
        GameObject.Find("ClickableObjectManager").GetComponent<ClickableObjectManager>().moveDragObject(bldg);
    }

    
}
