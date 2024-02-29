using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ParticleSystemJobs;

public class MonsterUnit : MonoBehaviour
{
    public string job;
    public bool busy;
    Animator slimeAnimator;
    NavMeshAgent agent;
    public int rayCount = 5; // Adjust the number of rays in the arc
    public float arcAngle = 45f; // Adjust the angle of the arc
    public float rayDistance = 10f; // Adjust the distance of the rays

    //woodcutting
    public float woodcuttingTime = 10f;
    public float treeDropDistance = 2f;


    public int shelterCost;

    public AK.Wwise.Event randomVoice;

    public AK.Wwise.Event spawnNoise;

    // Start is called before the first frame update
    void Start()
    {
        job = null;
        busy = false;
        slimeAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    /*void CastWideArcRays()
    {
        float startAngle = -arcAngle / 2f; // Starting angle of the arc
        float angleStep = arcAngle / (float)(rayCount - 1); // Angle between each ray

        for (int i = 0; i < rayCount; i++)
        {
            // Calculate the direction of the ray based on the current angle
            float angle = startAngle + i * angleStep;
            Vector3 rayDirection = Quaternion.Euler(0f, angle, 0f) * transform.forward;

            // Cast a ray in the calculated direction
            Ray ray = new Ray(transform.position + new Vector3(0,1,0), rayDirection);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

            // Perform other logic based on the ray hit if needed
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance))
            {
                // Handle the hit, e.g., print the name of the hit object
                Debug.Log("Hit: " + hit.collider.gameObject.name);
            }

            if (GameObject.Find("TreeMaterials").GetComponent<TreeMats>().treeMaterials.Contains(hit.collider.GetComponentInChildren<MeshRenderer>().material))
            {
                Debug.Log("Player is over a tree on the terrain!");
            }
        }
    }*/

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
        else 
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

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree") && job == "Woodcutting"){
            
            collision.gameObject.GetComponent<Animator>().SetTrigger("Cut Down");
            
        }
    }*/

    public void startCut(Animator treeAnimator)
    {
        StartCoroutine(waitForCut(treeAnimator));
    }

    IEnumerator waitForCut(Animator treeAnimator)
    {
        //treeAnimator.SetTrigger("Cut Down");
        
        //StartCoroutine(rotateTreeDependingOnPlayer(treeAnimator.gameObject));
        StartCoroutine(fadeTreeOut(treeAnimator.gameObject));
        //yield return new WaitForSeconds(woodcuttingTime / 2);
        
        
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
        FindAnyObjectByType<WoodManager>().AddSubtractWood(10);
        yield return null;

    }


    IEnumerator rotateTreeDependingOnPlayer(GameObject tree)
    {
        //also move tree
        // Get the initial rotation of the object
        Quaternion startRotation = tree.transform.rotation;
        Vector3 startPosition = tree.transform.position;

        // Calculate the target rotation away from the player
        Vector3 directionToPlayer = transform.Find("Body").position - tree.transform.position;
        // Project the direction onto the XZ plane to get the rotation axis
        Vector3 rotationAxis = Vector3.ProjectOnPlane(directionToPlayer, Vector3.up).normalized;

        // Calculate the target rotation by rotating around the calculated axis
        Quaternion targetRotation = Quaternion.AngleAxis(90f, rotationAxis) * startRotation;
        
        float rotationDuration = woodcuttingTime / 2;
        // Interpolate the rotation over time
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            if (tree != null)
            {
                Vector3 moveDirection = -directionToPlayer.normalized;
                Vector3 targetPosition = startPosition + moveDirection * treeDropDistance;
                tree.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / rotationDuration);
                tree.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
         
        // Ensure the final rotation is exactly the target rotation
        tree.transform.rotation = targetRotation;
        tree.transform.position = startPosition + -directionToPlayer.normalized * treeDropDistance;
        Destroy(tree);
        yield return null;
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
