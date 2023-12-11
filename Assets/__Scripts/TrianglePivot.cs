using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrianglePivot : MonoBehaviour
{
    // Start is called before the first frame update
    private Aztec_Sprite aztec;
    void Start()
    {
        aztec = GetComponentInParent<Aztec_Sprite>();        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = aztec.transform.position;

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;
    }
}
