using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Queue<Vector3> pathPosition = new Queue<Vector3>();
    public bool ready = false;

    Transform target, backup;
    public float speed = 0.000001f;
    bool walking = false;
    string mouseDirection = "Down";
    Vector3 beginPos = new Vector3();
    Quaternion beginRotation = new Quaternion();

    // Start is called before the first frame update
    void Start()
    {
        target = transform;
        backup = transform;
        beginPos = transform.position;
        beginRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (ready)
        {
            if (!walking)
            {
                StartCoroutine(Walk(0.2f));
            }
            
        }

       
    }

    IEnumerator Walk(float delayTime)
    {
        walking = true;
        yield return new WaitForSeconds(delayTime);

        if (pathPosition.Count != 0)
        {
            if(pathPosition.Peek().x > transform.position.x && pathPosition.Peek().z == transform.position.z && mouseDirection != "Left")
            {
                mouseDirection = "Left";
                tradeDirection();
            }
            else if (pathPosition.Peek().x < transform.position.x && pathPosition.Peek().z == transform.position.z && mouseDirection != "Right")
            {
                mouseDirection = "Right";
                tradeDirection();
            }
            else if (pathPosition.Peek().z > transform.position.z && pathPosition.Peek().x == transform.position.x && mouseDirection != "Up")
            {
                mouseDirection = "Up";
                tradeDirection();
            }
            else if (pathPosition.Peek().z < transform.position.z && pathPosition.Peek().x == transform.position.x && mouseDirection != "Down")
            {                
                mouseDirection = "Down";
                tradeDirection();
            }
            transform.position = pathPosition.Peek();
            pathPosition.Dequeue();            
        }
        walking = false;
    }

    void tradeDirection()
    {
        Vector3 axis = new Vector3(0, 1, 0);

        if (transform.position == beginPos)
        {
            //Debug.Log("Igual");
        }
        else if (mouseDirection == "Down")
        {
            transform.rotation = beginRotation;
            transform.RotateAround(transform.position, axis, 0);
        }
        else if (mouseDirection == "Up")
        {
            transform.rotation = beginRotation;
            transform.RotateAround(transform.position, axis, 180);
        }
        else if (mouseDirection == "Left")
        {
            transform.rotation = beginRotation;
            transform.RotateAround(transform.position, axis, -90);
        }
        else if (mouseDirection == "Right")
        {
            transform.rotation = beginRotation;
            transform.RotateAround(transform.position, axis, 90);
        }
    }

}
