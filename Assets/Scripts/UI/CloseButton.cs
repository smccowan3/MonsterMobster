using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CloseButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(delegate { GameObject.Find("MainUI").GetComponent<UIManager>().closeThis(); });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
