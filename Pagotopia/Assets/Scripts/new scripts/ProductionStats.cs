using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionStats : MonoBehaviour
{
    #region variables
    [Header("Tile Settings:")]
    [SerializeField] GameObject TierII;
    [SerializeField] GameObject TierIII;
    [SerializeField] GameObject SensorArray1;
    [SerializeField] GameObject SensorArray2;
    [SerializeField] GameObject SensorArray3;

    [Header("Tile Sounds:")]
    [SerializeField] AudioSource build_Sound;
    [SerializeField] AudioSource upgrade_Sound;

    [Header("(handshakes - don't touch)")]
    public bool wasPlaced = false;
    public int tierLevel = 0;
    private GameObject _sceneManager;
    private float _productionValue = 0f;
    private float _constructionCost;
    private float _upgradeCost;
    private float _productionCostPerMinute;
    private float _oneTimePayOff;
    // these are used by StatUIDisplay to display this particular tiles stats:
    public float thisTilesCurrentProductionValue = 0f;
    public float thisTilesCurrentProductionCost = 0f;
    #endregion

    void Awake()
    {
        _sceneManager = GameObject.Find("SceneManager");
        _productionValue = _sceneManager.GetComponent<NewGameManager>().baseProductionValuePerMinute / 50f / 60f;
        _constructionCost = _sceneManager.GetComponent<NewGameManager>().baseProductionConstructionCost;
        _oneTimePayOff = _sceneManager.GetComponent<NewGameManager>().baseProductionOneTimePayOff;
        _productionCostPerMinute = _sceneManager.GetComponent<NewGameManager>().baseProductionCostPerMinute / 50 / 60;
        _upgradeCost = _sceneManager.GetComponent<NewGameManager>().upgradeCost;
    }

    // called when Upgrade-Tile is placed onto this built tile:
    public void Upgrade()
    {
        if(tierLevel == 1)
        {
            _sceneManager.GetComponent<StatsManager>().availableMoney -= _upgradeCost;
            _sceneManager.GetComponent<StatsManager>().TotalUpgradesPlaced++;
            tierLevel++; // now: tier II
            _productionValue += _sceneManager.GetComponent<NewGameManager>().tier2ProductionValuePerMinute / 50f / 60f;
            _sceneManager.GetComponent<StatsManager>().upkeep += _sceneManager.GetComponent<NewGameManager>().tier2ProductionCostPerMinute / 50 / 60;
            TierII.SetActive(true);

            if (CompareTag("energy"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateEnergyProduction(_productionValue);
                _sceneManager.GetComponent<StatsManager>().energyValue += (_oneTimePayOff / 2);
            }
            if (CompareTag("happiness"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateHappinessProduction(_productionValue);
                _sceneManager.GetComponent<StatsManager>().happinessValue += (_oneTimePayOff / 2);
            }
            if (CompareTag("environment"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateEnvironmentProduction(_productionValue);
                _sceneManager.GetComponent<StatsManager>().environmentValue += (_oneTimePayOff / 2);
            }

            upgrade_Sound.Play();
            Debug.Log("upkeep shall now be " + _sceneManager.GetComponent<StatsManager>().upkeep);
            SensorArray2.SetActive(true);

            // updating tile stats to report to StatUIDisplay:
            thisTilesCurrentProductionValue += _sceneManager.GetComponent<NewGameManager>().tier2ProductionValuePerMinute;
            thisTilesCurrentProductionCost += _sceneManager.GetComponent<NewGameManager>().tier2ProductionCostPerMinute;
        }
        else if (tierLevel == 2)
        {
            _sceneManager.GetComponent<StatsManager>().availableMoney -= _upgradeCost;
            _sceneManager.GetComponent<StatsManager>().TotalUpgradesPlaced++;
            tierLevel++; // now: tier III
            _productionValue += _sceneManager.GetComponent<NewGameManager>().tier3ProductionValuePerMinute / 50f / 60f;
            _sceneManager.GetComponent<StatsManager>().upkeep += _sceneManager.GetComponent<NewGameManager>().tier3ProductionCostPerMinute / 50 / 60;
            TierIII.SetActive(true);

            if (CompareTag("energy"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateEnergyProduction(_productionValue);
                _sceneManager.GetComponent<StatsManager>().energyValue += (_oneTimePayOff / 2);
            }
            if (CompareTag("happiness"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateHappinessProduction(_productionValue);
                _sceneManager.GetComponent<StatsManager>().happinessValue += (_oneTimePayOff / 2);
            }
            if (CompareTag("environment"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateEnvironmentProduction(_productionValue);
                _sceneManager.GetComponent<StatsManager>().environmentValue += (_oneTimePayOff / 2);
            }

            upgrade_Sound.Play();
            Debug.Log("upkeep shall now be " + _sceneManager.GetComponent<StatsManager>().upkeep);
            SensorArray3.SetActive(true);

            // updating tile stats to report to StatUIDisplay:
            thisTilesCurrentProductionValue += _sceneManager.GetComponent<NewGameManager>().tier3ProductionValuePerMinute;
            thisTilesCurrentProductionCost += _sceneManager.GetComponent<NewGameManager>().tier3ProductionCostPerMinute;
        }
    }

    // called when being placed on grid:
    public void Build()
    {
            wasPlaced = true;
            build_Sound.Play();
            _sceneManager.GetComponent<StatsManager>().tileCounter++;
            _sceneManager.GetComponent<StatsManager>().TileCounterFeedbackToPlayer();
            _sceneManager.GetComponent<StatsManager>().availableMoney -= _constructionCost;
            _sceneManager.GetComponent<StatsManager>().upkeep += _productionCostPerMinute;
            if (CompareTag("energy"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateEnergyProduction(_productionValue);
                _sceneManager.GetComponent<StatsManager>().energyValue += _oneTimePayOff;
            }
            if (CompareTag("happiness"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateHappinessProduction(_productionValue);
                _sceneManager.GetComponent<StatsManager>().happinessValue += _oneTimePayOff;
            }
            if (CompareTag("environment"))
            {
                _sceneManager.GetComponent<StatsManager>().UpdateEnvironmentProduction(_productionValue);
                _sceneManager.GetComponent<StatsManager>().environmentValue += _oneTimePayOff;
            }
            tierLevel++; // now: tier I

        // turn off all sensor-arrays when building, and turn only tier 1 back on:
        SensorArray1.SetActive(false);
        SensorArray2.SetActive(false);
        SensorArray3.SetActive(false);
        SensorArray1.SetActive(true);
        // toggle shaders, as turning off sensor-arrays alone does not fullfill the OnExit-requirements of the influencers:
        _sceneManager.GetComponent<ToggleAreas>().UpdateShaders();

        // updating tile stats to report to StatUIDisplay:
        thisTilesCurrentProductionValue += _sceneManager.GetComponent<NewGameManager>().baseProductionValuePerMinute;
            thisTilesCurrentProductionCost += _sceneManager.GetComponent<NewGameManager>().baseProductionCostPerMinute;
    }
}
