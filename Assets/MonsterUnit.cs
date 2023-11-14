using System.Collections;
using System.Collections.Generic;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.AI;

public class MonsterUnit : MonoBehaviour
{
    public string job;
    public bool busy;
    Animator slimeAnimator;
    NavMeshAgent agent;
    public int rayCount = 5; // Adjust the number of rays in the arc
    public float arcAngle = 45f; // Adjust the angle of the arc
    public float rayDistance = 10f; // Adjust the distance of the rays
    public float woodcuttingTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
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
        treeAnimator.SetTrigger("Cut Down");
        yield return new WaitForSeconds(woodcuttingTime/2);
        StartCoroutine(rotateTreeDependingOnPlayer(treeAnimator.gameObject));
        yield return new WaitForSeconds(woodcuttingTime / 2);
        Destroy(treeAnimator.gameObject);
        slimeAnimator.SetBool("Woodcutting", false);
        busy = false;
        print("+1 wood");
        yield return null;
    }

    IEnumerator rotateTreeDependingOnPlayer(GameObject tree)
    {
        // Get the initial rotation of the object
        Quaternion startRotation = tree.transform.rotation;

        // Calculate the target rotation away from the player
        Vector3 directionToPlayer = transform.position - tree.transform.position;
        // Project the direction onto the XZ plane to get the rotation axis
        Vector3 rotationAxis = Vector3.ProjectOnPlane(directionToPlayer, Vector3.up).normalized;

        // Calculate the target rotation by rotating around the calculated axis
        Quaternion targetRotation = Quaternion.AngleAxis(90f, rotationAxis) * startRotation;
        float rotationDuration = woodcuttingTime / 2;
        // Interpolate the rotation over time
        float elapsedTime = 0f;
        while (elapsedTime < rotationDuration)
        {
            tree.transform.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final rotation is exactly the target rotation
        tree.transform.rotation = targetRotation;
    }


}
