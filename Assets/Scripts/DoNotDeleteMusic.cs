using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDeleteMusic : MonoBehaviour
{
    public static DoNotDeleteMusic BgInstance;

    void Awake()
    {
        if(BgInstance != null & BgInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        BgInstance = this;
        DontDestroyOnLoad(transform.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
