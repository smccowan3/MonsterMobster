using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{

    public Texture2D mouseCursor;
    public Vector2 offset;
    // Start is called before the first frame update
    void Awake()
    {
        Cursor.SetCursor(mouseCursor,offset, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
