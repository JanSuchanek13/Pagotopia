using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsManager : MonoBehaviour
{
    #region variables
    [Header("Stats Settings:")]
    [SerializeField] GameObject moneyDisplay;
    [SerializeField] GameObject tileCounterDisplay;
    [SerializeField] TextMeshProUGUI numericalMoneyDisplay;
    [SerializeField] TextMeshProUGUI numericalTileCounterDisplay;
    [SerializeField] Slider energyBar;
    [SerializeField] Slider happinessBar;
    [SerializeField] Slider environmentBar;
    [SerializeField] Image energyBarFilling;
    [SerializeField] Image happinessBarFilling;
    [SerializeField] Image environmentBarFilling;

    [Header("(handshakes - don't touch)")]
    public float energyValue;
    public float happinessValue;
    public float environmentValue;
    public float availableMoney;
    public float[] availableResources;
    public float upkeep = 0f; // how much production-tiles cost
    public float smallestAvailableValue;
    public int tileCounter = 0;
    private float _baseStatValue;
    private float _globalCostOfLiving;
    private float _startingMoney;
    public float _energyProductionRate;
    public float _happinessProductionRate;
    public float _environmentProductionRate;
    Color proImgBaseColor;
    Color hapImgBaseColor;
    Color envImgBaseColor;
    private bool giveIncomeFeedbackToPlayer = false;
    private bool giveTileCounterFeedbackToPlayer = false;

    //float accelleratedDegenerationRate;
    //float degenerationThreshold;
    //float speedUpDegenerationTime;
    //float degenerationIncreaseOverTime;
    //bool degenerationWasIncreased = false;

    // added to calculate score
    public int cowCounter = 0;
    public int efficientlyPlaced = 0;
    //public int efficiencePoints; // calculate when round ends: efficiencePoints = efficientlyPlaced * GetComponent<NewGameManager>().pointsForEfficiency;
    #endregion

    void Start()
    {
        _startingMoney = GetComponent<NewGameManager>().startingMoneyValue;
        availableMoney = _startingMoney;
        _globalCostOfLiving = GetComponent<NewGameManager>().baseCostOfLivingPerMinute / 50f / 60f; // this accounts for the base-tile of Pagotopia

        // basic stat values:
        _baseStatValue = GetComponent<NewGameManager>().baseStatValue; // this is used to stop overgrowth
        energyValue = _baseStatValue;
        happinessValue = _baseStatValue;
        environmentValue = _baseStatValue;

        // basic stat-bar colors (green):
        proImgBaseColor = energyBarFilling.color;
        hapImgBaseColor = happinessBarFilling.color;
        envImgBaseColor = environmentBarFilling.color;

        // pick up the timer-setting from GameManager to accellerate degeneration rates over time
        //speedUpDegenerationTime = SceneManager.GetComponent<GameManager>().speedUpDegenerationTime;
        //degenerationIncreaseOverTime = SceneManager.GetComponent<GameManager>().degenerationIncreaseOverTime;

        // accellerating degeneration over time
        /*if (SceneManager.GetComponent<GameManager>().useGraduallyIncresedDegeneration)
        {
            StartCoroutine("SpeedUpDegeneration");
        }*/
        //Debug.Log("the bar shows " + _baseStatValue);
    }

    // this updates the game stats at a rate of 50/s:
    void FixedUpdate()
    {
        numericalTileCounterDisplay.text = tileCounter.ToString() + " / 43"; // victory condition is checked by VictoryScript

        if (GetComponent<VictoryScript>().gameHasEnded == false)
        {
            energyValue += _energyProductionRate - _globalCostOfLiving;
            happinessValue += _happinessProductionRate - _globalCostOfLiving;
            environmentValue += _environmentProductionRate - _globalCostOfLiving;
            availableMoney -= upkeep;
            // checks if enough resources are available to build village:
            float[] currentAvailableResources = { energyValue, happinessValue, environmentValue };
            smallestAvailableValue = Mathf.Min(currentAvailableResources); // returns the lowest available resource value
            // updates Displays for current available money and tiles placed:
            numericalMoneyDisplay.text = availableMoney.ToString("F0");
        }
        


        //Debug.Log("energy Bar " + energyValue + " happiness Bar " + happinessValue + " and available money is: " + availableMoney);
        //Debug.Log("efficiency " + efficientlyPlaced);


        // as long as stat values are above the degeneration-threshold the normal degeneration-rate applies
        /*if (prosperityValue > degenerationThreshold && happinessValue > degenerationThreshold && environmentValue > degenerationThreshold)
        {
            // updates prosperity values 50/s
            prosperityValue -= prosperityDegenerationRate;
            happinessValue -= happinessDegenerationRate;
            environmentValue -= environmentDegenerationRate;
        }

        // once below the degeneration-threshold the accellerated degeneration-rate applies to all other stats
        if (environmentValue < degenerationThreshold)
        {
            prosperityValue -= prosperityDegenerationRate * accelleratedDegenerationRate;
            happinessValue -= happinessDegenerationRate * accelleratedDegenerationRate;
        }
        if (happinessValue < degenerationThreshold)
        {
            prosperityValue -= prosperityDegenerationRate * accelleratedDegenerationRate;
            environmentValue -= environmentDegenerationRate * accelleratedDegenerationRate;
        }
        if (prosperityValue < degenerationThreshold)
        {
            happinessValue -= happinessDegenerationRate * accelleratedDegenerationRate;
            environmentValue -= environmentDegenerationRate * accelleratedDegenerationRate;
        }*/


        // displays updated stat values in UI:
        energyBar.value = energyValue;
        happinessBar.value = happinessValue;
        environmentBar.value = environmentValue;

        // ensures that the stat caps cannot be surpassed
        if (energyValue > _baseStatValue)
        {
            energyValue = _baseStatValue;
        }
        if (happinessValue > _baseStatValue)
        {
            happinessValue = _baseStatValue;
        }
        if (environmentValue > _baseStatValue)
        {
            environmentValue = _baseStatValue;
        }

        // call "VictoryScript"-script to end game if any stat value drops to 0 (or below)
        if (energyValue <= 0f || happinessValue <= 0f || environmentValue <= 0f || availableMoney <= 0f)
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

        // PROSPERITY:
        if (energyValue > (_baseStatValue / 2))
        {
            energyBarFilling.color = proImgBaseColor; // green
        }else if (energyValue <= (_baseStatValue / 2) && energyValue > (_baseStatValue / 4))
        {
            energyBarFilling.color = new Color32(255, 116, 0, 150); // orange
        }else if (energyValue <= (_baseStatValue / 4))
        {
            energyBarFilling.color = new Color32(188, 0, 0, 200); // red
        }

        // HAPPINESS:
        if (happinessValue > (_baseStatValue / 2))
        {
            happinessBarFilling.color = hapImgBaseColor;
        }else if (happinessValue <= (_baseStatValue / 2) && happinessValue > (_baseStatValue / 4))
        {
            happinessBarFilling.color = new Color32(255, 116, 0, 150);
        }else if (happinessValue <= (_baseStatValue / 4))
        {
            happinessBarFilling.color = new Color32(188, 0, 0, 200);
        }

        // ENVIRONMENT:
        if (environmentValue > (_baseStatValue / 2))
        {
            environmentBarFilling.color = envImgBaseColor;
        }else if (environmentValue <= (_baseStatValue / 2) && environmentValue > (_baseStatValue / 4))
        {
            environmentBarFilling.color = new Color32(255, 116, 0, 150);
        }else if (environmentValue <= (_baseStatValue / 4))
        {
            environmentBarFilling.color = new Color32(188, 0, 0, 200);
        }

        /*if (tileCounter == 5 | tileCounter == 10 | tileCounter == 15 | tileCounter == 20 | tileCounter == 25 | tileCounter == 30 | tileCounter == 35 | tileCounter == 40 | tileCounter == 45)
        {
            if (degenerationWasIncreased == false)
            {
                IncreaseDegenerationRates();
                Debug.Log("tileCounter worked " + tileCounter);
            }
        }*/

        // futile attempt to use pingpong on income display
        if(giveIncomeFeedbackToPlayer == true)
        {
            float scale = 1 + Mathf.PingPong(Time.time * 0.2f, 1.5f - 1);
            moneyDisplay.transform.localScale = new Vector3(scale, scale, scale);
        }
        if (giveTileCounterFeedbackToPlayer == true)
        {
            float scale = 1 + Mathf.PingPong(Time.time * 0.2f, 1.5f - 1);
            tileCounterDisplay.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
    // this is supposed to update the degen. rates every 60 seconds - unless they already increased by placing tiles
    /*IEnumerator SpeedUpDegeneration()
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
        if (GetComponent<VictoryScript>().gameHasEnded == false) // don't change values after game has ended
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
    }*/

    public void TileCounterFeedbackToPlayer()
    {
        giveTileCounterFeedbackToPlayer = true;
        Invoke("ResetIncomeFeedbackToPlayer", 1f);
    }
    private void ResetTileCounterFeedbackToPlayer()
    {
        giveTileCounterFeedbackToPlayer = false;
    }
    public void IncomeFeedbackToPlayer() 
    {
        giveIncomeFeedbackToPlayer = true;
        Invoke("ResetIncomeFeedbackToPlayer", 1f);
    }
    private void ResetIncomeFeedbackToPlayer()
    {
        giveIncomeFeedbackToPlayer = false;
    }
    public void UpdateCostOfLiving(float additionalCosts)
    {
        _globalCostOfLiving += additionalCosts;
    }
    public void UpdateEnergyProduction(float additionalProduction)
    {
        _energyProductionRate += additionalProduction;
    }
    public void UpdateHappinessProduction(float additionalProduction)
    {
        _happinessProductionRate += additionalProduction;
    }
    public void UpdateEnvironmentProduction(float additionalProduction)
    {
        _environmentProductionRate += additionalProduction;
    }
}
