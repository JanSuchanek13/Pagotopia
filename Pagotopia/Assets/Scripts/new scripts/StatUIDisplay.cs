using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIDisplay : MonoBehaviour
{
    #region variables:
    [Header("Hover UI-Settings:")]
    GameObject SceneManager;
    [SerializeField] GameObject productionFacility_UI;
    [SerializeField] GameObject villageFacility_UI;
    [SerializeField] GameObject upgrade_UI;
    [SerializeField] GameObject cell_UI;
    [SerializeField] GameObject mountain_UI;
    [SerializeField] TextMeshProUGUI nameField;
    [SerializeField] TextMeshProUGUI tagField;
    [Header("Production TMPro-Fields:")]
    [SerializeField] TextMeshProUGUI _productionTypeField;
    [SerializeField] TextMeshProUGUI _productionTierField;
    [SerializeField] TextMeshProUGUI _productionValueField;
    [SerializeField] TextMeshProUGUI _productionCostField;
    [SerializeField] GameObject _postBuildDisplay;
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
    [SerializeField] TextMeshProUGUI _upgradeCostField;
    [Header("Cell TMPro-Fields:")]
    [SerializeField] Image _energyInfluenceIcon;
    [SerializeField] Image _happinessInfluenceIcon;
    [SerializeField] Image _environmentInfluenceIcon;
    [SerializeField] Image _neighborInfluenceIcon;
    [SerializeField] GameObject _noneTextField;

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
            _upgradeCostField.text = (SceneManager.GetComponent<NewGameManager>().upgradeCost).ToString();
        }
        if (gO.CompareTag("mountain"))
        {
            nameField.text = "Berg";
            tagField.text = "";
            mountain_UI.SetActive(true);
        }
        if (gO.CompareTag("cell"))
        {
            nameField.text = "Baugrund";
            tagField.text = "";
            Transform _energyInfluence = gO.transform.Find("Energy_Icon");
            Transform _happinessInfluence = gO.transform.Find("Happiness_Icon");
            Transform _environmentInfluence = gO.transform.Find("Environment_Icon");
            Transform _neighborInfluence = gO.transform.Find("Neighbor_Icon");
            cell_UI.SetActive(true);

            if (_energyInfluence.GetComponent<MeshRenderer>().enabled)
            {
                _energyInfluenceIcon.enabled = true;
            }
            if (_happinessInfluence.GetComponent<MeshRenderer>().enabled)
            {
                _happinessInfluenceIcon.enabled = true;
            }
            if (_environmentInfluence.GetComponent<MeshRenderer>().enabled)
            {
                _environmentInfluenceIcon.enabled = true;
            }
            if (_neighborInfluence.GetComponent<MeshRenderer>().enabled)
            {
                _neighborInfluenceIcon.enabled = true;
            }
            if(!_energyInfluence.GetComponent<MeshRenderer>().enabled && !_happinessInfluence.GetComponent<MeshRenderer>().enabled && !_environmentInfluence.GetComponent<MeshRenderer>().enabled && !_neighborInfluence.GetComponent<MeshRenderer>().enabled)
            {
                // show, that no influences are present
                _noneTextField.SetActive(true);
            }
        }
        if (gO.CompareTag("environment"))
        {
            nameField.text = gO.name;
            tagField.text = "Natur";

            productionFacility_UI.SetActive(true);
            _productionTypeField.text = tagField.text;
            _productionTierField.text = gO.GetComponent<ProductionStats>().tierLevel + " / 3";
            environmentIcon.enabled = true;

            if (gO.GetComponent<ProductionStats>().wasPlaced) // shows variables for a built tile:
            {
                _postBuildDisplay.SetActive(true);
                _productionValueField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionValue).ToString();
                _productionCostField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionCost).ToString();
            } else // shows default variables for an unbuilt tile:
            {
                _productionValueField.text = (SceneManager.GetComponent<NewGameManager>().baseProductionValuePerMinute).ToString();
                _productionCostField.text = (SceneManager.GetComponent<NewGameManager>().baseProductionConstructionCost).ToString();
            }
        }
        if (gO.CompareTag("energy"))
        {
            nameField.text = gO.name;
            tagField.text = "Energie";

            productionFacility_UI.SetActive(true);
            _productionTypeField.text = tagField.text;
            _productionTierField.text = gO.GetComponent<ProductionStats>().tierLevel + " / 3";
            energyIcon.enabled = true;

            if (gO.GetComponent<ProductionStats>().wasPlaced) // shows variables for a built tile:
            {
                _postBuildDisplay.SetActive(true);
                _productionValueField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionValue).ToString();
                _productionCostField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionCost).ToString();
            } else // shows default variables for an unbuilt tile:
            {
                _productionValueField.text = (SceneManager.GetComponent<NewGameManager>().baseProductionValuePerMinute).ToString();
                _productionCostField.text = (SceneManager.GetComponent<NewGameManager>().baseProductionConstructionCost).ToString();
            }
        }
        if (gO.CompareTag("happiness"))
        {
            nameField.text = gO.name;
            tagField.text = "Versorgung"; // lebensqualitaet, Gemeinde, 

            productionFacility_UI.SetActive(true);
            _productionTypeField.text = tagField.text;
            _productionTierField.text = gO.GetComponent<ProductionStats>().tierLevel + " / 3";
            happinessIcon.enabled = true;

            if (gO.GetComponent<ProductionStats>().wasPlaced) // shows variables for a built tile:
            {
                _postBuildDisplay.SetActive(true);
                _productionValueField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionValue).ToString();
                _productionCostField.text = (gO.GetComponent<ProductionStats>().thisTilesCurrentProductionCost).ToString();
            }
            else // shows default variables for an unbuilt tile:
            {
                _productionValueField.text = (SceneManager.GetComponent<NewGameManager>().baseProductionValuePerMinute).ToString();
                _productionCostField.text = (SceneManager.GetComponent<NewGameManager>().baseProductionConstructionCost).ToString();
            }
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
    }

    public void ResetStatBars()
    {
        nameField.text = null;
        tagField.text = null;

        if(cell_UI == isActiveAndEnabled)
        {
            _energyInfluenceIcon.enabled = false;
            _happinessInfluenceIcon.enabled = false;
            _environmentInfluenceIcon.enabled = false;
            _neighborInfluenceIcon.enabled = false;
            _noneTextField.SetActive(false);
            cell_UI.SetActive(false);
        }
        if (mountain_UI == isActiveAndEnabled)
        {
            mountain_UI.SetActive(false);
        }
        if (productionFacility_UI == isActiveAndEnabled)
        {
                _productionTypeField.text = null;
                _productionTierField.text = null;
                _productionValueField.text = null;
                _productionCostField.text = null;
                energyIcon.enabled = false;
                happinessIcon.enabled = false;
                environmentIcon.enabled = false;
                _postBuildDisplay.SetActive(false);
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
        if (upgrade_UI == isActiveAndEnabled)
        {
                upgrade_UI.SetActive(false);
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
            } else
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
