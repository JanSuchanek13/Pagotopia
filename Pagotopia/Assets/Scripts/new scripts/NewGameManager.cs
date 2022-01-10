using UnityEngine;
using TMPro;

public class NewGameManager : MonoBehaviour
{
    #region variables
    [Header("General Settings:")]
    // have metric displays of all variables
    
    public float baseStatValue = 100f;
    public float startingMoneyValue = 1000;
    public float pointsForEfficiency = 100f; // used in StatsManager to calculate points for efficiency at game-end

    [Header("Map Settings:")]
    public GameObject pagotopia;

    [Header("Village-Tile Settings:")]
    public float baseVillageConstructionCost = 10f;
    public float baseCostOfLivingPerMinute = 25f;
    public float baseTaxesGeneratedPerMinute = 100;
    public float bonusTaxes = 50; // added per intervall per influence
    [Tooltip("(in seconds)")]
    public float taxationFrequency = 60f;

    [Header("Production-Tile Settings:")]
    public float baseProductionConstructionCost = 500;
    public float upgradeCost = 800f;
    public float baseProductionOneTimePayOff = 20f;
    public float baseProductionValuePerMinute = 50f;
    public float baseProductionCostPerMinute = 200f;
    [Header("Tier-II Settings:")]
    public float tier2ProductionValuePerMinute = 10f;
    public float tier2ProductionCostPerMinute = 15f;
    [Header("Tier-III Settings:")]
    public float tier3ProductionValuePerMinute = 15f;
    public float tier3ProductionCostPerMinute = 15f;

    [Header("Testing:")]
    public bool developerMode = false;
    public bool quickWin = false;



    // how fast do stats decay in general
    // updates 50/s or 3000x per minute
    //public float globalDegenerationRate = 0.1f;
    // this multiplicator accelerated decay when dropping below threshold
    // this is calculated in the "Start()"-function of "NeedsManager()"-script
    //[Tooltip("the degenerationThreshold is turnt into % relative to the \"baseStatValue\"")]
    //public float degenerationThreshold = 50f;
    // this multiplicator accelerated decay when dropping below threshold
    //public float accelleratedDegenerationRate = 1.5f;

    // Stats of tiles will be applied contineously
    //public bool usingContinuousValues = true;
    // public float minInfluencePerTile = .125f;
    //public float maxInfluencePerTile = .325f;
    // Multiply tiny values to show in UI-display, without changing the display
    //public float statDisplayMultiplicator = 200f;
    //[Tooltip("the degenerationFactor sets the factor at which the static stats of tiles wille contineously influence the degeneration rate")]
    //public float degenerationFactor = .01f;

    // this gets picked up in NEEDSMANAGER to adjust basic degeneration over time
    //public bool useGraduallyIncresedDegeneration;
    //public float speedUpDegenerationTime = 60f;
    //public float degenerationIncreaseOverTime = .001f;

    // Stats of tiles will be applied only once
    //public bool usingOneTimeValues = false;
    //public float minStatsPerTile = 3f;
    //public float maxStatsPerTile = 6f;

    // Separate, optional display that lets you read out the actual degeneration rates of all stats
    // becomes activated if using numerical degeneration-rates or stat-values (see above)
    //[Tooltip("becomes activated if using numerical degeneration-rates or stat-values (see above)")]

    //[SerializeField] GameObject developmentStatUI;

    //[SerializeField] bool showDegenerationRates = true;
    //[SerializeField] TextMeshProUGUI prosperityDegenerationRateField;
    //[SerializeField] TextMeshProUGUI happinessDegenerationRateField;
    //[SerializeField] TextMeshProUGUI environmentDegenerationRateField;
    // This allows to see NUMERICAL info about current state of the game stats
    //[SerializeField] bool numericalValueDisplay = true;
    //[SerializeField] TextMeshProUGUI prosperityValueField;
   // [SerializeField] TextMeshProUGUI happinessValueField;
    //[SerializeField] TextMeshProUGUI environmentValueField;

    // live update of degeneration Bonuses
    //[SerializeField] TextMeshProUGUI prosperityDegBonus;
    //[SerializeField] TextMeshProUGUI happinessDegBonus;
    //[SerializeField] TextMeshProUGUI environmentDegBonus;

