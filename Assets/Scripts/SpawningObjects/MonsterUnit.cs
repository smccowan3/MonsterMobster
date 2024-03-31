using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ParticleSystemJobs;
using UnityEngine.UI;
using TMPro;
public class MonsterUnit : MonoBehaviour
{
    public string job;
    bool automatic;
    public void toggleAutomatic(GameObject button) { automatic = !automatic; if (automatic) button.GetComponent<Image>().color = Color.green; else button.GetComponent<Image>().color = Color.white; } 
    public bool busy;
    Animator slimeAnimator;
    NavMeshAgent agent;
    public int rayCount = 5; // Adjust the number of rays in the arc
    public float arcAngle = 45f; // Adjust the angle of the arc
    public float rayDistance = 10f; // Adjust the distance of the rays

    //woodcutting
    public float woodcuttingTime = 10f;
    public float treeDropDistance = 2f;

    public int shelterCost; //this is how much shelter it costs to summon this unit
    public GameObject linkedPath;

    public int voicePitch;

    //audio
    public AK.Wwise.Event randomVoice;
    public AK.Wwise.Event spawnNoise;
    public AK.Wwise.Event deathNoise;

   


    // Start is called before the first frame update
    void Start()
    {
        job = null;
        busy = false;
        slimeAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        voicePitch = Random.Range(0, 100);
        AkSoundEngine.SetRTPCValue("Pitch", voicePitch, gameObject);
        AkSoundEngine.SetRTPCValue("Pitch", voicePitch, transform.Find("ButtonClickSound").gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<TreeDetection>() == null)
        {
            Debug.LogError("No tree detection component attached");
        }
        else if (busy)
        {
            
        }
        else if (job == "Woodcutting" && automatic)
        {
            Vector3 targetPosition = GetComponent<TreeDetection>().findNearestTree();
            GetComponent<NavMeshAgent>().SetDestination(targetPosition);
        }
        if(job == "Woodcutting")
        {
            
            GetComponent<TreeDetection>().CheckTrees(this);
        }


    }

    public void treeHit(int treeIndex, Vector3 treeWorldPosition)
    {
        if(job == "Woodcutting")
        {
            slimeAnimator.SetBool("Woodcutting", true);
            GameObject.Find("WoodcuttingManager").GetComponent<CutTreeReplacer>().RemoveAndReplaceTree(this, treeIndex, treeWorldPosition);
            busy = true;
        }
    }

    public void killUnit()
    {
        FindAnyObjectByType<ShelterManage>().AddSubtractShelter(shelterCost);
        FindAnyObjectByType<VictoryLoseProgressBar>().ChangePopulation(-1);
        deathNoise.Post(gameObject);
        Destroy(gameObject);
    }

    public void startCut(Animator treeAnimator)
    {
        StartCoroutine(waitForCut(treeAnimator));
    }

    IEnumerator waitForCut(Animator treeAnimator)
    {
        
        StartCoroutine(fadeTreeOut(treeAnimator.gameObject));
        
        
        yield return null;
    }

    IEnumerator fadeTreeOut(GameObject tree)
    {
        
        yield return new WaitForSeconds(woodcuttingTime / 2);
        float fadeSpeed = woodcuttingTime/2;
        float currTime = 0;
        Renderer rend = tree.GetComponentInChildren<Renderer>();
        
        //Color color = rend.material.color;
        //Color newAlpha = new Color(color.r, color.g, color.b, color.a);
        while (currTime < fadeSpeed)
        {
            currTime += Time.deltaTime;
            Transform[] trees = tree.GetComponentsInChildren<Transform>();
            for (int i = 0; i < trees.Length; i++)
            {
                trees[i].transform.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(0.8f, 0.8f, 0.8f), currTime / fadeSpeed);
                //Debug.Log($"Tree {i + 1} Scale: {trees[i].transform.localScale}");
                
            }
            /*for(int i = 0; i< rend.materials.Length; i++)
            {
                rend.materials[i].SetFloat("_CutOff", Mathf.Lerp(0, 1, currTime / fadeSpeed));
            }*/
            yield return null;

        }
        Destroy(tree);
        slimeAnimator.SetBool("Woodcutting", false);
        busy = false;
        print("+1 wood");
        int woodmodifier = int.Parse(GameObject.Find("MainUI/Services/Resources/Objects/Tavern").GetComponentInChildren<TextMeshProUGUI>().text);
        FindAnyObjectByType<WoodManager>().AddSubtractWood(10+woodmodifier);
        yield return null;

    }

    public void BecomeTaverner()
    {
        if(!busy)
        {
            if(job != null)
            {
                transform.Find("Body/" + job + "Hat").gameObject.SetActive(false);
            }
            job = "Taverner";
            transform.Find("Body/TavernerHat").gameObject.SetActive(true);

        }
    }

    public void BecomePolice()
    {
        if (!busy)
        {
            if (job != null)
            {
                transform.Find("Body/" + job + "Hat").gameObject.SetActive(false);
            }
            job = "Police";
            transform.Find("Body/PoliceHat").gameObject.SetActive(true);

        }
    }


    public void BecomeWoodcutter()
    {
        if (!busy)
        {
            if (job != null)
            {
                transform.Find("Body/" + job + "Hat").gameObject.SetActive(false);
            }
            job = "Woodcutting";
            transform.Find("Body/WoodcuttingHat").gameObject.SetActive(true);
        }
        
    }
    public void NoJob()
    {
        if (!busy)
        {
            if (job != null)
            {
                transform.Find("Body/" + job + "Hat").gameObject.SetActive(false);
            }
            job = null;

        }

    }

    public void OnClickVoice()
    {
        float roll = Random.Range(1, 101);
        if(roll > 30)
        {
            randomVoice.Post(gameObject);
        }
        
    }

    public void OnSpawnNoise()
    {
        spawnNoise.Post(gameObject);
    }



}
