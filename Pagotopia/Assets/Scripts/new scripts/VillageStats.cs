using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VillageStats : MonoBehaviour
{
    #region variables
    [Header("Tile Settings:")]
    [SerializeField] GameObject TierII;
    [SerializeField] GameObject TierIII;
    [SerializeField] GameObject TierIV;
    [SerializeField] GameObject TierV;
    [SerializeField] GameObject incomeDisplay;
    [SerializeField] TextMesh _currentIncomeDisplay;
    [SerializeField] GameObject neighborSensorArray;
    //[SerializeField] GameObject altDisplay;

    [Header("Tile Sounds:")]
    [SerializeField] AudioSource build_Sound;
    [SerializeField] AudioSource upgrade_Sound; // happy kids? people laughing or talking?
    [SerializeField] AudioSource cashRegister_Sound; // Ka-Ching!

    [Header("(handshakes - don't touch)")]
    //private LookAt _LookAtScript;
    public bool wasPlaced = false;
    public bool influencedByEnergy = false;
    private bool _energyAccounted = false; // prevents double-counts
    public bool influencedByHappiness = false;
    private bool _happinessAccounted = false; // prevents double-counts
    public bool influencedByNature = false;
    private bool _natureAccounted = false; // prevents double-counts
    public bool influencedByNeighbors = false;
    public bool _neighborsAccounted = false;
    private GameObject _sceneManager;
    private float _bonusTaxes;
    private float _constructionCost;
    // these are public so StatUIDisplay can read them:
    public float _frequencyToPay;
    public float _costOfLiving;
    public float _taxesToPay;
    public int _tierLevel;
    #endregion

    void Awake()
    {
        _sceneManager = GameObject.Find("SceneManager");
        _costOfLiving = _sceneManager.GetComponent<NewGameManager>().baseCostOfLivingPerMinute / 50f / 60f;
        _frequencyToPay = _sceneManager.GetComponent<NewGameManager>().taxationFrequency; // in seconds
        _taxesToPay = _sceneManager.GetComponent<NewGameManager>().baseTaxesGeneratedPerMinute / (60 / _frequencyToPay);
        _bonusTaxes = _sceneManager.GetComponent<NewGameManager>().bonusTaxes;
        _constructionCost = _sceneManager.GetComponent<NewGameManager>().baseVillageConstructionCost;
        incomeDisplay.SetActive(false);

        // build Pagotopia:
        if (CompareTag("city") == true)
        {
            _tierLevel++; // now: TierI
            wasPlaced = true;
            _sceneManager.GetComponent<StatsManager>().tileCounter++;
            _sceneManager.GetComponent<StatsManager>().cowCounter++;
            StartCoroutine("GenerateIncome"); // starts money generation
            _sceneManager.GetComponent<StatsManager>().UpdateCostOfLiving(_costOfLiving);
            neighborSensorArray.SetActive(false);
            neighborSensorArray.SetActive(true); // tells all adjacent tiles they now have a neighbor
        }
    }

    void FixedUpdate()
    {
        if(_tierLevel < 5)
        {
            if (influencedByEnergy && !_energyAccounted)
            {
                _energyAccounted = true; // stops counting it again
                Upgrade();
            }
            if (influencedByHappiness && !_happinessAccounted)
            {
                _happinessAccounted = true; // stops counting it again
                Upgrade();
            }
            if (influencedByNature && !_natureAccounted)
            {
                _natureAccounted = true; // stops counting it again
                Upgrade();
            }
            if (influencedByNeighbors && !_neighborsAccounted)
            {
                Debug.Log(gameObject.name + " accounted for neighbor and is upgrading");
                Upgrade();
                _neighborsAccounted = true; // stops counting it again

            }
        }
    }

    // AUTOMATICALY upgrades (once), when new influence is detected:
    public void Upgrade()
    {
        if (_tierLevel == 1)
        {
            _tierLevel++; // now: tier II
            _taxesToPay += _bonusTaxes;
            //TierII.GetComponent<MeshRenderer>().enabled = true;
            TierII.SetActive(true);
            upgrade_Sound.Play();
            if (CompareTag("city") == true)
            {
                _sceneManager.GetComponent<StatsManager>().cowCounter++;
            }
        }
        else if (_tierLevel == 2)
        {
            _tierLevel++; // now: tier III
            _taxesToPay += _bonusTaxes;
            //TierIII.GetComponent<MeshRenderer>().enabled = true;
            TierIII.SetActive(true);
            upgrade_Sound.Play();
            if (CompareTag("city") == true)
            {
                _sceneManager.GetComponent<StatsManager>().cowCounter++;
            }
        }
        else if (_tierLevel == 3)
        {
            _tierLevel++; // now: tier IV
            _taxesToPay += _bonusTaxes;
            //TierIV.GetComponent<MeshRenderer>().enabled = true;
            TierIV.SetActive(true);
            upgrade_Sound.Play();
            if (CompareTag("city") == true)
            {
                _sceneManager.GetComponent<StatsManager>().cowCounter++;
            }
        }
        else if (_tierLevel == 4)
        {
            _tierLevel++; // now: tier V
            _taxesToPay += _bonusTaxes;
            //TierV.GetComponent<MeshRenderer>().enabled = true;
            TierV.SetActive(true);
            upgrade_Sound.Play();
            if (CompareTag("city") == true)
            {
                _sceneManager.GetComponent<StatsManager>().cowCounter++;
            }
        }
    }

    // generate income:
    IEnumerator GenerateIncome()
    {
        yield return new WaitForSeconds(_frequencyToPay);

        if (_sceneManager.GetComponent<VictoryScript>().gameHasEnded == false)
        {
            _sceneManager.GetComponent<StatsManager>().availableMoney += _taxesToPay;
            _sceneManager.GetComponent<StatsManager>().TotalMoneyProduced += _taxesToPay; // for highscore-calculation
            _sceneManager.GetComponent<StatsManager>().IncomeFeedbackToPlayer();
            _sceneManager.GetComponent<StatsManager>().efficientlyPlaced += _tierLevel;
            StartCoroutine("DisplayIncome");
            StartCoroutine("GenerateIncome"); // restart money-generation-timer
        }
    }

    // audiovisual feedback to player (about income):
    IEnumerator DisplayIncome()
    {
        incomeDisplay.SetActive(true);

        cashRegister_Sound.Play();
        _currentIncomeDisplay.text = _taxesToPay.ToString();
        //altDisplay.GetComponent<TextMesh>().text = _taxesToPay.ToString();

        // turns all MeshRenderers in the children of "incomeDisplay" on & off:
        // --> doesn't work if put into single "foreach" loop.
        MeshRenderer[] _drawableObjects = incomeDisplay.GetComponentsInChildren<MeshRenderer>();
        foreach (var r in _drawableObjects)
        {
            r.enabled = true;
        }
        yield return new WaitForSeconds(1.5f);
        foreach (var r in _drawableObjects)
        {
            r.enabled = false;
        }
    }

    // called when being placed on grid:
    public void Build()
    {
        _tierLevel++; // now: TierI
        wasPlaced = true;
        _sceneManager.GetComponent<StatsManager>().tileCounter++;
        _sceneManager.GetComponent<StatsManager>().TileCounterFeedbackToPlayer();
        _sceneManager.GetComponent<StatsManager>().energyValue -= _constructionCost;
        _sceneManager.GetComponent<StatsManager>().happinessValue -= _constructionCost;
        _sceneManager.GetComponent<StatsManager>().environmentValue -= _constructionCost;
        // we could consider houses also costing MONEY instead or in addition to resources!
        if (build_Sound != null)
            {
                build_Sound.Play();
            }
        StartCoroutine("GenerateIncome"); // starts money generation
        _sceneManager.GetComponent<StatsManager>().UpdateCostOfLiving(_costOfLiving);
        neighborSensorArray.SetActive(false);
        neighborSensorArray.SetActive(true); // tells all adjacent tiles they now have a neighbor
        _sceneManager.GetComponent<ToggleAreas>().UpdateShaders();
    }
}
