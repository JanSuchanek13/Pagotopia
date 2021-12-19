using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinningCondition : MonoBehaviour
{
    NeedsManager NeedsManager;
    GameObject endScreen;
    float enviromentLimit;
    float happinessLimit;
    float prosperityLimit;


    // Start is called before the first frame update
    void Start()
    {
        GameObject SceneManager = GameObject.Find("SceneManager");
        NeedsManager = SceneManager.GetComponent<NeedsManager>();
        enviromentLimit = NeedsManager.environmentValue * 0.8f;
        happinessLimit = NeedsManager.happinessValue * 0.8f;
        prosperityLimit = NeedsManager.prosperityValue * 0.8f;

        //Button für Screenshot suchen
        endScreen = GameObject.Find("endGameScreen_UI");
        //endScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (enviromentLimit < NeedsManager.environmentValue && happinessLimit < NeedsManager.happinessValue && prosperityLimit < NeedsManager.prosperityValue)
        {
            if (NeedsManager.environmentDegenerationRate < 0 && NeedsManager.happinessDegenerationRate < 0 && NeedsManager.prosperityDegenerationRate < 0)
            {
                //Debug.Log("winning condition erreicht");

                endScreen.SetActive(true);
            }
        }
    }
}
