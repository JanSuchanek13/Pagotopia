using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTile : MonoBehaviour
{
    private GameObject Object;
    public bool setFix = false;
    private GameObject StatsDisplay;
    public GameObject Over;
    public Vector3 myPos;
    private GameObject SceneManager;

    private void Awake()
    {
        SceneManager = GameObject.Find("SceneManager");
        myPos = gameObject.transform.position;
    }

    public void OnMouseDown()
    {
        if (gameObject.CompareTag("Upgrade"))
        {
            gameObject.GetComponent<UpgradeScripts>().PickUpgradeUp();
        }

        // this prevents the Stats-UI from bugging out when clicking on an GO with setFix == true:
        if(gameObject.GetComponent<ClickTile>() != null && gameObject.GetComponent<ClickTile>().setFix == false)
        {
            Object = gameObject;
            SceneManager.GetComponent<NewGameManager>().hoverInfoEnabled = false;
            // pick up tile (setFix == false):
            SceneManager.GetComponent<PlaceObjectsOnGrid>().OnMouse(Object);
        }
    }

    // cast tile-stats to Stats-UI:
    private void OnMouseOver()
    {
        SceneManager.GetComponent<PlaceObjectsOnGrid>().upgradeMapTile = gameObject;//für upgrade funktion, damit man weiß welches GO geupdatet werden soll
            if (SceneManager.GetComponent<NewGameManager>().hoverInfoEnabled == true)
            {
                if (gameObject.CompareTag("happiness") || gameObject.CompareTag("environment") || gameObject.CompareTag("energy") || gameObject.CompareTag("village") || gameObject.CompareTag("city") || gameObject.CompareTag("Upgrade") || gameObject.CompareTag("cell") || gameObject.CompareTag("mountain"))
                {
                    Over = gameObject;
                    StatsDisplay = GameObject.Find("Stats_UI");
                    if (StatsDisplay == isActiveAndEnabled)
                    {
                        StatsDisplay.GetComponentInParent<StatUIDisplay>().CastStatsToUI(Over);
                    }
                }
            }
    }

    // reset Stats-UI:
    private void OnMouseExit()
    {
        SceneManager.GetComponent<PlaceObjectsOnGrid>().upgradeMapTile = null;//für upgrade funktion, damit man weiß welches GO geupdatet werden soll
        if (StatsDisplay == isActiveAndEnabled)
        {
            if (gameObject.CompareTag("happiness") || gameObject.CompareTag("environment") || gameObject.CompareTag("energy") || gameObject.CompareTag("village") || gameObject.CompareTag("city") || gameObject.CompareTag("Upgrade") || gameObject.CompareTag("cell") || gameObject.CompareTag("mountain"))
            {
                if (SceneManager.GetComponent<NewGameManager>().hoverInfoEnabled == true)
                {
                    StatsDisplay.GetComponent<StatUIDisplay>().ResetStatBars();
                }
            }
        }
    }

    // puts checkmarks on village & city-tagged GO's to display current influences on said GO:
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("village") || collision.CompareTag("city"))
        {
            if (setFix == true && collision.gameObject.GetComponent<VillageStats>() != null)
            {
                string str = gameObject.tag;
                switch (str)
                {
                    case "energy":
                        collision.gameObject.GetComponent<VillageStats>().influencedByEnergy = true;

                        break;
                    case "happiness":
                        collision.gameObject.GetComponent<VillageStats>().influencedByHappiness = true;

                        break;
                    case "environment":
                        collision.gameObject.GetComponent<VillageStats>().influencedByNature = true;

                        break;

                    case "village":
                        collision.gameObject.GetComponent<VillageStats>().influencedByNeighbors = true;

                        break;

                    case "city":
                        collision.gameObject.GetComponent<VillageStats>().influencedByNeighbors = true;

                        break;
                    default:
                        break;
                }
            }
        }
    }
}
