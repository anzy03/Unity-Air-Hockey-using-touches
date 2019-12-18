using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    private float minX, maxX, minY, maxY;
    private float playerRadius;
    public float moveSpeed = 1f;
    public GameObject dice;
    public float dashSpeed = 3f;
    private bool hitDice;

    Vector2 startPos;
    Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        CapsuleCollider2D playerCollider = GetComponent<CapsuleCollider2D>();
        playerRadius = playerCollider.bounds.extents.x;

        // Getting Bottom Corner & Top Corner Of the Screen.
        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        // Giving Contrains Accoring to the Screen Size.
        minX = bottomCorner.x + playerRadius + 0.2f;
        maxX = topCorner.x - playerRadius - 0.2f;
        //Bottom Half
        minY = 0;
        maxY = topCorner.y - playerRadius - 0.2f;

        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectWithTag("AICollider").GetComponent<AIZone>().AiActive == true)//Gets bool from AI Zone Script.
        {
            if (dice.transform.position.y < transform.position.y && !hitDice)
            {
                //move towords the dice.
                transform.position = Vector2.MoveTowards(transform.position, dice.transform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                //move towords the goal.
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 4f), moveSpeed * Time.deltaTime);
            }

        }
        else
        {
            //Moves back to Start Position.
            transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
        }
        if (hitDice == true)
        {
            StartCoroutine(WaitTime(2f));
        }
          // Horizontal contraint
        if (transform.position.x < minX)
            transform.position = new Vector3(minX, transform.position.y);
        if (transform.position.x > maxX)
            transform.position = new Vector3(maxX, transform.position.y); 

        // vertical contraint
        if (transform.position.y < minY)
            transform.position = new Vector3(transform.position.x, minY);
        if (transform.position.y > maxY)
            transform.position = new Vector3(transform.position.x, maxY); 


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dice")
        {
            hitDice = true;
        }
    }

    IEnumerator WaitTime(float time)
    {
        yield return new WaitForSeconds(time);
        hitDice = false;
    }

}
