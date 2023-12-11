using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackArea = default;
    private bool attacking = false;
    private float timeToAttack = 0.25f;
    private float timer = 0f;
        

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        attackArea.SetActive(attacking);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) {
            attack();
        }
        if(attacking) {
            timer += Time.deltaTime;

            if (timer >= timeToAttack) {
                timer = 0;
                attacking = false;
                attackArea.SetActive(attacking);
            }
        }
    }

    private void attack() {
        attacking = true;
        attackArea.SetActive(attacking);
    }
}
