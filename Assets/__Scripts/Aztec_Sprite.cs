using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Aztec_Sprite : MonoBehaviour
{
    [Header("Inscribed")]
    public HealthBar healthBar;

    public float speed = 5;
    public int maxHealth = 15;

    public int health;
    private Animator anim;
    public int dirFace = -1;
    private int dirHeld = -1;
    public bool godMode = false;
    private Rigidbody2D rigid;
    public bool damageable = true;
    private float timeToAttack = 12f;
    private float timer = 0f;
    Collider2D collide;
    private bool attacking = false;

    void Awake ()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (godMode) {
            damageable = false;
            damageable = false;
            collide = GetComponent<Collider2D>();
            collide.isTrigger = true;
        }
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }


    void Update()
    {
        if (godMode) {
            if (Input.GetKeyDown("space")) {
                speed += 1;
            }
        }

        if (health <= 0) {
            die();
        }

        if (attacking && timer > timeToAttack) {
            attacking = false;
            timer = 0;
        } else {timer += 1;}

        //Animation direction
        Vector3 mousePos = Input.mousePosition;
        float mX = mousePos.x;
        float mY = mousePos.y;

        //WASD movement controls
        dirHeld = -1;
        if (attacking) {attackAnimation();}
        else if(dirHeld == -1) {anim.speed = 0;}
        
        //Macuahuitl attack
        if (Input.GetMouseButtonDown(0)) {
            attacking = true;
            attackAnimation();
            Debug.Log("Click");
        } 
        else if (Input.GetMouseButtonDown(1)) {
            //right click for bow firing
        } 
        else {
            //Change direction to face mouse
            if (!attacking) {
                if ((mY > 0.5625 * mX) && (mY > -0.5625 * mX + 1080)) {
                    anim.Play("Walk_Up");
                    dirFace = 1;
                }
                else if ((mY > 0.5625 * mX) && (mY <= -0.5625 * mX + 1080)) {
                    anim.Play("Walk_Left");
                    dirFace = 2;
                }
                else if ((mY <= 0.5625 * mX) && (mY <= -0.5625 * mX + 1080)) {
                    anim.Play("Walk_Down");
                    dirFace = 3;
                }
                else if ((mY <= 0.5625 * mX) && (mY > -0.5625 * mX + 1080)) {
                    anim.Play("Walk_Right");
                    dirFace = 0;
                }

                if (Input.GetKey(KeyCode.D)) {
                    dirHeld = 0;
                    anim.speed = 1;
                }
                if (Input.GetKey(KeyCode.W)) {
                    dirHeld = 1;
                    anim.speed = 1;
                }
                if (Input.GetKey(KeyCode.A)) {
                    dirHeld = 2;
                    anim.speed = 1;
                }
                if (Input.GetKey(KeyCode.S)) {
                    dirHeld = 3;
                    anim.speed = 1;
                } 
            }
        }


        Vector2 vel = Vector2.zero;
        switch (dirHeld) {
            case 0: 
                vel = Vector2.right;
                break;
            case 1:
                vel = Vector2.up;
                break;
            case 2:
                vel = Vector2.left;
                break;
            case 3:
                vel = Vector2.down;
                break;
        }

        rigid.velocity = vel * speed;
    
    }

    void OnCollisionEnter2D(Collision2D collision) {
        
        GameObject collider = collision.gameObject;

        if (collider.CompareTag("Ahuizotl") && damageable) {
            //Debug.Log("Damage");
            health -= 1;
            healthBar.SetHealth(health);
        }
    } 
    

    void die() {
        SceneManager.LoadScene("Scene_Death");
    }

    void attackAnimation() {
        anim.speed = 1;
        if (dirFace == 0) {anim.Play("Attack_Right");}
        else if (dirFace == 1) {anim.Play("Attack_Up");}
        else if (dirFace == 2) {anim.Play("Attack_Left");}
        else if (dirFace == 3) {anim.Play("Attack_Down");}
    }
}