    // live update of a tiles stats
    //[SerializeField] TextMeshProUGUI currentTileProsperity;
    //[SerializeField] TextMeshProUGUI currentTileHappiness;
    //[SerializeField] TextMeshProUGUI currentTileEnvironment;

    // to adjust camera-zoom ("CameraZoom"-script)
    public float cameraZoomSpeed = 10f;
    public float cameraMaxZoom = 15f;
    public float cameraMinZoom = 70f;
    //[SerializeField] TextMeshProUGUI cameraTransformMetrics1;
    //[SerializeField] TextMeshProUGUI cameraTransformMetrics2;

    // used to reset the UI-display at the start of the game
    private GameObject StatsDisplay;
    public bool hoverInfoEnabled = true;

    // these are used by the "STATS" script on terrain tiles to determine the amount of impact neighbors have
    //float greatInfluence = -.002f; // currently not used
    //public float positiveInfluence = -.001f;
    //public float neutralInfluence = 0f;
    //public float negativeInfluence = .001f;
    //float terribleInfluence = .002f; // currently not used
    #endregion
    void Start()
    {
        // new
        //baseCostOfLivingPerMinute = baseCostOfLivingPerMinute / 50f / 60f; // x / fps / sec = x per sec.
        
        // old
        StatsDisplay = GameObject.Find("Stats_UI");
        StatsDisplay.GetComponent<StatUIDisplay>().ResetStatBars();
        //if (developerMode == true)
        //{
        //    developmentStatUI.SetActive(true);
        //}
    }

    /*void FixedUpdate()
    {
        if (developerMode)
        {
            prosperityDegenerationRateField.text = "prosperity degeneration-rate " + SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate.ToString();
            happinessDegenerationRateField.text = "happiness degeneration-rate " + SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate.ToString();
            environmentDegenerationRateField.text = "environment degeneration-rate " + SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate.ToString();
            //}

            // if activated this displays STATS numerically in the UI (optional)
            //if (numericalValueDisplay == true)
            //{
            prosperityValueField.text = "prosperity " + SceneManager.GetComponent<NeedsManager>().prosperityValue.ToString();
            happinessValueField.text = "happiness " + SceneManager.GetComponent<NeedsManager>().happinessValue.ToString();
            environmentValueField.text = "environment " + SceneManager.GetComponent<NeedsManager>().environmentValue.ToString();
            //}

            // live update of camera settings
            cameraTransformMetrics1.text = "camera field Of View: " + Camera.main.fieldOfView.ToString();
            cameraTransformMetrics2.text = "camera orthographic size: " + Camera.main.orthographicSize.ToString();

            // live update of "degenerationBonus" from "Stats"-script
            prosperityDegBonus.text = "local prosperity bonus " + SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().prosperityBonus.ToString();
            happinessDegBonus.text = "local happiness bonus " + SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().happinessBonus.ToString();
            environmentDegBonus.text = "local environment bonus " + SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().environmentBonus.ToString();

            // numerical display of current tiles static and contineous stats
            //currentTileProsperity.text = "this tiles prosperity: static " + SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().prosperityStat.ToString() + "contineous: " + (SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().prosperityStat * degenerationFactor).ToString("0.00000");
            //currentTileHappiness.text = "this tiles happiness: static " + SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().happinessStat.ToString() + "contineous: " + (SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().happinessStat * degenerationFactor).ToString("0.00000");
            //currentTileEnvironment.text = "this tiles environment: static " + SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().environmentStat.ToString() + "contineous: " + (SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().environmentStat * degenerationFactor).ToString("0.00000");
        }
    }
    public void ShowDevStats(float a, float b, float c)
    {
        if (developerMode)
        {
            currentTileProsperity.text = "this tiles prosperity: static " + a + "contineous: " + (a * degenerationFactor).ToString("0.00000");
            currentTileHappiness.text = "this tiles happiness: static " + b + "contineous: " + (b * degenerationFactor).ToString("0.00000");
            currentTileEnvironment.text = "this tiles environment: static " + c + "contineous: " + (c * degenerationFactor).ToString("0.00000");
        }
    }
    public void ResetDevStats()
    {
        if (developerMode)
        {
            currentTileProsperity.text = "this tiles prosperity: static 0.00 contineous: 0.00";
            currentTileHappiness.text = "this tiles happiness: static 0.00 contineous: 0.00";
            currentTileEnvironment.text = "this tiles environment: static 0.00 contineous: 0.00";
        }
    }*/
}
