using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wmo : MonoBehaviour
{
    public bool faceRight = true;
    public float speed;
    private Vector2 direction;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");

        if (faceRight == false && direction.x < 0)
        {
            Flip();
        }

        if (faceRight == true && direction.x> 0)
        {
            Flip();
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
    void Flip()
    {
        faceRight = !faceRight;

        transform.Rotate(0f, 180f, 0f);
    }
}
