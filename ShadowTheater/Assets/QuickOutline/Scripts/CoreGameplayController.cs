using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum PlayState
{
    Intro, Spinning, Result
}
public class CoreGameplayController : MonoBehaviour
{
    public MirroredShape[] MirroredShapes;
    public Vector3 TargetLocationForMirrorShape;
    public Vector3 TargetLocationForLitShape;
    public Canvas TextCanvas;
    public MirroredShape ActiveShape;

    //Gets set to Intro on start
    public PlayState StateOfPlay;
    
    public UnityEngine.UI.Text IntroText;
    public UnityEngine.UI.Text RoundFinishedText;
    public UnityEngine.UI.Text PercentageCorrectText;

    public string PercentageCorrectTextString = "Percentage Incorrect ";
    // Start is called before the first frame update

    public float DifferenceInRotations;

    public Light AreaLight;

    void Start()
    {
        SetUpIntro();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch(StateOfPlay)
            {
                case PlayState.Intro:
                    SetupSpin();
                    break;
                case PlayState.Spinning:
                    StopSpinningAndCompare();
                    break;
                case PlayState.Result:
                    StartNewRound();
                    break;
            }
            
        }
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (StateOfPlay == PlayState.Result)
                ResetRound();
        }
    }

    private void ResetRound()
    {
        //throw new NotImplementedException();
    }

    private void StartNewRound()
    {
        //throw new NotImplementedException();
    }

    private void StopSpinningAndCompare()
    {
        RoundFinishedText.gameObject.SetActive(true);
        PercentageCorrectText.gameObject.SetActive(true);

        PercentageCorrectText.text = PercentageCorrectTextString + CorrectPercentage() +"%";

        ActiveShape.Spinning = false;
        ActiveShape.targetShape.gameObject.SetActive(true);

        StateOfPlay = PlayState.Result;

    }

    private void SetupSpin()
    {
        IntroText.gameObject.SetActive(false);
        StateOfPlay = PlayState.Spinning;

        ActiveShape.gameObject.SetActive(true);
        Quaternion baseRotation = Quaternion.Euler(0, 0, 0);

        ActiveShape.transform.rotation = baseRotation;
        ActiveShape.Spinning = true;
        ActiveShape.targetShape.gameObject.SetActive(false);
    }

    private void SetUpIntro()
    {
        //Show Explannation Text to start
        IntroText.gameObject.SetActive(true);
        StateOfPlay = PlayState.Intro;
        //In the future I will want there to be a random selection from the mirrorshapes array. Next there will be a random rotation, then the random MirroredShape's Lit Shape will be set to that rotation
        //and then the lit shape will have Active set to true and the text will appear on screen

        ActivateShape(ChooseActiveShape());
    }

    private int CorrectPercentage()
    {
        float rotation = ActiveShape.transform.rotation.eulerAngles.x;
        while(rotation > 360)
        {
            rotation -= 360;
        }

        DifferenceInRotations = rotation - ActiveShape.targetShape.transform.rotation.eulerAngles.x;

        //for percentage we want divided by 360 then multiplied by 100 so divided by 3.6
        float percentageDifference = DifferenceInRotations / 3.6f;
        if (percentageDifference < 0)
            percentageDifference *= -1;
        if (percentageDifference > 50)
            percentageDifference -= 50;

        return (int)percentageDifference;
    }

    public void ActivateShape(MirroredShape shape)
    {
        shape.gameObject.SetActive(false);

        Quaternion targetRotation = Quaternion.Euler(GetTargetRotation(), 0, 0);
        shape.targetShape.gameObject.transform.SetPositionAndRotation(TargetLocationForLitShape, targetRotation);
        shape.targetShape.gameObject.SetActive(true);
        
    }

    public int GetTargetRotation()
    {
        System.Random rnd = new System.Random();
        return rnd.Next(0, 360);
    }

    public MirroredShape ChooseActiveShape()
    {
        return ActiveShape;
    }
}
