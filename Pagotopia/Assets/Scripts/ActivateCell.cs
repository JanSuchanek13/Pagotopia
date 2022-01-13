using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zellen werden sichbar sobald ein MapTiles gesetzt wird

public class ActivateCell : MonoBehaviour
{
    //new var
    [SerializeField] MeshRenderer natureShader;
    [SerializeField] MeshRenderer factoryShader;
    [SerializeField] MeshRenderer socialShader;
    [SerializeField] MeshRenderer neighborShader;

    [SerializeField] Material _energyMaterial;
    [SerializeField] Material _happinessMaterial;
    [SerializeField] Material _environmentMaterial;
    [SerializeField] Material _neighborMaterial;
    [SerializeField] Material _PLACEDenergyMaterial;
    [SerializeField] Material _PLACEDhappinessMaterial;
    [SerializeField] Material _PLACEDenvironmentMaterial;
    [SerializeField] Material _PLACEDneighborMaterial;
    public bool hasEnergy;
    public bool hasHappiness;
    public bool hasEnvironment;
    public bool hasNeighbor;
    public bool isOccupied = false;

    //old var
    [SerializeField] Material placeable;
    public bool Active = false;

    public List<GameObject> neighbors = new List<GameObject>();
    int factory;
    int social;
    int nature;
    int sustainable;
    GameObject SceneManager;

    //speicher Original Farbe
    Color OriginalColor;
    public Material Material1;
    public Material Material2;
    Renderer[] children;

    GameObject StatsDisplay;

    private void Awake()
    {
        SceneManager = GameObject.Find("SceneManager");
        StatsDisplay = GameObject.Find("Stats_UI");
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("hey");
        Active = true;
        //gameObject.GetComponent<Renderer>().material = placeable;
        gameObject.GetComponent<Renderer>().enabled = true;

        if (!isOccupied) // this is to check if something was built here already:
        {
            //liste aller benachbarten Zellen
            string str = other.tag;
            switch (str)
            {
                case "energy":
                    if (other.GetComponentInParent<ProductionStats>().wasPlaced)
                    {
                        factoryShader.material = _PLACEDenergyMaterial;
                    }
                    factoryShader.enabled = true;
                    hasEnergy = true;
                    break;

                case "happiness":
                    if (other.GetComponentInParent<ProductionStats>().wasPlaced)
                    {
                        socialShader.material = _PLACEDhappinessMaterial;
                    }
                    socialShader.enabled = true;
                    hasHappiness = true;
                    break;

                case "environment":
                    if (other.GetComponentInParent<ProductionStats>().wasPlaced)
                    {
                        natureShader.material = _PLACEDenvironmentMaterial;
                    }
                    natureShader.enabled = true;
                    hasEnvironment = true;
                    break;

                case "village":
                    if (other.GetComponentInParent<VillageStats>().wasPlaced)
                    {
                        neighborShader.material = _PLACEDneighborMaterial;
                    }
                    neighborShader.enabled = true;
                    hasNeighbor = true;
                    break;

                case "city":
                    if (other.GetComponentInParent<VillageStats>().wasPlaced)
                    {
                        neighborShader.material = _PLACEDneighborMaterial;
                    }
                    neighborShader.enabled = true;
                    hasNeighbor = true;
                    break;

                default:
                    //Debug.Log("Fehler");
                    break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Debug.Log("hey");
        Active = true;
        //gameObject.GetComponent<Renderer>().material = placeable;
        gameObject.GetComponent<Renderer>().enabled = true;

        //liste aller benachbarten Zellen
        string str = other.tag;
        switch (str)
        {
            case "happiness":
                socialShader.enabled = false;
                hasHappiness = false;

                break;
            case "environment":
                natureShader.enabled = false;
                hasEnvironment = false;

                break;
            case "energy":
                factoryShader.enabled = false;
                hasEnergy = false;

                break;
            case "village":
                neighborShader.enabled = false;
                hasNeighbor = false;

                break;
            case "city":
                neighborShader.enabled = false;
                hasNeighbor = false;


                break;
            default:
                //Debug.Log("Fehler");
                break;
        }
    }

    private void OnMouseOver()
    {
        if (SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab != null)
        {

            //blau beim hovern
            Color colorStart = new Color(.5f, .6f, 0.23f);
            Color colorEnd = new Color(.3f, .4f, 0.21f);
            float duration = 1.0f;

            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            gameObject.GetComponent<Renderer>().material.color = Color.Lerp(colorStart, colorEnd, lerp);

            //gameObject.GetComponent<Renderer>().material.color = new Color(1f, 0.85f, 0.35f);//Color.blue;


            //zählt die jeweiligen Tags der Nachbarn
            //Debug.Log(neighbors.Count);
            foreach (var tile in neighbors)
            {
                children = tile.GetComponentsInChildren<Renderer>();
                //tile.GetComponentsInChildren<>(true);
                string str = tile.tag;
                switch (str)
                {
                    case "factory":
                        factory ++;
                        children[1].GetComponent<Renderer>().material.color = Color.gray;//new Color(1f, 0.85f, 0.35f);//Boden von Nachbarn wird gefärbt

                        break;
                    case "social":
                        social++;
                        children[1].GetComponent<Renderer>().material.color = Color.magenta;

                        break;
                    case "nature":
                        nature++;
                        children[1].GetComponent<Renderer>().material.color = Color.green;

                        break;
                    case "sustainable":
                        sustainable++;
                        children[1].GetComponent<Renderer>().material.color = Color.blue;

                        break;
                    default:
                        //Debug.Log("Fehler");
                        break;
                }
            }
            //SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().NeighborEffect(factory, social, nature, sustainable); // this casts the neighbor effect
            //Debug.Log("factory" + factory);
            //Debug.Log("social" + social);
            //Debug.Log("nature" + nature);
            //Debug.Log("sustainable" + sustainable);
            factory = 0;
            social = 0;
            nature = 0;
            sustainable = 0;
        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material = Material1;

        //StatsDisplay.GetComponent<StatUIDisplay>().ResetBonusStatBars(); // not working, dunno why

        foreach (var tile in neighbors)
        {
            children = tile.GetComponentsInChildren<Renderer>();
            //tile.GetComponentsInChildren<>(true);
            string str = tile.tag;
            switch (str)
            {
                case "factory":
                    
                    children[1].GetComponent<Renderer>().material = Material2;

                    break;
                case "social":
                    
                    children[1].GetComponent<Renderer>().material = Material2;

                    break;
                case "nature":
                    
                    children[1].GetComponent<Renderer>().material = Material2;

                    break;
                case "sustainable":
                    
                    children[1].GetComponent<Renderer>().material = Material2;

                    break;
                default:
                    //Debug.Log("Fehler");
                    break;
            }
        }
    }
}
