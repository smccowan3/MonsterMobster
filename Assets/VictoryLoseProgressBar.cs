using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
public class VictoryLoseProgressBar : MonoBehaviour
{
    string state;
    public GameObject VictoryScreen;
    public GameObject LossScreen;

    public GameObject lossLiquid;
    public GameObject winLiquid;
    public float startingScale = 0.1f;

    public int populationRequirement = 20;
    public float gainSpeed = 5; //Rate  per minute

    int populationValue;
    float gainValue;

    float maxGain = 100;

    public float policemen;


    // Start is called before the first frame update
    void Start()
    {
        policemen = 0;
        state = "Playing";
        lossLiquid.transform.localScale = new Vector3(1, startingScale, 1);
        winLiquid.transform.localScale = new Vector3(1, startingScale, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == "Win" || state == "Loss")
        {
            if(state == "Win") VictoryScreen.SetActive(true);
            if(state == "Loss") LossScreen.SetActive(true);
            int i = 0;
            for (i = 0; i < 1000000; i++)
            {
                //lets wait for a bit
            }

            if(Input.GetMouseButton(0))
            {
                SceneManager.LoadScene(0);
            }
        }
        else if (state == "Playing")
        {
            
            gainValue += (float)Time.deltaTime * gainSpeed / 60f * Mathf.Pow(0.9f, policemen);
            
            lossLiquid.transform.localScale = Vector3.Lerp(new Vector3(1, startingScale, 1), new Vector3(1, 1.3f, 1), (float)gainValue / (float)maxGain);
            if (gainValue > maxGain)
            {
                state = "Loss";
            }
            else if (populationValue > populationRequirement)
            {
                state = "Win";
            }

        }
    }

    public void ChangePopulation(int population)
    {
        populationValue += population;
        winLiquid.transform.localScale = Vector3.Lerp(new Vector3(1, startingScale, 1), new Vector3(1, 1.3f, 1), (float)populationValue / (float)populationRequirement);
    }

}
