using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    public float smooth = 0.3f;
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset = new Vector3(0, 15, 0);

    //Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
 
    }
}
