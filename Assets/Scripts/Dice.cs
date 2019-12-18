using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    float paddleHit = 3.5f;
    float velocityMulti = 0.2f;
    float minVelocity = 3.5f;
    Vector2 lastRecordedVelocity;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        lastRecordedVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "paddle")
        {
            Vector2 transformVector = transform.position - col.transform.position;
            Vector2 paddleVelocity = velocityMulti * col.relativeVelocity;
           // float movingVelocity = col.gameObject.GetComponent<Player>().dashSpeed;
            float movingVelocity = col.gameObject.GetComponent<TouchMove>().dashSpeed;
            minVelocity += movingVelocity;
            paddleHit = minVelocity;
            paddleHit = Mathf.Clamp(paddleHit, 0.1f, 3.0f);
            rb.velocity = (transformVector + paddleVelocity) * paddleHit;
        }
        if (col.gameObject.tag == "AIpaddle")
        {
            Vector2 transformVector = transform.position - col.transform.position;
            Vector2 paddleVelocity = velocityMulti * col.relativeVelocity;
            float movingVelocity = col.gameObject.GetComponent<AI>().dashSpeed;
            minVelocity += movingVelocity;
            paddleHit = minVelocity;
            paddleHit = Mathf.Clamp(paddleHit, 0.1f, 3.0f);
            rb.velocity = (transformVector + paddleVelocity) * paddleHit;
        }

        if (col.gameObject.tag == "Vwall")
        {

            Vector2 reflect = Vector2.Reflect(lastRecordedVelocity, col.contacts[0].normal);

            //rb.velocity = reflect.normalized * paddleHit;
            rb.velocity = reflect;
        }
       // Debug.Log(col.gameObject.tag);
    }
}
