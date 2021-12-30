using UnityEngine;
using TMPro;

public class VictoryScript : MonoBehaviour
{
    // winning and loosing the game are handled here
    // in general you win by having placed all 48 tiles
    // loosing early by dropping a stat to zero is called by "NeedsManager"-script

    GameObject SceneManager;
    StatsManager StatsManager;
    NewGameManager GameManager;

    // input Podest Models to deactivate them
    [SerializeField] GameObject podest1;
    [SerializeField] GameObject podest2;
    [SerializeField] GameObject podest3;
    [SerializeField] GameObject newSetButton;
    [SerializeField] GameObject stats_UI;
    [SerializeField] GameObject game_UI;

    // this carries the input field for names (highscore)
    [SerializeField] GameObject enterName_UI;

    public GameObject youWonLogo; // public in case i want to turn off with a button
    [SerializeField] AudioSource youWonSound;

    public GameObject youLostLogo; // public in case i want to turn off with a button
    [SerializeField] AudioSource youLostSound;


    //[SerializeField] AudioSource windSound; // this may be cool for after loosing: just a dry wind blowing
    [SerializeField] AudioSource ambienteSound; // to turn off music
    public GameObject endGame_UI; // public so the "OpenHighscore_UI()" (from BUTTONS) can call it to close it

    public bool gameHasEnded = false; // public so the "NeedsManager" can read this information 

    void Awake()
    {
        SceneManager = GameObject.Find("SceneManager");
        GameManager = SceneManager.GetComponent<NewGameManager>();
        StatsManager = SceneManager.GetComponent<StatsManager>();
    }


    void Update()
    {
        // this whole IF statement is only for Quick Win while testing the game: --> winning by placing 1 tile
        // increased to 3 tiles to check for neighborEfficiencyBonus
        if (GameManager.quickWin == true && StatsManager.tileCounter == 3 && gameHasEnded == false)
        {
            Winner();
            gameHasEnded = true;

            // pulled up early
            stats_UI.SetActive(false);
            game_UI.SetActive(false);
            SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();
            podest1.SetActive(false);
            podest2.SetActive(false);
            podest3.SetActive(false);
            SceneManager.GetComponent<CalculateHighscore>().Calculate();
            //SceneManager.GetComponent<CalculateHighscore>().DetailWindow();
            Highscore_carrying_GO.GetComponent<Highscore>().AllowHighscore();
            if (Highscore_carrying_GO.GetComponent<Highscore>().allowedHS)
            {
                enterName_UI.SetActive(true);
            }
            else
            {
                Invoke("EndGame_UI", 3f);
            }
        }

        // checks for real winning condition
        if (StatsManager.tileCounter >= 43 && gameHasEnded == false)
        {
            Winner();
            gameHasEnded = true;

            // pulled up early
            stats_UI.SetActive(false);
            game_UI.SetActive(false);
            SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();
            podest1.SetActive(false);
            podest2.SetActive(false);
            podest3.SetActive(false);
            SceneManager.GetComponent<CalculateHighscore>().Calculate();
            //SceneManager.GetComponent<CalculateHighscore>().DetailWindow();
            Highscore_carrying_GO.GetComponent<Highscore>().AllowHighscore();
            if (Highscore_carrying_GO.GetComponent<Highscore>().allowedHS)
            {
                enterName_UI.SetActive(true);
            }else
            {
                Invoke("EndGame_UI", 3f);
            }
        }
    }


    public void Winner()
    {
        youWonLogo.SetActive(true);
        ambienteSound.Stop(); //--> not turning off ambient music, because i guess its cool!?
        youWonSound.Play();
        // invoke: open proceed UI: (with dramatic delay
        // trying to have this happen when game ends IF NO highscore was achieved
        //Invoke("EndGame_UI", 3f); // probably length of the victory sound. Adjust manually
    }
    public void Loser()
    {
        gameHasEnded = true;// needs to be here as this can be called outside of having 48 placed tiles!
        Debug.Log("THIS STILL WORKS 1");
        youLostLogo.SetActive(true);
        Debug.Log("THIS STILL WORKS 2");
        ambienteSound.Stop();
        Debug.Log("THIS STILL WORKS 3");
        youLostSound.Play();
        Debug.Log("THIS STILL WORKS 4");
        //Invoke("EndGame_UI", 3f); // probably length of the victory sound. Adjust manually

        stats_UI.SetActive(false);
        Debug.Log("THIS STILL WORKS 5");
        game_UI.SetActive(false);
        Debug.Log("THIS STILL WORKS 6");
        //SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();
        Debug.Log("THIS STILL WORKS 7");
        podest1.SetActive(false);
        Debug.Log("THIS STILL WORKS 8");
        podest2.SetActive(false);
        Debug.Log("THIS STILL WORKS 9");
        podest3.SetActive(false);
        Debug.Log("THIS STILL WORKS 10");
        SceneManager.GetComponent<CalculateHighscore>().Calculate();
        Debug.Log("THIS STILL WORKS 11");
        //SceneManager.GetComponent<CalculateHighscore>().DetailWindow();
        Highscore_carrying_GO.GetComponent<Highscore>().AllowHighscore();
        Debug.Log("THIS STILL WORKS 12");
        if (Highscore_carrying_GO.GetComponent<Highscore>().allowedHS)
        {
            enterName_UI.SetActive(true);
            Debug.Log("THIS STILL WORKS 13");
        }
        else
        {
            Invoke("EndGame_UI", 3f);
            Debug.Log("THIS STILL WORKS 14");
        }
    }

