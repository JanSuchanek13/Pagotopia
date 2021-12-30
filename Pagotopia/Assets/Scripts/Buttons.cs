using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] AudioSource standardClick;
    [SerializeField] AudioSource exitClick;
    [SerializeField] AudioSource doorClose;
    [SerializeField] AudioSource ambientSound;
    public AudioSource kickingSound;
    public AudioSource mooh;
    public AudioSource soundOfPaper;

    [SerializeField] GameObject pauseScreen_UI;
    [SerializeField] GameObject areYouSure_UI;

    [SerializeField] GameObject pop_up_screen_1;
    [SerializeField] GameObject pop_up_screen_2;

    [SerializeField] GameObject jan;
    [SerializeField] GameObject sophie;
    [SerializeField] GameObject paula;
    [SerializeField] GameObject felix;

    GameObject lastClickedNameButton;

    [SerializeField] GameObject credits_PopUp;
    public TextMeshProUGUI developerName_TextField;
    public TextMeshProUGUI developerSkills_TextField;
    bool aCreditsButtonHasBeenPressed = false;

    bool gameIsPaused = false;
    // used to check if the player can pause the scene he/she is in
    bool canBePaused = false;

    [SerializeField] GameObject getNewSetOfTiles_Button;

    // respawn-blink-timer; used by KickOnClick-script
    public float blinkIntervall = .3f;

    // all of this is for the Tutorial:
    int tutorialPage;
    [SerializeField] int numberOfPages = 9;
    [SerializeField] GameObject tutorial_page_0;
    [SerializeField] GameObject tutorial_page_1;
    [SerializeField] GameObject tutorial_page_2;
    [SerializeField] GameObject tutorial_page_3;
    [SerializeField] GameObject tutorial_page_4;
    [SerializeField] GameObject tutorial_page_5;
    [SerializeField] GameObject tutorial_page_6;
    [SerializeField] GameObject tutorial_page_7;
    [SerializeField] GameObject tutorial_page_8;
    [SerializeField] GameObject forwardButton;
    [SerializeField] GameObject backwardButton;
    public AudioSource errorSound; // also used by the tileGenerator script when pressing the NewSet button in Error
    [SerializeField] TextMeshProUGUI tutorial_page_counter;

    // used to reset Stat-Bars when requesting new set of tiles
    GameObject StatsDisplay;

    // this Start function is only to turn off your pop_ups in case you forgot
    private void Start()
    {
        StatsDisplay = GameObject.Find("Stats_UI");

        if (pop_up_screen_1 != null)
        {
            pop_up_screen_1.SetActive(false);
        }
        if (pop_up_screen_2 != null)
        {
            pop_up_screen_2.SetActive(false);
        }

        // checks if player is currently in the actual game in order to unlock "pause"-ability
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            canBePaused = true;
        }
        
        
        if (SceneManager.GetActiveScene().name == "Tutorial_1")
        {
            backwardButton.GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            tutorial_page_counter.text = "1/" + numberOfPages;
        }

    }
    // Pause and open a screen while playing, enabling leaving the game midgame
    void Update()
    {
        if(gameIsPaused == false && canBePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape)/* | Input.GetKeyDown(KeyCode.Space)*/)
            {
                //standardClick.Play();
                //ambientSound.Pause();
                PauseGame();
            }
        }
    }
    public void PauseGame()
    {
        standardClick.Play();
        ambientSound.Pause();
        // this doesnt need sound, as this gets called WITH sound in the Update
        gameIsPaused = true;
        Time.timeScale = 0;
        pauseScreen_UI.SetActive(true);
    }
    public void ResumeGame()
    {
        standardClick.Play();
        ambientSound.UnPause();
        gameIsPaused = false;
        if (pauseScreen_UI != null)
        {
            pauseScreen_UI.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void StartTutorial()
    {
        standardClick.Play();
        SceneManager.LoadScene("Tutorial_1");
    }

    public void StartGame()
    {
        standardClick.Play();
        SceneManager.LoadScene("Level_1");
        Debug.Log("Start Button has been pressed");
        ResumeGame();
    }

    public void Restart()
    {
        standardClick.Play();
        ResumeGame();
        // this restarts current Level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Debug.Log("this scene was just restarted");
    }

    // Opens Credit Screen
    public void Credits()
    {
        standardClick.Play();
        SceneManager.LoadScene("Credits_UI");
    }

    // Goes back to main menu
    public void BackToMain()
    {
        standardClick.Play();
        ResumeGame();
        SceneManager.LoadScene("Start_UI");
    }

    // Exits the game when asked to confirm
    public void YesIAmSure()
    {
        standardClick.Play();
        Debug.Log("The game would now end in built game!"); // this is to check operation while in Editor
        Application.Quit(); // only works when built
    }
    public void NoIAmNot()
    {
        standardClick.Play();
        areYouSure_UI.SetActive(false);
    }

    // launches UI asking to confirm if you REALLY wanna leave the game
    public void QuitGame()
    {
        //optional sounds for extra polish
        //exitClick.Play(); // should play door closing & clicking sound
        //doorClose.Play();
        standardClick.Play();
        areYouSure_UI.SetActive(true);
    }

    public void About_Sustainabilty_UI()
    {
        standardClick.Play();
        SceneManager.LoadScene("Sustainability_UI");
        Debug.Log("About_Sustainability-Button was pressed");
    }
    public void Highscore_UI()
    {
        standardClick.Play();
        SceneManager.LoadScene("Highscore_UI");
        Debug.Log("Highscore-Button was pressed");
    }

    // this gets the player a new set of tiles (referencing the TileGenerator-script)
    bool nextSetButtonCooldown = false;
    public void GetNewSetOfTiles()
    {
        Debug.Log("GetNewSetOfTiles_button was pressed");
        GameObject SceneManager = GameObject.Find("SceneManager");

        if (nextSetButtonCooldown == false)
        {
            //Debug.Log("nextSetButtonCooldown is false");

            nextSetButtonCooldown = true;
            standardClick.Play();
            SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();
            SceneManager.GetComponent<NewGameManager>().hoverInfoEnabled = true;
            //SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab = null;
            StatsDisplay.GetComponent<StatUIDisplay>().ResetStatBars();
            //SceneManager.GetComponent<TileGenerator>().NextSet();
            //getNewSetOfTiles_Button.GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            Invoke("ResetNextSetButtonCooldown", 10f);
        }else
        {
            errorSound.Play();
        }
    }
    void ResetNextSetButtonCooldown()
    {
        nextSetButtonCooldown = false;
        getNewSetOfTiles_Button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        return;
    }

    // all of this is for the Tutorial:
    public void Forward()
    {
        if(tutorialPage !< (numberOfPages - 1))
        {
            tutorialPage += 1;
            standardClick.Play();
            tutorial_page_counter.text = (tutorialPage + 1).ToString() + "/" + numberOfPages; // manually change to accurate page count
        }else
        {
            if (errorSound)
            {
                errorSound.Play();
            }
        }
        
        if (tutorialPage == 1)
        {
            backwardButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            tutorial_page_0.SetActive(false);
            tutorial_page_1.SetActive(true);
        }else if (tutorialPage == 2)
        {
            tutorial_page_1.SetActive(false);
            tutorial_page_2.SetActive(true);
        }else if (tutorialPage == 3)
        {
            tutorial_page_2.SetActive(false);
            tutorial_page_3.SetActive(true);
        }else if (tutorialPage == 4)
        {
            tutorial_page_3.SetActive(false);
            tutorial_page_4.SetActive(true);
        }else if (tutorialPage == 5)
        {
            tutorial_page_4.SetActive(false);
            tutorial_page_5.SetActive(true);
        }else if (tutorialPage == 6)
        {
            tutorial_page_5.SetActive(false);
            tutorial_page_6.SetActive(true);
        }else if (tutorialPage == 7)
        {
            tutorial_page_6.SetActive(false);
            tutorial_page_7.SetActive(true);
        }else if (tutorialPage == 8)
        {
            tutorial_page_7.SetActive(false);
            tutorial_page_8.SetActive(true);
            forwardButton.GetComponent<Image>().color = new Color32(100, 100, 100, 100);
        }
        if(tutorialPage == numberOfPages + 1)
        {
            Debug.Log(tutorialPage + " vs " + numberOfPages);
            forwardButton.GetComponent<Image>().color = new Color32(100, 100, 100, 100);
        }else
        {
            forwardButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }
    public void Backward()
    {
        if (tutorialPage > 0)
        {
            tutorialPage -= 1;
            standardClick.Play();
            tutorial_page_counter.text = (tutorialPage + 1).ToString() + "/" + numberOfPages; // manually change to accurate page count
        }else
        {
            if (errorSound)
            {
                errorSound.Play();
            }
        }
        if (tutorialPage == 0)
        {
            backwardButton.GetComponent<Image>().color = new Color32(100, 100, 100, 100);
            tutorial_page_0.SetActive(true);
            tutorial_page_1.SetActive(false);
        }
        else if (tutorialPage == 1)
        {
            tutorial_page_1.SetActive(true);
            tutorial_page_2.SetActive(false);
        }else if (tutorialPage == 2)
        {
            tutorial_page_2.SetActive(true);
            tutorial_page_3.SetActive(false);
        }else if (tutorialPage == 3)
        {
            tutorial_page_3.SetActive(true);
            tutorial_page_4.SetActive(false);
        }else if (tutorialPage == 4)
        {
            tutorial_page_4.SetActive(true);
            tutorial_page_5.SetActive(false);
        }else if (tutorialPage == 5)
        {
            tutorial_page_5.SetActive(true);
            tutorial_page_6.SetActive(false);
        }else if (tutorialPage == 6)
        {
            tutorial_page_6.SetActive(true);
            tutorial_page_7.SetActive(false);
        }
        else if (tutorialPage == 7)
        {
            tutorial_page_7.SetActive(true);
            tutorial_page_8.SetActive(false);
            forwardButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    // buttons at the end of the game:
    // when ending a game by winning/loosing (not exiting) you will have to continue by clicking this
    [SerializeField] GameObject highscore_UI;
    [SerializeField] GameObject highscore_UI_Buttons;

    public void OpenHighscore_UI()
    {
        // could close the "WIN" and "LOSE" Logos here, but may be cool to keep them as overlay for the rest of the round
        standardClick.Play();
        GameObject SceneManager = GameObject.Find("SceneManager");
        SceneManager.GetComponent<VictoryScript>().endGame_UI.SetActive(false);
        highscore_UI.SetActive(true);
        highscore_UI_Buttons.SetActive(true);

        // --> display all current highscores (maybe 10?) 
        // -------> here an option for "moreInfo" would be awesome, to see relevant stats of your round.
        // --> "TakeScreenshot" button to allow a pic (now after being able to see how well you did)
        // --> "BackToMain" button (back to main menu)
        // --> "PlayAgain" button (starts another round)
    }





    // these are the buttons in the credits_Scene
    public void JanKuh()
    {
        jan.GetComponent<KickOnClick>().wobbleEnabled = true;
        mooh.Play();
        lastClickedNameButton = jan;
        Invoke("StopWobble", 3f);
        developerName_TextField.text = "Jan";
        developerSkills_TextField.text = "Game Design and Code";
    }
    public void SophieKuh()
    {
        sophie.GetComponent<KickOnClick>().wobbleEnabled = true;
        mooh.Play();
        lastClickedNameButton = sophie;
        Invoke("StopWobble", 3f);
        developerName_TextField.text = "Sophie";
        developerSkills_TextField.text = "Art, 3D and Music";
    }
    public void PaulaKuh()
    {
        paula.GetComponent<KickOnClick>().wobbleEnabled = true;
        mooh.Play();
        lastClickedNameButton = paula;
        Invoke("StopWobble", 3f);
        developerName_TextField.text = "Paula";
        developerSkills_TextField.text = "Art, 3D and VFX";
    }
    public void FelixKuh()
    {
        felix.GetComponent<KickOnClick>().wobbleEnabled = true;
        mooh.Play();
        lastClickedNameButton = felix;
        Invoke("StopWobble", 3f);
        developerName_TextField.text = "Felix";
        developerSkills_TextField.text = "Game Design, UI's and Code";
    }
    // called by the buttons in Credits_Scene to stop a cow from wobbling after 3 seconds
    public void StopWobble()
    {
        lastClickedNameButton.GetComponent<KickOnClick>().wobbleEnabled = false;
    }

    // ----------------------------------------------

    // these are completly optional UI_Screens you can assign via [SerializeFields]
    public void Open_PoP_Up_Screen_1()
    {
        if (pop_up_screen_1 != null)
        {
            standardClick.Play();
            Time.timeScale = 0;
            pop_up_screen_1.SetActive(true);
        }else
        {
            Debug.Log("You want to open pop_up_screen_1");
            Debug.Log("Make sure that you put the desired pop-up in the corresponding slot of the \"Buttons\"-Script in the \"SceneManager\"-GameObject in the Editor");
            Debug.Log("Be aware that there is pop_up_screen_1 & pop_up_screen_2! --> this is targeting Nr.: 1");
        }
    }
    public void Close_PoP_Up_Screen_1()
    {
        if (pop_up_screen_1 != null)
        {
            standardClick.Play();
            Time.timeScale = 1;
            pop_up_screen_1.SetActive(false);
        }
    }
    public void Open_PoP_Up_Screen_2()
    {
        if (pop_up_screen_2 != null)
        {
            standardClick.Play();
            Time.timeScale = 0;
            pop_up_screen_2.SetActive(true);
        }else
        {
            Debug.Log("You want to open pop_up_screen_2");
            Debug.Log("Make sure that you put the desired pop-up in the corresponding slot of the \"Buttons\"-Script in the \"SceneManager\"-GameObject in the Editor");
            Debug.Log("Be aware that there is pop_up_screen_1 & pop_up_screen_2! --> this is targeting Nr.: 2");
        }
    }
    public void Close_PoP_Up_Screen_2()
    {
        if (pop_up_screen_2 != null)
        {
            standardClick.Play();
            Time.timeScale = 1;
            pop_up_screen_2.SetActive(false);
        }
    }
}