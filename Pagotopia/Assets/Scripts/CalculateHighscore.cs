using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculateHighscore : MonoBehaviour
{
    #region variables
    [Header("")]
    // score for the round:
    public int Score;
    // components of the score
    private GameObject _sceneManager;
    float _energyPoints;
    float _environmentPoints;
    float _happinessPoints;
    float _moneyPoints;
    float _efficiencyPoints;
    float _playtimePoints;
    int _upgradePoints;
    int _cowPoints;// just for fun

    // text boxes:
    GameObject _yearsEntry;
    GameObject _timeEntry;
    GameObject _energyProducedEntry;
    GameObject _environmentProducedEntry;
    GameObject _happinessProducedEntry;
    GameObject _moneyProducedEntry;
    GameObject _efficiencyCountDisplay;
    GameObject _cowCountDisplay;
    GameObject _scoreEntry;
    GameObject _upgradeEntry;

    float startTime;
    float days;
    float years;
    //float neighborCount;
    //int cowCount;

    // public variables:
    //public float detailPros;
    //public float detailEnv;
    //public float detailHap;
    #endregion

    void Awake()
    {
        startTime = Time.timeSinceLevelLoad;
        //Debug.Log("Starttime: " + startTime);
    }

    public void Calculate()
    {
        // calculate playtime:
        float currentTime = Time.timeSinceLevelLoad;
        float spentTime = currentTime;// - startTime;
        days = spentTime * 6.0833f; // currently a round takes about 6-8 minutes
        // 9/s would be 1.4795 years per minute
        // 3.041667/s would be .5 years per minute
        years = days / 365;

        _sceneManager = GameObject.Find("SceneManager");
        _energyPoints = _sceneManager.GetComponent<StatsManager>().TotalEnergyProduced;
        _environmentPoints = _sceneManager.GetComponent<StatsManager>().TotalEnvironmentProduced;
        _happinessPoints = _sceneManager.GetComponent<StatsManager>().TotalHappinessProduced;
        _moneyPoints = _sceneManager.GetComponent<StatsManager>().TotalMoneyProduced;
        _efficiencyPoints = _sceneManager.GetComponent<StatsManager>().efficientlyPlaced * _sceneManager.GetComponent<NewGameManager>().pointsForEfficiency;
        _upgradePoints = _sceneManager.GetComponent<StatsManager>().TotalUpgradesPlaced * _sceneManager.GetComponent<NewGameManager>().pointsForUpgrades;
        _cowPoints = _sceneManager.GetComponent<StatsManager>().cowCounter * _sceneManager.GetComponent<NewGameManager>().pointsForCows;// just for fun

        // if you lose before placing 48 tiles:
        // reward the time you managed to stay in the game
        // 200 points per played minute (ingame: year) = 200pt / 365days = .54794521 points per day
        // .54794521 * 6.0833f = 3.3333 points per second = 200 points per minute 
        // 200 points per year = 3000 points after 15 mins/15 years played
        // if you finished the game (48 tiles) in 15 min you'd get 4000 points
        // .54794521 * days = points for time spent 
        if (_sceneManager.GetComponent<StatsManager>().tileCounter < 43)
        {
            _playtimePoints = .54794521f * days;
        }
        // if you manage to place 48 tiles:
        // base 2000 points
        // + 5000 - .54794521 * days
        // @ 8 years + 185 days = 5000 - .54794521 * 3105 days = 3298.63 pt
        // @ 12 years + 214 days = 5000 - .54794521 * 4594 days = 2485 pt
        // @ 18 years + 76 days = 5000 - .54794521 * 6646 days = 1358.36 pt
        else
        {
            _playtimePoints = 2000f + (5000f - .54794521f * days);
        }

        // Values for Display:
        // takes degenRate * 50 (fixed Framerate/pS) * 60 seconds
        // = degenrate of 1 year (ingame) == player get annual growth value!
        /*detailPros = ((ene * 50 * 60) * 10) * -1;
        detailEnv = ((env * 50 * 60) * 10) * -1;
        detailHap = ((hap * 50 * 60) * 10) * -1;*/

        // NEW Highscore Calculation:
        // these values represent score-points, not feedback values for players (as in X % growth per year).
        // at 0.00 degeneration rate you get 1000 points
        // worse than that you subtract points from 1000, if better you add them
        // formula if good: 1000 + ((degRate * -1) * 1,000,000) && if bad: 1000 - (degRate * 1,000,000)
        // example degRates: @.003 = -2000 pt, @.0009 = 100 pt, @.0006 = 400 (starting value), @.0001 = 900 pte, @-.0002 = 1200 pt, @-.003 = 4000

        /*Debug.Log(ene);
        Debug.Log(env);
        Debug.Log(hap);*/

        // this was for having people ONLY win when they manage to balance the game (outdated):
        /*
        if (ene > 0) // if UNBALANCED
        {
            ene = 1000f - (ene * 1000000f);  
        }else // if BALANCED
        {
            ene = 1000f + ((ene * -1) * 10000f);
        }

        if (env > 0) // if UNBALANCED
        {
            env = 1000f - (env * 1000000f);
        }else // if BALANCED
        {
            env = 1000f + ((env * -1) * 10000f);
        }

        if (hap > 0) // if UNBALANCED
        {
            hap = 1000f - (hap * 1000000f);
        }else // if BALANCED
        {
            hap = 1000f + ((hap * -1) * 10000f);
        }*/

        //neighborCount = _sceneManager.GetComponent<StatsManager>().efficientlyPlaced;
        // every well placed neighbor (+.1) will grant 50 points
        // ex.: 1.2 (= neighborBonus received at least 12 times (12 * .1)) = 600 points
        //float neighborEfficiencyBonus = neighborCount * 500f;
        Score = (int)_energyPoints + (int)_happinessPoints + (int)_environmentPoints + (int)_moneyPoints + (int)_efficiencyPoints + (int)_playtimePoints + _upgradePoints + _cowPoints;

        //Score = (int)ene + (int)env + (int)hap + (int)playtimeBonus + (int)neighborEfficiencyBonus;
        // Debug.Log("prosperity score " + (int)pros); // testing
        // Debug.Log("environment score " + (int)env); // testing
        // Debug.Log("happiness score " + (int)hap); // testing
        // Debug.Log("time score " + (int)playtimeBonus); // testing
        // Debug.Log("neighbor score " + (int)neighborEfficiencyBonus); // testing
        /*Debug.Log("prosScore " + ene);
        Debug.Log("envScore " + env);
        Debug.Log("hapScore " + hap);
        Debug.Log("timeScore " + playtimeBonus);
        Debug.Log("neighborScore " + neighborEfficiencyBonus);*/




        // old script safety: @ JAN - anything need to be saved?
        /*
        // werte zwichenspeichern für Detail Seite
        detailPros = pros;
        detailEnv = env;
        detailHap = hap;
         * 
        // Wenn einer der DegenerationRates positiv sein sollte, wird dieser auf null gesetzt um die Berechnung nicht zu störren 
        if (pros > 0)
        {
            pros *= -1;
        }
        if (env > 0)
        {
            env = 0;
        }
        if (hap > 0)
        {
            hap = 0;
        }
        pros = pros * -1 * 1000000;
        env = env * -1 * 1000000;
        hap = hap * -1 * 1000000;
        float playtimeBonus = years * -1; // currently not accounting for days...
        score = (int)pros + (int)env + (int)hap + (int)playtimeBonus;
        Debug.Log("score " + score); // added this to label log
        
        // Wenn man nicht alle Felder voll hat und dadurch verloren
        if (SceneManager.GetComponent < NeedsManager >().tileCounter < 48)//(pros == 0 & env == 0 & hap == 0)//wenn alle DegenarationRates positiv sind, ist score 0
        {
            score = (int)spentTime;
        }*/
    }

    public void DetailWindow()
    {
        int currentYear = (int)years + 2021; //Userfeedback start in 2021 oder Jahre die man gebraucht hat anzeigen
        int currentDay = (int)days - ((currentYear - 2021) * 365);

        // shows the year
        _yearsEntry = GameObject.Find("Years Entry");
        _yearsEntry.GetComponent<TextMeshProUGUI>().text = currentYear.ToString();

        // shows time played (years + days)
        _timeEntry = GameObject.Find("Time Entry");
        _timeEntry.GetComponent<TextMeshProUGUI>().text = years.ToString("0.") + " years, " + currentDay + " days";

        // shows points for efficiency:
        _efficiencyCountDisplay = GameObject.Find("efficiency Count");
        _efficiencyCountDisplay.GetComponent<TextMeshProUGUI>().text = _efficiencyPoints.ToString("0.");

        // shows points for upgrades:
        _upgradeEntry = GameObject.Find("Upgrade Count");
        _upgradeEntry.GetComponent<TextMeshProUGUI>().text = _upgradePoints.ToString("0.");

        // shows points for cows:
        _cowCountDisplay = GameObject.Find("cowCount");
        _cowCountDisplay.GetComponent<TextMeshProUGUI>().text = _cowPoints.ToString("0."); // currently this shows count = points (will change if you enter a multiplier in NewGameManager)

        // shows points for energy produced:
        _energyProducedEntry = GameObject.Find("Energy Entry");
        _energyProducedEntry.GetComponent<TextMeshProUGUI>().text = _energyPoints.ToString("0.00");

        // shows points for happiness produced:
        _happinessProducedEntry = GameObject.Find("Happiness Entry");
        _happinessProducedEntry.GetComponent<TextMeshProUGUI>().text = _happinessPoints.ToString("0.00");

        // shows points for environment produced:
        _environmentProducedEntry = GameObject.Find("Environment Entry");
        _environmentProducedEntry.GetComponent<TextMeshProUGUI>().text = _environmentPoints.ToString("0.00");

        // shows points for money produced:
        _moneyProducedEntry = GameObject.Find("Money Entry");
        _moneyProducedEntry.GetComponent<TextMeshProUGUI>().text = _moneyPoints.ToString();

        // shows Score (total):
        _scoreEntry = GameObject.Find("ScoreCount");
        _scoreEntry.GetComponent<TextMeshProUGUI>().text = Score.ToString();

        //Test 
        /*YearsEntry.GetComponent<TextMeshProUGUI>().text = "Test";
        timeEntry.GetComponent<TextMeshProUGUI>().text = "Test";
        neighborCountDisplay.GetComponent<TextMeshProUGUI>().text = "Test";
        UpgradeEntry.GetComponent<TextMeshProUGUI>().text = "Test";
        scoreEntry.GetComponent<TextMeshProUGUI>().text = "Test";
        ProsEntry.GetComponent<TextMeshProUGUI>().text = "Test";
        EnvEntry.GetComponent<TextMeshProUGUI>().text = "Test";
        HapEntry.GetComponent<TextMeshProUGUI>().text = "Test";
        MoneyEntry.GetComponent<TextMeshProUGUI>().text = "Test";*/
    }

}
