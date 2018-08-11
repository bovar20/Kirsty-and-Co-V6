using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MainMenu : MonoBehaviour
{
    
    public string startLevel;
    public AudioSource audioSourceIntro;
    public AudioSource audioSourceLoop;
    bool startedLoop = false;


    void FixedUpdate()
    {
        if (!audioSourceIntro.isPlaying && !startedLoop)
        {
            audioSourceLoop.Play();
            Debug.Log("Done playing");
            startedLoop = true;
        }
    }





    public void NewGame()
    {

        SceneManager.LoadScene(startLevel);

    }

    public void QuitGame()
    {
        Debug.Log("Game Exited");
        Application.Quit();
    }
}
