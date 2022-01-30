using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCasterBehavior : MovingShapeBehavior
{

    public bool exampleShowing = false;
    public bool comparing = false;
    public float timeShowingAtStart = 5f;
    public float currentTimeShowing = 0f;
    public MovingShapeBehavior[] controlledObjects;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(exampleShowing)
        {
            //increase current time active
            currentTimeShowing += Time.deltaTime;
            if(currentTimeShowing>=timeShowingAtStart)
            {
                FinishActiveTime();
            }
        }
    }

    public override void Activate()
    {
        comparing = true;
        gameObject.SetActive(true);
        foreach(MovingShapeBehavior co in controlledObjects)
        {
            co.gameObject.SetActive(true);
        }
        gameObject.SetActive(true);

        //currentTimeShowing = 0;
        //exampleShowing = true;
    }

    protected void FinishActiveTime()
    {
        exampleShowing = false;
        currentTimeShowing = 0;
        controlledObjects[0].Activate();
        gameObject.SetActive(false);
    }

    //protected void Compare()
    //{
    //    comparing = true;
    //    gameObject.SetActive(true);
    //}
}
