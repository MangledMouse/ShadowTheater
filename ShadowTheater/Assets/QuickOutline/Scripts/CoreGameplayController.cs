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

    public string PercentageCorrectTextString = "Percentage Correct ";
    // Start is called before the first frame update

    public float DifferenceInRotations;
    public float ActiveShapeRotation;
    public float LitShapeRotation;
    public int TargetRotation;

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
        if(Input.GetKeyDown(KeyCode.Return))
        {
            if (StateOfPlay == PlayState.Result)
                ResetRound();
        }
    }

    private void ResetRound()
    {
        SetUpIntro(false);
    }

    private void StartNewRound()
    {
        SetUpIntro();
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

    private void SetUpIntro(bool withNewShape = true)
    {
        RoundFinishedText.gameObject.SetActive(false);
        PercentageCorrectText.gameObject.SetActive(false);
        //Show Explannation Text to start
        IntroText.gameObject.SetActive(true);
        StateOfPlay = PlayState.Intro;
        //In the future I will want there to be a random selection from the mirrorshapes array. Next there will be a random rotation, then the random MirroredShape's Lit Shape will be set to that rotation
        //and then the lit shape will have Active set to true and the text will appear on screen

        if (withNewShape)
        {
            ActiveShape = ChooseActiveShape();
        }
        ActivateShape(ActiveShape, withNewShape);
    }

    private int CorrectPercentage()
    {
        float rotation = ActiveShape.transform.rotation.eulerAngles.x;
        ActiveShapeRotation = rotation;
        LitShapeRotation = ActiveShape.targetShape.transform.rotation.eulerAngles.x;
        if (ActiveShapeRotation > LitShapeRotation)
            DifferenceInRotations = ActiveShapeRotation - LitShapeRotation;
        else
            DifferenceInRotations = LitShapeRotation - ActiveShapeRotation;

        //for percentage we want divided by 360 then multiplied by 100 so divided by 3.6
        float percentageDifference = DifferenceInRotations / 3.6f;
        if (percentageDifference < 0)
            percentageDifference *= -1;

        percentageDifference = (float)Math.Round((decimal)percentageDifference, 0);

        return 100 - (int)percentageDifference;
    }

    public void ActivateShape(MirroredShape shape, bool withNewShape = true)
    {
        shape.gameObject.SetActive(false);
        if (withNewShape)
        {
            Quaternion targetRotation = Quaternion.Euler(GetTargetRotation(), 0, 0);
            shape.targetShape.gameObject.transform.SetPositionAndRotation(TargetLocationForLitShape, targetRotation);
        }
        shape.targetShape.gameObject.SetActive(true);
    }

    public int GetTargetRotation()
    {
        System.Random rnd = new System.Random();
        TargetRotation = rnd.Next(0, 360);
        return TargetRotation;
    }

    public MirroredShape ChooseActiveShape()
    {
        foreach(MirroredShape ms in MirroredShapes)
        {
            ms.gameObject.SetActive(false);
            ms.targetShape.gameObject.SetActive(false);
        }
        System.Random rnd = new System.Random();
        int randomIndex= rnd.Next(0, MirroredShapes.Length);
        return MirroredShapes[randomIndex];
    }
}
