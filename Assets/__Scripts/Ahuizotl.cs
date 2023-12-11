using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ahuizotl : MonoBehaviour
{

    [Header("Inscribed")]
    public float speed = 3.5f;
    public float health = 3f;
    public float damage = 1;
    public float knockbackForce = 20f;

    public Sprite[] sprites = new Sprite[4];
    public float angle;
    public float distance = 0.0f;
    private Animator anim;
    private Rigidbody2D rigid;
    private Vector2 movement;
    private bool canMove = true;


    private bool seePlayer = false;

    public Transform player;
    public GameObject playerObj;
    public UnityEvent OnBegin, OnDone;
    
    
    [Header("Dynamic")]
    [Range(0,4)]
    public int dirFace = 0;

    void Awake()
    {
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.transform;
        rigid = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
    }

    void Update()
    {

        if (health <= 0) {
            Destroy(gameObject);
        }

        if (distance > 3) {canMove = true;}
        if (!seePlayer) {anim.speed = 0;}

        Vector3 direction = player.position - transform.position;
        distance = Mathf.Sqrt((direction.x * direction.x + direction.y * direction.y));
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (angle < 0) {
            angle += 360;
        }

        if ((angle < 32.23 || angle >= 327.77) && distance < 12) {
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            dirFace = 0;
        } else if (angle >= 32.23 && angle < 147.23 && distance < 12) {
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            dirFace = 1;
        } else if (angle >= 147.23 && angle < 212.23 && distance < 12) {
            GetComponent<SpriteRenderer>().sprite = sprites[2];
            dirFace = 2;
        } else if (distance < 12) {
            GetComponent<SpriteRenderer>().sprite = sprites[3];
            dirFace = 3;
        }

        anim.Play("Dog_Walk_" + dirFace);
        if (seePlayer) {
            anim.speed = 1;
        }

        


        direction.Normalize();
        movement = direction;
    }

    private void FixedUpdate() {
        if (canMove) {moveCharacter(movement);}
    }

    void moveCharacter(Vector2 direction) {
        rigid.MovePosition((Vector2)transform.position + (direction * speed * Time.deltaTime));
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        canMove = false;
        GameObject collider = collision.gameObject;

        if (collider.CompareTag("Player")) {
            PlayFeedback(collider);
        }
    } 
    

    void OnHit(Vector2 knockback) {
        rigid.AddForce(knockback, ForceMode2D.Impulse);
    }
    void PlayFeedback(GameObject sender) {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rigid.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }

    private IEnumerator Reset() {
        yield return new WaitForSeconds(0.5f);
        rigid.velocity = Vector3.zero;
        OnDone?.Invoke();
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("AttackArea")) {health -= 2;}
    }
}
