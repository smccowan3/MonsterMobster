using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WoodManager : MonoBehaviour
{
    int woodAmount;
    public TextMeshProUGUI woodtext;
    public GameObject errorGUI;
    // Start is called before the first frame update
    void Start()
    {
        woodAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddSubtractWood(int woodToAdd)
    {
        woodAmount += woodToAdd;
        SyncToUI();
    }

    public bool CheckIfWoodSufficient(int amountToCheck)
    {
        return amountToCheck <= woodAmount;
    }

    void SyncToUI()
    {
        woodtext.text = woodAmount.ToString();
    }

    public void DisplayErrorUI(int cost)
    {
        errorGUI.GetComponentInChildren<TextMeshProUGUI>().text = "Wood insufficient\n" + cost.ToString() + " required!\nCut down more trees";
        errorGUI.SetActive(true);
        StartCoroutine(hide());
    }

    IEnumerator hide()
    {
        yield return new WaitForSeconds(2);
        errorGUI.SetActive(false);
    }

}
