using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIDisplay : MonoBehaviour
{
    [SerializeField] Slider prosperityBar;
    [SerializeField] Slider happinessBar;
    [SerializeField] Slider environmentBar;

    [SerializeField] Slider NEGprosperityBar;
    [SerializeField] Slider NEGhappinessBar;
    [SerializeField] Slider NEGenvironmentBar;

    [SerializeField] TextMeshProUGUI nameField;
    [SerializeField] TextMeshProUGUI tagField;

    // this is to display changed STATS through the neihbor-effect:
    [SerializeField] Slider prosperityBonusBar;
    [SerializeField] Slider happinessBonusBar;
    [SerializeField] Slider environmentBonusBar;

    [SerializeField] Slider NEGprosperityBonusBar;
    [SerializeField] Slider NEGhappinessBonusBar;
    [SerializeField] Slider NEGenvironmentBonusBar;

    GameObject SceneManager;

    float pS;
    float hS;
    float eS;

    private void Start()
    {
        ResetStatBars();
        ResetBonusStatBars();
        SceneManager = GameObject.Find("SceneManager");
    }
    public void CastStatsToUI(GameObject gO)
    {
        if(gO.CompareTag("environment"))
        {
            nameField.text = gO.name;
            tagField.text = "Natur";
        }
        if (gO.CompareTag("energy"))
        {
            nameField.text = gO.name;
            tagField.text = "Energie";
        }
        if (gO.CompareTag("happiness"))
        {
            nameField.text = gO.name;
            tagField.text = "Versorgung"; // lebensqualitaet, Gemeinde, 
        }
        if (gO.CompareTag("village"))
        {
            nameField.text = gO.name;
            tagField.text = "Dorf";
        }
        if (gO.CompareTag("city"))
        {
            nameField.text = "Pagotopia";
            tagField.text = "Dorf"; // "\n" to add another line 
        }

        // this displays Stats of Tiles that are hovered over
        //if (SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab = null) // this should make it so it only displays old stats if nothing is being carried
            //if (SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab = null)
            //{
            if (hS > 0f) // show happiness
            {
                happinessBar.value = hS;
                NEGhappinessBar.value = 0f;
            }
            else
            {
                happinessBar.value = 0f;
                NEGhappinessBar.value = hS * -1;
            }

            if (pS > 0f) // show prosperity
            {
                prosperityBar.value = pS;
                NEGprosperityBar.value = 0f;
            }
            else
            {
                prosperityBar.value = 0f;
                NEGprosperityBar.value = pS * -1;
            }

            if (eS > 0f) // show environment
            {
                environmentBar.value = eS;
                NEGenvironmentBar.value = 0f;
            }
            else
            {
                environmentBar.value = 0f;
                NEGenvironmentBar.value = eS * -1;
            }
        //}

        //test:
        if (SceneManager.GetComponent<GameManager>().hoverInfoEnabled == false)
        {
            Debug.Log("CastStatsToUI is being called, even though it shouldnt");
        }
    }

    public void ResetStatBars()
    {
        prosperityBar.value = 0f;
        happinessBar.value = 0f;
        environmentBar.value = 0f;

        NEGprosperityBar.value = 0f;
        NEGhappinessBar.value = 0f;
        NEGenvironmentBar.value = 0f;

        nameField.text = null;
    }

    public void CastNeighborEffectToUI(float prosperityStat, float happinessStat, float environmentStat, float prosperityBonus, float happinessBonus, float environmentBonus)
    {
        if (SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab != null) // prob. redundant, as this only gets called in "ActivateCell" if this condition is true
        {
            float newProsperityStat = prosperityStat + prosperityBonus;
            float newHappinessStat = happinessStat + happinessBonus;
            float newEnvironmentStat = environmentStat + environmentBonus;

            // calculate amount of static stat +/- neighboreffect
            if (newHappinessStat > 0f) // show happiness
            {
                happinessBonusBar.value = newHappinessStat;
                NEGhappinessBonusBar.value = 0f;
            }else
            {
                happinessBonusBar.value = 0f;
                NEGhappinessBonusBar.value = newHappinessStat;
            }

            if (newProsperityStat > 0f) // show prosperity
            {
                prosperityBonusBar.value = newProsperityStat;
                NEGprosperityBonusBar.value = 0f;
            }else
            {
                prosperityBonusBar.value = 0f;
                NEGprosperityBonusBar.value = newProsperityStat;
            }

            if (newEnvironmentStat > 0f) // show environment
            {
                environmentBonusBar.value = newEnvironmentStat;
                NEGenvironmentBonusBar.value = 0f;
            }else
            {
                environmentBonusBar.value = 0f;
                NEGenvironmentBonusBar.value = newEnvironmentStat;
            }

            /*if (prosperityStat >= 0)
            {
                newProsperityStat = prosperityStat * (1 + prosperityBonus);
                prosperityBonusBar.value = newProsperityStat;
                NEGprosperityBonusBar.value = 0f;
            }else
            {
                newProsperityStat = prosperityStat * (1 + (-1 * prosperityBonus));
                prosperityBonusBar.value = 0f;
                NEGprosperityBonusBar.value = newProsperityStat * -1;
            }

            if (happinessStat >= 0)
            {
                newHappinessStat = happinessStat * (1 + happinessBonus);
                happinessBonusBar.value = newHappinessStat;
                NEGhappinessBonusBar.value = 0f;
            }else
            {
                newHappinessStat = happinessStat * (1 + (-1 * happinessBonus));
                happinessBonusBar.value = 0f;
                NEGhappinessBonusBar.value = newHappinessStat * -1;
            }

            if (environmentStat >= 0)
            {
                newEnvironmentStat = environmentStat * (1 + environmentBonus);
                environmentBonusBar.value = newEnvironmentStat;
                NEGenvironmentBonusBar.value = 0f;
            }else
            {
                newEnvironmentStat = environmentStat * (1 + (-1 * environmentBonus));
                environmentBonusBar.value = 0f;
                NEGenvironmentBonusBar.value = newEnvironmentStat * -1;
            }*/
        }else
        {
            ResetBonusStatBars(); // probably redundant, as this is handled in "ActivateCell"-OnMouseOver
        }
    }
    public void ResetBonusStatBars()
    {
        prosperityBonusBar.value = 0f;
        happinessBonusBar.value = 0f;
        environmentBonusBar.value = 0f;

        NEGprosperityBonusBar.value = 0f;
        NEGhappinessBonusBar.value = 0f;
        NEGenvironmentBonusBar.value = 0f;
    }
}
