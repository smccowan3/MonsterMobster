using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShelterManage : MonoBehaviour
{
    int shelterAmount;
    public TextMeshProUGUI shelterText;
    public GameObject errorGUI;
    // Start is called before the first frame update
    void Start()
    {
        shelterAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddSubtractShelter(int shelterToAdd)
    {
        shelterAmount += shelterToAdd;
        SyncToUI();
    }

    public bool CheckIfShelterSufficient(int amountToCheck)
    {
        return amountToCheck <= shelterAmount;
    }

    void SyncToUI()
    {
        shelterText.text = shelterAmount.ToString();
    }

    public void DisplayErrorUI(int cost)
    {
        errorGUI.GetComponentInChildren<TextMeshProUGUI>().text = "Not enough Shelter\n" + cost.ToString() + " required!";
        errorGUI.SetActive(true);
        StartCoroutine(hide());
    }

    IEnumerator hide()
    {
        yield return new WaitForSeconds(2);
        errorGUI.SetActive(false);
    }
}
