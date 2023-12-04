using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;

    public Vector2 movement;
    public Vector3 dir;

    public float Health 
    { 
        set
        {
            skelHealth = value;

            if (skelHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
        get
        {
            return skelHealth;
        }
    }
    public float skelHealth = 2;

    private bool isInChaseRange;
    private bool isInAttackRange;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isRunning", isInChaseRange);
        anim.SetBool("isIdle", isInAttackRange);
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, whatIsPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if (shouldRotate)
        {
            anim.SetFloat("Horizontal", dir.x);
            anim.SetFloat("Vertical", dir.y);
        }
    }

    private void FixedUpdate()
    {
        if (isInChaseRange && !isInAttackRange)
        {
            MoveCharacter(movement);
        }
        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }

    void OnHit(float damage)
    {
        skelHealth -= damage;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        OnHit(skelHealth);
    }
}
