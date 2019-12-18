using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    bool mouseButtonPress;
    private Vector3 offset;
    private float moveSpeed = 5;
    private float minX, maxX, minY, maxY;
    private float playerRadius;
    public bool player2;
    Rigidbody2D rb;

    Vector2 startPos;
    float posDelay = 0.5f;
    float currentTime;

    public float dashSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //CircleCollider2D playerCollider = GetComponent<CircleCollider2D>();
        BoxCollider2D playerCollider = GetComponent<BoxCollider2D>();
        playerRadius = playerCollider.bounds.extents.x;


        float camDistance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector2 bottomCorner = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, camDistance));
        Vector2 topCorner = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, camDistance));

        minX = bottomCorner.x + playerRadius + 0.2f;
        maxX = topCorner.x - playerRadius - 0.2f;
        if (!player2)
        {
            minY = bottomCorner.y + playerRadius + 0.2f;
            maxY = 0;
        }
        else
        {
            minY = 0;
            maxY = topCorner.y - playerRadius - 0.2f;
        }
    }

    private void Update()
    {

        transform.Translate(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Time.deltaTime * moveSpeed);

        if (mouseButtonPress == true && Time.timeSinceLevelLoad > currentTime)
        {
            startPos = transform.position;
            currentTime = Time.timeSinceLevelLoad + posDelay;
        }

    }
    private void OnMouseUp()
    {
        mouseButtonPress = false;
    }

    void OnMouseDown()
    {
        mouseButtonPress = true;
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));
    }

    void OnMouseDrag()
    {

        float mousePositionX = Input.mousePosition.x;
        float mousePositionY = Input.mousePosition.y;

        Vector3 newPosition = new Vector3(mousePositionX, mousePositionY , 10.0f);
        transform.position = Camera.main.ScreenToWorldPoint(newPosition) + offset;

        // Horizontal contraint
        if (transform.position.x < minX)
            transform.position = new Vector3(minX, transform.position.y);
        if (transform.position.x > maxX)
            transform.position = new Vector3(maxX, transform.position.y);

        // vertical contraint
        if (transform.position.y < minY)
            transform.position = new Vector3(transform.position.x , minY );
        if (transform.position.y > maxY)
            transform.position = new Vector3(transform.position.x, maxY);

        dashSpeed = Vector2.Distance(transform.position, startPos) * 2f;
       
    }

   /* private void OnCollisionEnter2D(Collision2D col)
    {
        
        //Debug.Log(col.gameObject.name);
    }*/
}
