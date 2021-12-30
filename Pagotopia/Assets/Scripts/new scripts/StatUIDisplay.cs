using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIDisplay : MonoBehaviour
{
    #region variables:
    [Header("Hover UI-Settings:")]
    [SerializeField] GameObject productionFacility_UI;
    [SerializeField] GameObject villageFacility_UI;
    [SerializeField] GameObject upgrade_UI;
    [SerializeField] TextMeshProUGUI nameField;
    [SerializeField] TextMeshProUGUI tagField;
    [Header("Production TMPro-Fields:")]
    [SerializeField] TextMeshProUGUI _productionTypeField;
    [SerializeField] TextMeshProUGUI _productionTierField;
    [SerializeField] TextMeshProUGUI _productionValueField;
    [SerializeField] TextMeshProUGUI _productionCostField;
    [SerializeField] Image energyIcon;
    [SerializeField] Image happinessIcon;
    [SerializeField] Image environmentIcon;
    [Header("Village TMPro-Fields:")]
    [SerializeField] TextMeshProUGUI _villageTierField;
    [SerializeField] TextMeshProUGUI _villageIncomeField;
    [SerializeField] TextMeshProUGUI _villageUpkeepField;
    [SerializeField] GameObject _hasEnergyMark;
    [SerializeField] GameObject _hasHappinessMark;
    [SerializeField] GameObject _hasEnvironmentMark;
    [SerializeField] GameObject _hasNeighborsMark;
    [Header("Upgrade TMPro-Fields:")]
    //[SerializeField] TextMeshProUGUI _upgradeTypeField;
    //[SerializeField] TextMeshProUGUI _upgradeDescriptionField;
    //[SerializeField] TextMeshProUGUI _upgradeValueField;
    [SerializeField] TextMeshProUGUI _upgradeCostField;

    GameObject SceneManager;
    #endregion

    private void Start()
    {
        ResetStatBars();
        //ResetBonusStatBars();
        SceneManager = GameObject.Find("SceneManager");
    }
    public void CastStatsToUI(GameObject gO)
    {
        if (gO.CompareTag("Upgrade"))
        {
            nameField.text = "Modernisierung";
            tagField.text = "Upgrade";
            upgrade_UI.SetActive(true);
            //_upgradeTypeField.text = tagField.text;
            //_upgradeDescriptionField.text = "\"Modernisiere eine Produktionsanlage\"";
            //_upgradeValueField.text = "Platziere ein Upgrade auf Einer Energieanlage";
            //environmentIcon.enabled = true;
            _upgradeCostField.text = (SceneManager.GetComponent<NewGameManager>().upgradeCost).ToString();
        }
        if (gO.CompareTag("environment"))
        {
            nameField.text = gO.name;
            tagField.text = "Natur";

            productionFacility_UI.SetActive(true);
            _productionTypeField.text = tagField.text;
            _productionTierField.text = gO.GetComponent<ProductionStats>().tierLevel + " / 3";
            _productionValueField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionValue).ToString();
            environmentIcon.enabled = true;
            _productionCostField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionCost).ToString();
        }
        if (gO.CompareTag("energy"))
        {
            nameField.text = gO.name;
            tagField.text = "Energie";

            productionFacility_UI.SetActive(true);
            _productionTypeField.text = tagField.text;
            _productionTierField.text = gO.GetComponent<ProductionStats>().tierLevel + " / 3";
            _productionValueField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionValue).ToString();
            energyIcon.enabled = true;
            _productionCostField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionCost).ToString();
        }
        if (gO.CompareTag("happiness"))
        {
            nameField.text = gO.name;
            tagField.text = "Versorgung"; // lebensqualitaet, Gemeinde, 

            productionFacility_UI.SetActive(true);
            _productionTypeField.text = tagField.text;
            _productionTierField.text = gO.GetComponent<ProductionStats>().tierLevel + " / 3";
            _productionValueField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionValue).ToString();
            happinessIcon.enabled = true;
            _productionCostField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionCost).ToString();
        }
        if (gO.CompareTag("village"))
        {
            nameField.text = gO.name;
            tagField.text = "Dorf";

            villageFacility_UI.SetActive(true);
            _villageTierField.text = gO.GetComponent<VillageStats>()._tierLevel + " / 5";
            _villageIncomeField.text = (gO.GetComponent<VillageStats>()._taxesToPay).ToString()/* + "    /" + (gO.GetComponent<VillageStats>()._frequencyToPay).ToString() + "Sec."*/;
            _villageUpkeepField.text = (gO.GetComponent<VillageStats>()._costOfLiving * 50 * 60).ToString(); // calculations to revert math in Village Stats for UIDisplay.
            if (gO.GetComponent<VillageStats>().influencedByEnergy)
            {
                _hasEnergyMark.SetActive(true);
            }
            if (gO.GetComponent<VillageStats>().influencedByHappiness)
            {
                _hasHappinessMark.SetActive(true);
            }
            if (gO.GetComponent<VillageStats>().influencedByNature)
            {
                _hasEnvironmentMark.SetActive(true);
            }
            if (gO.GetComponent<VillageStats>().influencedByNeighbors)
            {
                _hasNeighborsMark.SetActive(true);
            }
        }
        if (gO.CompareTag("city"))
        {
            nameField.text = "Pagotopia";
            tagField.text = ""; // "\n" to add another line 

            villageFacility_UI.SetActive(true);
            _villageTierField.text = (gO.GetComponent<VillageStats>()._tierLevel).ToString() + " / 5";
            _villageIncomeField.text = (gO.GetComponent<VillageStats>()._taxesToPay).ToString()/* + " /" + (gO.GetComponent<VillageStats>()._frequencyToPay).ToString() + "Sec."*/;
            _villageUpkeepField.text = (gO.GetComponent<VillageStats>()._costOfLiving * 50 * 60).ToString(); // calculations to revert math in Village Stats for UIDisplay.
            if (gO.GetComponent<VillageStats>().influencedByEnergy)
            {
                _hasEnergyMark.SetActive(true);
            }
            if (gO.GetComponent<VillageStats>().influencedByHappiness)
            {
                _hasHappinessMark.SetActive(true);
            }
            if (gO.GetComponent<VillageStats>().influencedByNature)
            {
                _hasEnvironmentMark.SetActive(true);
            }
            if (gO.GetComponent<VillageStats>().influencedByNeighbors)
            {
                _hasNeighborsMark.SetActive(true);
            }
        }

        // this displays Stats of Tiles that are hovered over
        //if (SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab = null) // this should make it so it only displays old stats if nothing is being carried
        //if (SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab = null)
        //{
        /*if (hS > 0f) // show happiness
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
            }*/
        //}

        /*test:
        if (SceneManager.GetComponent<GameManager>().hoverInfoEnabled == false)
        {
            Debug.Log("CastStatsToUI is being called, even though it shouldnt");
        }*/
    }

    public void ResetStatBars()
    {
        nameField.text = null;
        tagField.text = null;
        // reset all display Fields:
        if (productionFacility_UI == isActiveAndEnabled)
        {
            _productionTypeField.text = null;
            _productionTierField.text = null;
            _productionValueField.text = null;
            _productionCostField.text = null;
            energyIcon.enabled = false;
            happinessIcon.enabled = false;
            environmentIcon.enabled = false;
            productionFacility_UI.SetActive(false);
        }
        if (villageFacility_UI == isActiveAndEnabled)
        {
            _villageTierField.text = null;
            _villageIncomeField.text = null;
            _villageUpkeepField.text = null;
            _hasEnergyMark.SetActive(false);
            _hasHappinessMark.SetActive(false);
            _hasEnvironmentMark.SetActive(false);
            _hasNeighborsMark.SetActive(false);
            villageFacility_UI.SetActive(false);
        }
    }










    // everything beyond here should be redundant
    public void CastNeighborEffectToUI(float prosperityStat, float happinessStat, float environmentStat, float prosperityBonus, float happinessBonus, float environmentBonus)
    {
        if (SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab != null) // prob. redundant, as this only gets called in "ActivateCell" if this condition is true
        {
            float newProsperityStat = prosperityStat + prosperityBonus;
            float newHappinessStat = happinessStat + happinessBonus;
            float newEnvironmentStat = environmentStat + environmentBonus;

            // calculate amount of static stat +/- neighboreffect
            /*if (newHappinessStat > 0f) // show happiness
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
        //prosperityBonusBar.value = 0f;
        //happinessBonusBar.value = 0f;
        //environmentBonusBar.value = 0f;

        //NEGprosperityBonusBar.value = 0f;
       //NEGhappinessBonusBar.value = 0f;
       // NEGenvironmentBonusBar.value = 0f;
    }
}
