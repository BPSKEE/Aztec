using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{

    public Transform player;
    public Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        offset.z = -10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3 (player.position.x + offset.x, player.position.y + offset.y, offset.z);
    }
}
