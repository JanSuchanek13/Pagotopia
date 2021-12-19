using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ContinuousStats : MonoBehaviour
{
    public float prosperityStat;
    public float happinessStat;
    public float environmentStat;

    float randomFloat;
    public float mainStat;
    public float randomStat1;
    public float randomStat2;

    public bool wasPlaced = false;

    [SerializeField] float minStatsPerTile = 3f;
    [SerializeField] float maxStatsPerTile = 6f;

    [SerializeField] float minInfluencePerTile = -.0125f;
    [SerializeField] float maxInfluencePerTile = .0125f;

    public bool isCow = false;

    public void RandomizeStats(GameObject tile)
    {
        GameObject SceneManager = GameObject.Find("SceneManager");

        if (SceneManager.GetComponent<GameManager>().usingContinuousValues) // can be deleted if we chose a one-time-effect for good
        {
            randomFloat = Random.Range(minInfluencePerTile, maxInfluencePerTile);
            mainStat = randomFloat / 2 + Random.Range(0f, (randomFloat / 2f));
            randomStat1 = randomFloat * Random.Range(-.5f, .5f);
            randomStat2 = randomFloat * Random.Range(-.5f, .5f);

            // this prevents all stats from being positive or the same
            if (randomStat1 >= 0f && randomStat2 >= 0f | randomStat1 == randomStat2)
            {
                int randomStat = Random.Range(1, 2);
                if(randomStat == 1)
                {
                    randomStat1 = randomStat1 * -1;
                } else
                {
                    randomStat2 = randomStat2 * -1;
                }
            }

            Transform myPosition = transform;

            if (tag == "factory")
            {
                prosperityStat = mainStat;
                happinessStat = randomStat1;
                environmentStat = randomStat2;
            }
            else if (tag == "nature")
            {
                prosperityStat = randomFloat * Random.Range(-1f, 0f); // nature always costs money
                happinessStat = randomFloat * Random.Range(0f, .5f); // nature always makes people happy
                environmentStat = mainStat;
            }
            else if (tag == "social")
            {
                prosperityStat = randomStat1;
                happinessStat = mainStat;
                environmentStat = randomStat2;
            }
            else if (tag == "sustainable")
            {
                prosperityStat = randomStat2;
                happinessStat = randomStat1;
                environmentStat = randomFloat;
            }
        }

        //... old version:

        if (SceneManager.GetComponent<GameManager>().usingOneTimeValues) // can be deleted if we chose continous-effects for good
        {
            randomFloat = Random.Range(minStatsPerTile, maxStatsPerTile);
            mainStat = randomFloat / 2 + Random.Range(0f, (randomFloat / 2f));
            randomStat1 = randomFloat * Random.Range(-.5f, .5f);
            randomStat2 = randomFloat * Random.Range(-.5f, .5f);

            // this prevents all stats from being positive or the same
            if (randomStat1 >= 0f && randomStat2 >= 0f | randomStat1 == randomStat2)
            {
                int randomStat = Random.Range(1, 2);
                if (randomStat == 1)
                {
                    randomStat1 = randomStat1 * -1;
                }
                else
                {
                    randomStat2 = randomStat2 * -1;
                }
            }

            Transform myPosition = transform;

            if (tag == "factory")
            {
                prosperityStat = mainStat;
                happinessStat = randomStat1;
                environmentStat = randomStat2;
            }
            else if (tag == "nature")
            {
                prosperityStat = randomFloat * Random.Range(-1f, 0f); // nature always costs money
                happinessStat = randomFloat * Random.Range(0f, .5f); // nature always makes people happy
                environmentStat = mainStat;
            }
            else if (tag == "social")
            {
                prosperityStat = randomStat1;
                happinessStat = mainStat;
                environmentStat = randomStat2;
            }
            else if (tag == "sustainable")
            {
                prosperityStat = randomStat2;
                happinessStat = randomStat1;
                environmentStat = randomFloat;
            }
        }
    }
    public void UpdateStats() // called when placing tile
    {
        wasPlaced = true;
        GameObject SceneManager = GameObject.Find("SceneManager");

        if (SceneManager.GetComponent<GameManager>().usingContinuousValues) // can be deleted if we chose a one-time-effect for good
        {
            SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate += prosperityStat;
            if (SceneManager.GetComponent<NeedsManager>().prosperityValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().prosperityValue = 10f;
            }
            SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate += environmentStat;
            if (SceneManager.GetComponent<NeedsManager>().environmentValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().environmentValue = 10f;
            }
            SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate += happinessStat;
            if (SceneManager.GetComponent<NeedsManager>().happinessValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().happinessValue = 10f;
            }

            SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();

            SceneManager.GetComponent<TileGenerator>().NextSet();
            return;
        }

        //... old version: 

        if (SceneManager.GetComponent<GameManager>().usingOneTimeValues) // can be deleted if we chose continous-effects for good
        {
            SceneManager.GetComponent<NeedsManager>().prosperityValue += prosperityStat;
            if (SceneManager.GetComponent<NeedsManager>().prosperityValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().prosperityValue = 10f;
            }
            SceneManager.GetComponent<NeedsManager>().environmentValue += environmentStat;
            if (SceneManager.GetComponent<NeedsManager>().environmentValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().environmentValue = 10f;
            }
            SceneManager.GetComponent<NeedsManager>().happinessValue += happinessStat;
            if (SceneManager.GetComponent<NeedsManager>().happinessValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().happinessValue = 10f;
            }

            SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();

            SceneManager.GetComponent<TileGenerator>().NextSet();
            return;
        }
    }

    /*public void InterdependenceEffect(neighbor.tag)
    {
        if (neighbor.tag == "nature")
        {}

    }*/



}