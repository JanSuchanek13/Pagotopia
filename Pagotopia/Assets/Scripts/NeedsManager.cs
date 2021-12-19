using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NeedsManager : MonoBehaviour
{
    public int tileCounter = 0;
    float speedUpDegenerationTime;
    float degenerationIncreaseOverTime;
    bool degenerationWasIncreased = false;

    public float prosperityValue;
    public float happinessValue;
    public float environmentValue;

    public float prosperityDegenerationRate;
    public float happinessDegenerationRate;
    public float environmentDegenerationRate;

    float accelleratedDegenerationRate;
    float degenerationThreshold;
    float baseStatValue;

    [SerializeField] Slider prosperityBar;
    [SerializeField] Slider happinessBar;
    [SerializeField] Slider environmentBar;
    [SerializeField] Image proImg;
    [SerializeField] Image hapImg;
    [SerializeField] Image envImg;

    Color proImgBaseColor;
    Color hapImgBaseColor;
    Color envImgBaseColor;

    // added to calculate score
    public int cowCounter = 0;
    public float efficientlyPlaced = 0f;

    void Start()
    {
        GameObject SceneManager = GameObject.Find("SceneManager");

        // pick up the timer-setting from GameManager to accellerate degeneration rates over time
        speedUpDegenerationTime = SceneManager.GetComponent<GameManager>().speedUpDegenerationTime;
        degenerationIncreaseOverTime = SceneManager.GetComponent<GameManager>().degenerationIncreaseOverTime;

        // determines the degeneration Threshold as % of the maximum stat-value
        degenerationThreshold = SceneManager.GetComponent<GameManager>().degenerationThreshold / 100f * SceneManager.GetComponent<GameManager>().baseStatValue;
        
        // gets the accellerated degeneration-rate value from "GameManager"-class
        accelleratedDegenerationRate = SceneManager.GetComponent<GameManager>().accelleratedDegenerationRate;
        
        // determines basic degeneration rates
        prosperityDegenerationRate = SceneManager.GetComponent<GameManager>().globalDegenerationRate;
        happinessDegenerationRate = SceneManager.GetComponent<GameManager>().globalDegenerationRate;
        environmentDegenerationRate = SceneManager.GetComponent<GameManager>().globalDegenerationRate;

        // determines basic stat values
        baseStatValue = SceneManager.GetComponent<GameManager>().baseStatValue;
        prosperityValue = baseStatValue;
        happinessValue = baseStatValue;
        environmentValue = baseStatValue;

        // determines basic stat-bar colors (green)
        proImgBaseColor = proImg.color;
        hapImgBaseColor = hapImg.color;
        envImgBaseColor = envImg.color;

        // accellerating degeneration over time
        if (SceneManager.GetComponent<GameManager>().useGraduallyIncresedDegeneration)
        {
            StartCoroutine("SpeedUpDegeneration");
        }
    }

    // this updates the game stats at a rate of 50/s
    void FixedUpdate()
    {
            // as long as stat values are above the degeneration-threshold the normal degeneration-rate applies
        if (prosperityValue > degenerationThreshold && happinessValue > degenerationThreshold && environmentValue > degenerationThreshold)
        {
            // updates prosperity values 50/s
            prosperityValue -= prosperityDegenerationRate;
            happinessValue -= happinessDegenerationRate;
            environmentValue -= environmentDegenerationRate;
        }

        // once below the degeneration-threshold the accellerated degeneration-rate applies to all other stats
        if(environmentValue < degenerationThreshold)
            {
                prosperityValue -= prosperityDegenerationRate * accelleratedDegenerationRate;
                happinessValue -= happinessDegenerationRate * accelleratedDegenerationRate;
            }
        if(happinessValue < degenerationThreshold)
            {
                prosperityValue -= prosperityDegenerationRate * accelleratedDegenerationRate;
                environmentValue -= environmentDegenerationRate * accelleratedDegenerationRate;
            }
        if (prosperityValue < degenerationThreshold)
            {
                happinessValue -= happinessDegenerationRate * accelleratedDegenerationRate;
                environmentValue -= environmentDegenerationRate * accelleratedDegenerationRate;
            }


        // displays updated stat values in UI
        // was previously in the if/else statement above. if this works - DELETE this comment! (!!!!!!)
        prosperityBar.value = prosperityValue;
        happinessBar.value = happinessValue;
        environmentBar.value = environmentValue;

        // ensures that the stat caps cannot be surpassed
        if (prosperityValue > baseStatValue)
        {
            prosperityValue = baseStatValue;
        }
        if (happinessValue > baseStatValue)
        {
            happinessValue = baseStatValue;
        }
        if (environmentValue > baseStatValue)
        {
            environmentValue = baseStatValue;
        }

        // call "VictoryScript"-script to end game if any stat value drops to 0 (or below)
        if (prosperityValue <= 0f || happinessValue <= 0f || environmentValue <= 0f)
        {
            if (GetComponent<VictoryScript>().gameHasEnded == false)
            {
                GetComponent<VictoryScript>().Loser();
            }
        }

            //color changes:
            // 50.01%-100% green
            // 25.01%-50% orange
            // 0%-25% red

            //PROSPERITY
            if (prosperityValue > (baseStatValue / 2))
            {
                proImg.color = proImgBaseColor; // green
            }else if (prosperityValue <= (baseStatValue / 2) && prosperityValue > (baseStatValue / 4))
            {
                proImg.color = new Color32(255, 116, 0, 150); // orange
            }else if (prosperityValue <= (baseStatValue / 4))
            {
                proImg.color = new Color32(188, 0, 0, 200); // red
            }

            //HAPPINESS
            if (happinessValue > (baseStatValue / 2))
            {
                hapImg.color = hapImgBaseColor;
            }else if (happinessValue <= (baseStatValue / 2) && happinessValue > (baseStatValue / 4))
            {
                hapImg.color = new Color32(255, 116, 0, 150);
            }else if (happinessValue <= (baseStatValue / 4))
            {
                hapImg.color = new Color32(188, 0, 0, 200);
            }

            //ENVIRONMENT
            if (environmentValue > (baseStatValue / 2))
            {
                envImg.color = envImgBaseColor;
            }else if (environmentValue <= (baseStatValue / 2) && environmentValue > (baseStatValue / 4))
            {
                envImg.color = new Color32(255, 116, 0, 150);
            }else if (environmentValue <= (baseStatValue / 4))
            {
                envImg.color = new Color32(188, 0, 0, 200);
            }

        if (tileCounter == 5 | tileCounter == 10 | tileCounter == 15 | tileCounter == 20 | tileCounter == 25 | tileCounter == 30 | tileCounter == 35 | tileCounter == 40 | tileCounter == 45)
        {
            if (degenerationWasIncreased == false)
            {
                IncreaseDegenerationRates();
                Debug.Log("tileCounter worked " + tileCounter);
            }
        }
    }
    // this is supposed to update the degen. rates every 60 seconds - unless they already increased by placing tiles
    IEnumerator SpeedUpDegeneration()
    {
        // this should only work until all tiles are full (at 48+1pagotopia-tile)... not sure how to test
        for (tileCounter = 0; tileCounter <= 48;) 
        {
            yield return new WaitForSeconds(speedUpDegenerationTime); // currently 60 seconds

            if (degenerationWasIncreased == false)
            {
                IncreaseDegenerationRates();
                Debug.Log("COROUTINE Worked!");
            }
        }
    }

    void IncreaseDegenerationRates()
    {
        if(GetComponent<VictoryScript>().gameHasEnded == false) // don't change values after game has ended
        {
            prosperityDegenerationRate += degenerationIncreaseOverTime; // should add .00333 to all degenerationrates (doubling base degeneration every 60 secs)
            happinessDegenerationRate += degenerationIncreaseOverTime;
            environmentDegenerationRate += degenerationIncreaseOverTime;
            degenerationWasIncreased = true;
            Invoke("ResetDegenerationIncreaser", speedUpDegenerationTime);
            Debug.Log("degeneration rates were increased");
        }
    }
    void ResetDegenerationIncreaser()
    {
        degenerationWasIncreased = false;
        Debug.Log("increaser cooldown was reset");
    }
}
    

