using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenScript : MonoBehaviour
{
    float baseTimeToSwitchScene;
    float currentTimeOnScreen;
    public AudioSource IntroMusic;
    // Start is called before the first frame update
    void Start()
    {
        currentTimeOnScreen = 0f;
        baseTimeToSwitchScene = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        currentTimeOnScreen += Time.deltaTime;
        if (currentTimeOnScreen >= baseTimeToSwitchScene)
            SwitchScene();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            SwitchScene();
        }
    }

    void SwitchScene()
    {
        SceneManager.LoadScene(1);
    }
}
