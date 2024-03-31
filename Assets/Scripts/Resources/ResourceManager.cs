using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ResourceManager : MonoBehaviour
{
    public GameObject UI;
    public string resourceName;
    int amount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToResource(int add)
    {
        amount += add;
        updateUI();
    }

    void updateUI()
    {
        UI.GetComponent<TextMeshProUGUI>().text = amount.ToString();
    }
}
