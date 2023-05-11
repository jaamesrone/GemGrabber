using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesY : MonoBehaviour
{
    public GameObject bottomPoint;
    public GameObject topPoint;
    private Vector3 bottomPos;
    private Vector3 topPos;
    public int speed;
    public bool goingDown;

    // Start is called before the first grame update
    void Start()
    {
        bottomPos = bottomPoint.transform.position;
        topPos = topPoint.transform.position;
    }

    private void Move()
    {
        if (goingDown)
        {
            if (transform.position.y <= bottomPos.y)
            {
                goingDown = false;
            }
            else
            {
                transform.position += Vector3.down * Time.deltaTime * speed;
            }
        }
        else
        {
            if (transform.position.y >= topPos.y)
            {
                goingDown = true;
            }
            else
            {
                transform.position += Vector3.up * Time.deltaTime * speed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
