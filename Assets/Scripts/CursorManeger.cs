using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManeger : MonoBehaviour
{
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private Texture2D pointerCursor;

    private Vector2 cursorHotSpot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CrossHairCursor()
    {
        cursorHotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(cursorTexture, cursorHotSpot, CursorMode.Auto);
    }

    public void PointerCursor()
    {
        cursorHotSpot = new Vector2(cursorTexture.width / 2, cursorTexture.height / 2);
        Cursor.SetCursor(pointerCursor, cursorHotSpot, CursorMode.Auto);


    }
}
