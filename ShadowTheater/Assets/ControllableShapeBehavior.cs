using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableShapeBehavior : MovingShapeBehavior
{
    public bool controllable = false;
    public bool flyingIn = false;
    public Vector3 targetPosition;
    public MovingShapeBehavior nextMovingShape;
    public float speedOfFlyIn = 1f;
    public float speedOfRotate = 50f;

    // Start is called before the first frame update
    void Start()
    {
    }

    //Should send it to the target location when it wakes up
    void Awake()
    {
        //flyingIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Move to target position
        if(flyingIn)
        {
            if(gameObject.transform.position == targetPosition)
            {
                flyingIn = false;
                controllable = true;
            }
            else
            {
                float step = speedOfFlyIn * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            }
        }
        //Rotate and allow for player input to stop the rotation
        if(controllable)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopPressed();
            }
            else
            {
                float step = speedOfRotate * Time.deltaTime;
                //transform.rotation.Set(transform.rotation.x, transform.rotation.y, transform.rotation.z + step, tran);
                transform.Rotate(0, 0, step);
            }
        }
    }

    protected void StopPressed()
    {
        controllable = false;
        if(nextMovingShape != null)
        {
            gameObject.SetActive(false);
            nextMovingShape.Activate();
        }
    }

    public override void Activate()
    {
        flyingIn = true;
    }
}
