using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{


    private Aztec_Sprite aztec;
        

    void Start()
    {
        aztec = GetComponentInParent<Aztec_Sprite>();        
    }

    void Update()
    {
        transform.position = aztec.transform.position;
        transform.position = transform.position - new Vector3(0, 0.25f, 0);

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);
        transform.up = direction;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        GameObject collider = collision.gameObject;
        if (collider.CompareTag("Ahuizotl")) {
           // Debug.Log("Registered");
        }
    }
}
