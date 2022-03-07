using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy12 : MonoBehaviour
{
    private Animator anim;
    private Transform target;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float range;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        target=FindObjectOfType<PlayerController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
    public void FollowPlayer()
    {
        anim.SetBool("ism", true);
        anim.SetFloat("mx",target.position.x-transform.position.x);
        anim.SetFloat("my", target.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

    }
}
