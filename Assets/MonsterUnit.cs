using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterUnit : MonoBehaviour
{
    public string job;
    Animator slimeAnimator;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        slimeAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tree") && job == "Woodcutting"){
            slimeAnimator.SetBool("Woodcutting", true);
            collision.gameObject.GetComponent<Animator>().SetTrigger("Cut Down");
            StartCoroutine(waitForCut(collision.gameObject.GetComponent<Animator>()));
        }
    }

    IEnumerator waitForCut(Animator treeAnimator)
    {
        yield return new WaitForSeconds(treeAnimator.GetCurrentAnimatorStateInfo(0).length);

        Destroy(treeAnimator.gameObject);
        slimeAnimator.SetBool("Woodcutting", false);
        yield return null;
    }
}