    // overlay called once Logo & Sound were shown
    // this GO sits in EndGameScreen > Highscore_UI > Highscore_Background 
    [SerializeField] GameObject Highscore_carrying_GO;
    void EndGame_UI()
    {
        endGame_UI.SetActive(true);
        SceneManager.GetComponent<CalculateHighscore>().Calculate();
        SceneManager.GetComponent<CalculateHighscore>().DetailWindow();
        FillDepleteStats();

        /*stats_UI.SetActive(false);
        game_UI.SetActive(false);
        SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();
        podest1.SetActive(false);
        podest2.SetActive(false);
        podest3.SetActive(false);
        endGame_UI.SetActive(true);


        // this checks if a highscore was achieved/there was still open entries. If so, it opens the name input-field
        SceneManager.GetComponent<CalculateHighscore>().Calculate();
        SceneManager.GetComponent<CalculateHighscore>().DetailWindow();
        Highscore_carrying_GO.GetComponent<Highscore>().AllowHighscore();
        if (Highscore_carrying_GO.GetComponent<Highscore>().allowedHS)
        {
            enterName_UI.SetActive(true);
        }
        FillDepleteStats();*/
    }

    void FillDepleteStats()
    {
        //StatsManager.prosperityDegenerationRate *= 10;
        //StatsManager.happinessDegenerationRate *= 10;
        //StatsManager.environmentDegenerationRate *= 10;

        // better would be filling them up in like 3 seconds
        // maxVal - currVal = openVal / 50 = X (how much Val per tic needed to fill openVal in 1 sec)
        // X / 3 = Y (how much Val per tic needed to fill openVal in 3 sec)
        // newDegRate = Y --> this should fill all bars, no matter how high they are, in 3 seconds each
    }

    // button function to continue after entering a name scoring a highscore
    bool canContinue = false;
    public void ContinueFromEnterNameUI()
    {
        if (canContinue)
        {
            enterName_UI.SetActive(false);
            EndGame_UI();
        }else
        {
            AudioSource errorSound = GameObject.Find("Error_Sound").GetComponent<AudioSource>();
            errorSound.Play();
        }
    }
    // this gets enabled by the OnValueChange variable in the input field, enabling the continue button
    public void EnableContinueButton()
    {
        canContinue = true;
    }
    // called when done editing input field (when you hit enter)
    public void DoneEnteringName()
    {
        AudioSource successSound = GameObject.Find("Winning_Sound").GetComponent<AudioSource>();
        successSound.Play();
    }
}





//--------------------------------
// safeties:

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    GameObject SceneManager;
    NeedsManager NeedsManager;
    GameManager GameManager;
    GameObject ScreenshotButton;
    float enviromentLimit;
    float happinessLimit;
    float prosperityLimit;

    [SerializeField] GameObject endGameScreen_UI;
    //Highscore = highscoreScript;

    bool gameEnded = false;

    void Start()
    {
        SceneManager = GameObject.Find("SceneManager");
        GameManager = SceneManager.GetComponent<GameManager>();
        NeedsManager = SceneManager.GetComponent<NeedsManager>();

        //highscoreScript = SceneManager.GetComponent<Highscore>();
        //endGameScreen_UI.SetActive(false);


        //enviromentLimit = NeedsManager.environmentValue * 0.8f;
        //happinessLimit = NeedsManager.happinessValue * 0.8f;
        //prosperityLimit = NeedsManager.prosperityValue * 0.8f;

        //Button für Screenshot suchen
        ScreenshotButton = GameObject.Find("Screenshot_Button"); // name has to be exactly the same as in the actual game
        ScreenshotButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.developerMode == true && GameManager.quickWin == true && NeedsManager.tileCounter == 1)
        {
            NeedsManager.prosperityDegenerationRate -= .005f;
            NeedsManager.happinessDegenerationRate -= .005f;
            NeedsManager.environmentDegenerationRate -= .005f;

                if (NeedsManager.environmentDegenerationRate < 0f && NeedsManager.happinessDegenerationRate < 0f && NeedsManager.prosperityDegenerationRate < 0f)
                {
                    Debug.Log("You Won");

                    //highscoreScript.CalculateHighscore();
                    endGameScreen_UI.SetActive(true);

                    // execute winner screen HERE

                    ScreenshotButton.SetActive(true);
                if ((NeedsManager.prosperityValue && NeedsManager.happinessValue && NeedsManager.environmentValue) >= GameManager.baseStatValue)// && NeedsManager.happinessValue && NeedsManager.environmentValue))
                {
                    Time.timeScale = 0;
                }
                }
            
        }

            if (NeedsManager.tileCounter == 48)
{
    if (NeedsManager.environmentDegenerationRate < 0f && NeedsManager.happinessDegenerationRate < 0f && NeedsManager.prosperityDegenerationRate < 0f)
    {
        Debug.Log("You Won");

        // this will fill up all bars really quick:
        NeedsManager.prosperityDegenerationRate *= 10;
        NeedsManager.happinessDegenerationRate *= 10;
        NeedsManager.environmentDegenerationRate *= 10;

        //highscoreScript.CalculateHighscore();
        endGameScreen_UI.SetActive(true);

        // execute winner screen HERE

        ScreenshotButton.SetActive(true);
    }
    else // = if any degeneration rate is still going downwards
    {
        Debug.Log("You Lost");

        // this will fill up all bars really quick:
        NeedsManager.prosperityDegenerationRate *= 10;
        NeedsManager.happinessDegenerationRate *= 10;
        NeedsManager.environmentDegenerationRate *= 10;

        //highscoreScript.CalculateHighscore();
        endGameScreen_UI.SetActive(true);

        // execute loser screen HERE

        ScreenshotButton.SetActive(true);
    }
}
    }
}*/

