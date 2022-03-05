using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class e1 : MonoBehaviour
{
    public float speed;
    public float cheackradius;
    public float attackradius;

    public bool rt;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    public Vector3 dir;

    private bool isch;
    private bool isat;

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
        anim.SetBool("ism",isch);
        isch = Physics2D.OverlapCircle(transform.position, cheackradius, whatIsPlayer);
        isat = Physics2D.OverlapCircle(transform.position, attackradius, whatIsPlayer);

        dir = target.position - transform.position;
        float angle =  Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        
        dir.Normalize();
        movement = dir;
        if(rt)
        {
            anim.SetFloat("mx", dir.x);
            anim.SetFloat("my", dir.y);
        }
    }
    private void FixedUpdate()
    {
        if(isch&&isat)
        {
            MoveCharacter(movement);
        }
        if(isat)
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position+(dir*speed*Time.deltaTime));
    }
}
