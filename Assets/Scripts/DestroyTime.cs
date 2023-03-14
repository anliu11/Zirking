using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waitUntilDestroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator waitUntilDestroy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
