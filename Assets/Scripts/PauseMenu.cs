using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	//public string levelSelect;

	public string mainMenu;

    private Animator anim;

	public bool isPaused;

	public GameObject pauseMenuCanvus;

	private void Awake()
	{
        anim = GameObject.Find("Kirsty").GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {



		if (isPaused) {
			pauseMenuCanvus.SetActive (true);
			Time.timeScale = 0f;
            GameObject[] gameObjectsArray = GameObject.FindGameObjectsWithTag("projectiles");
            {
                foreach (GameObject go in gameObjectsArray) {
                    go.GetComponent<ProjectileController>().enabled = false;
                    go.GetComponent<DestroyObjectOverTime>().enabled = false;
                }
            }
		} else {
			pauseMenuCanvus.SetActive (false);
			Time.timeScale = 1f;
            GameObject[] gameObjectsArray = GameObject.FindGameObjectsWithTag("projectiles");
            {
                foreach (GameObject go in gameObjectsArray)
                {
                    go.GetComponent<ProjectileController>().enabled = true;
                    go.GetComponent<DestroyObjectOverTime>().enabled = true;
                }
            }
		}

        if (Input.GetButtonDown("Pause") && !isPaused)
        {
            isPaused = true;
            anim.enabled = false;
        }
        else if (Input.GetButtonDown("Pause") && isPaused)
        {
            isPaused = false;
            anim.enabled = true;
        }
	}

	public void Resume(){
        anim.enabled = true;
        isPaused = false;
	}

    public void Quit()
    {
        Application.Quit();
    }

	//public void LevelSelect(){
		//isPaused = false;
        //Application.LoadLevel (levelSelect);
	//}

	public void MainMenu (){
		isPaused = false;
		Application.LoadLevel (mainMenu);
	}
}
