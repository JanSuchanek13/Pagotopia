using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculateHighscore : MonoBehaviour
{
    // private variables:
    GameObject SceneManager;
    GameObject YearsEntry;
    GameObject ProsEntry;
    GameObject EnvEntry;
    GameObject HapEntry;
    GameObject timeEntry;
    GameObject neighborCountDisplay;
    GameObject cowCountDisplay;
    GameObject scoreEntry;

    float startTime;
    float days;
    float years;
    float neighborCount;
    int cowCount;

    // public variables:
    public float detailPros;
    public float detailEnv;
    public float detailHap;
    public int score;

    void Awake()
    {
        startTime = Time.timeSinceLevelLoad;
        //Debug.Log("Starttime: " + startTime);
    }

    public void Calculate()
    {
        // Zeit berechnen die man gespielt hat
        float currentTime = Time.timeSinceLevelLoad;
        float spentTime = currentTime;// - startTime;
        days = spentTime * 6.0833f; // currently a round takes about 6-8 minutes
        // 9/s would be 1.4795 years per minute
        // 3.041667/s would be .5 years per minute
        
        years = days / 365;
        //Debug.Log("years " + years);

        SceneManager = GameObject.Find("SceneManager");
        float ene = SceneManager.GetComponent<StatsManager>()._energyProductionRate;
        float env = SceneManager.GetComponent<StatsManager>()._environmentProductionRate;
        float hap = SceneManager.GetComponent<StatsManager>()._happinessProductionRate;

        // Values for Display:
        // takes degenRate * 50 (fixed Framerate/pS) * 60 seconds
        // = degenrate of 1 year (ingame) == player get annual growth value!
        detailPros = ((ene * 50 * 60) * 10) * -1;
        detailEnv = ((env * 50 * 60) * 10) * -1;
        detailHap = ((hap * 50 * 60) * 10) * -1;

        // NEW Highscore Calculation:
        // these values represent score-points, not feedback values for players (as in X % growth per year).
        // at 0.00 degeneration rate you get 1000 points
        // worse than that you subtract points from 1000, if better you add them
        // formula if good: 1000 + ((degRate * -1) * 1,000,000) && if bad: 1000 - (degRate * 1,000,000)
        // example degRates: @.003 = -2000 pt, @.0009 = 100 pt, @.0006 = 400 (starting value), @.0001 = 900 pte, @-.0002 = 1200 pt, @-.003 = 4000

        Debug.Log(ene);
        Debug.Log(env);
        Debug.Log(hap);

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
        }

        // if you lose before placing 48 tiles:
        // reward the time you managed to stay in the game
        // 200 points per played minute (ingame: year) = 200pt / 365days = .54794521 points per day
        // .54794521 * 6.0833f = 3.3333 points per second = 200 points per minute 
        // 200 points per year = 3000 points after 15 mins/15 years played
        // if you finished the game (48 tiles) in 15 min you'd get 4000 points
        // .54794521 * days = points for time spent 
        float playtimeBonus;
        if (SceneManager.GetComponent<StatsManager>().tileCounter < 48)
        {
            playtimeBonus = .54794521f * days;
        }
        // if you manage to place 48 tiles:
        // base 2000 points
        // + 5000 - .54794521 * days
        // @ 8 years + 185 days = 5000 - .54794521 * 3105 days = 3298.63 pt
        // @ 12 years + 214 days = 5000 - .54794521 * 4594 days = 2485 pt
        // @ 18 years + 76 days = 5000 - .54794521 * 6646 days = 1358.36 pt
        else
        {
            playtimeBonus = 2000f + (5000f - .54794521f * days);
        }

        neighborCount = SceneManager.GetComponent<StatsManager>().efficientlyPlaced;
        // every well placed neighbor (+.1) will grant 50 points
        // ex.: 1.2 (= neighborBonus received at least 12 times (12 * .1)) = 600 points
        float neighborEfficiencyBonus = neighborCount * 500f;

        // no points for cows - because technically they are not sustainable at all and shouldnt be saught after
        cowCount = SceneManager.GetComponent<StatsManager>().cowCounter; // just for fun

        score = (int)ene + (int)env + (int)hap + (int)playtimeBonus + (int)neighborEfficiencyBonus;
        // Debug.Log("prosperity score " + (int)pros); // testing
        // Debug.Log("environment score " + (int)env); // testing
        // Debug.Log("happiness score " + (int)hap); // testing
        // Debug.Log("time score " + (int)playtimeBonus); // testing
        // Debug.Log("neighbor score " + (int)neighborEfficiencyBonus); // testing
        Debug.Log("prosScore " + ene);
        Debug.Log("envScore " + env);
        Debug.Log("hapScore " + hap);
        Debug.Log("timeScore " + playtimeBonus);
        Debug.Log("neighborScore " + neighborEfficiencyBonus);




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
        YearsEntry = GameObject.Find("Years Entry");
        YearsEntry.GetComponent<TextMeshProUGUI>().text = currentYear.ToString();
        // shows time played (years + days)
        timeEntry = GameObject.Find("Time Entry");
        timeEntry.GetComponent<TextMeshProUGUI>().text = years.ToString("0.") + " years, " + currentDay + " days";
        // shows how efficiently neighbors were placed
        // ex.: 13 should show as 130 to be more impressive
        neighborCountDisplay = GameObject.Find("neighborCount");
        neighborCountDisplay.GetComponent<TextMeshProUGUI>().text = (neighborCount * 100).ToString("0."); 
        // shows amount of cows placed --> just for fun = no effect
        cowCountDisplay = GameObject.Find("cowCount");
        cowCountDisplay.GetComponent<TextMeshProUGUI>().text = cowCount.ToString(); 
        // displays total score
        scoreEntry = GameObject.Find("ScoreCount");
        scoreEntry.GetComponent<TextMeshProUGUI>().text = score.ToString();

        ProsEntry = GameObject.Find("Pros Entry");
        ProsEntry.GetComponent<TextMeshProUGUI>().text = detailPros.ToString("0.00") + " %";

        EnvEntry = GameObject.Find("Env Entry");
        EnvEntry.GetComponent<TextMeshProUGUI>().text = detailEnv.ToString("0.00") + " %";

        HapEntry = GameObject.Find("Hap Entry");
        HapEntry.GetComponent<TextMeshProUGUI>().text = detailHap.ToString("0.00") + " %";

    }

}
