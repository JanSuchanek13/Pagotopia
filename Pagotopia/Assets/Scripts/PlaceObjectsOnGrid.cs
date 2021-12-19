using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsOnGrid : MonoBehaviour
{
    //new var
    public GameObject upgradeMapTile;

    //old var
    public Transform City;
    public Transform gridCellPrefab;
    public Transform cube;
    public Transform onMousePrefab;
    public Vector3 smoothMousePosition;
    [SerializeField] private int height;
    [SerializeField] int width;

    private Vector3 mousePosition;
    private Node[,] nodes;
    private Plane plane;

    //My Var.
    [SerializeField] AudioSource PlopSound;
    [SerializeField] AudioSource moohSound;

    //[SerializeField] ObjFollowMouse ObjFollowMouse;
    //[SerializeField] Stats Stats;
    //[SerializeField] Material placeable;
    //bool setFix = false;
    public bool isOnGrid;
    //bool activeFix = false;
    public GameObject curObject;
    int allCells;

    GameObject StatsDisplay;
    GameObject sceneManager;

    void Start()
    {
        StatsDisplay = GameObject.Find("Stats_UI");
        sceneManager = GameObject.Find("SceneManager");

        CreateGrid();
        plane = new Plane(inNormal: Vector3.up, inPoint: transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePositionOnGrid();
        if (!isOnGrid)
        {
            if (curObject != null)
            {
                curObject.transform.position = smoothMousePosition + new Vector3(x: 0, y: 0.5f, z: 0);
                curObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        foreach (var node in nodes)
        {
            if (node.activeFix == false)
            {
                if (node.obj.GetComponent<ActivateCell>().Active == true)
                {
                    node.isPlaceable = true;
                    node.activeFix = true;
                }
            }
            //winning Condition
            /*
            else if (node.activeFix == true)
            {
                if (node.isPlaceable == false)
                {
                    allCells--;
                    //Debug.Log(allCells);
                    if (allCells == 0)
                    {
                        Debug.Log("Gewonnen OXOXOXOXOXOXOXOXOXOOXOXOXOXOXO");
                    }
                }
            }
            */
        }
        //allCells = width * height;    //allCells wieder voll machen für nächsten Loop
    }

    RaycastHit hit; // is this used?

    void GetMousePositionOnGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (plane.Raycast(ray,out var enter) )        
        {
            //Debug.Log("AAAA");
            mousePosition = ray.GetPoint(enter);
            smoothMousePosition = mousePosition;
            mousePosition.y = 0;
            mousePosition = Vector3Int.RoundToInt(mousePosition);

            

            /*GameObject[] podest = GameObject.FindGameObjectsWithTag("podest");
            foreach (var i in podest)
            {
                if (i.transform.position == mousePosition)
                {
                    Debug.Log("EEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                }
            }*/


            foreach (var node in nodes)
            {

                if (node.cellPosition == mousePosition && node.isPlaceable)
                {
                    //Debug.Log("BBBB");
                    if (Input.GetMouseButtonUp(0))
                    {
                        //Debug.Log("CCCC");
                        if (onMousePrefab != null) // tile gets placed
                        {
                            curObject.GetComponent<BoxCollider>().enabled = true;
                            curObject.GetComponent<Stats>().AddNeighborBonus();
                            curObject.GetComponent<Stats>().UpdateStats();
                            curObject.GetComponent<ClickTile>().setFix = true;
                            curObject.GetComponent<ProductionStats>().Build(); // new 
                            sceneManager.GetComponent<GameManager>().hoverInfoEnabled = true;
                            Debug.Log("hover Info ON (tile was placed)");

                            StatsDisplay.GetComponent<StatUIDisplay>().ResetStatBars();
                            StatsDisplay.GetComponent<StatUIDisplay>().ResetBonusStatBars();

                            node.isPlaceable = false;
                            isOnGrid = true;
                            curObject.transform.position = node.cellPosition + new Vector3(x: 0, y: 0.1f, z: 0);
                            onMousePrefab = null;

                            PlopSound.Play();
                            if (curObject.GetComponent<Stats>().isCow == true)
                            {
                                moohSound.Play();
                            }
                        }
                    }

                }
                else if (node.cellPosition == mousePosition && !node.isPlaceable)
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        if (onMousePrefab.CompareTag("Upgrade")) // Maptile gets upgrade
                        {
                            upgradeMapTile.GetComponent<ProductionStats>().Upgrade();

                            Destroy(upgradeMapTile);
                            onMousePrefab = null;
                        }
                    }
                }
            }
        }
    }

    public void OnMouse(GameObject clickObject)
    {
        if (onMousePrefab == null && clickObject.GetComponent<ClickTile>().setFix == false)
        {
            curObject = clickObject;
            isOnGrid = false;
            onMousePrefab = gameObject.transform;
            //onMousePrefab = Instantiate(cube, mousePosition, Quaternion.identity);

            // Felix Code:
            /*
                GameObject Over = curObject; // this is curObject
                tag = curObject.tag;
                GameObject StatsDisplay = GameObject.Find("Stats_UI");

                float a = Over.GetComponent<Stats>().prosperityStat;
                float b = Over.GetComponent<Stats>().environmentStat;
                float c = Over.GetComponent<Stats>().happinessStat;

                StatsDisplay.GetComponentInParent<StatUIDisplay>().CastStatsToUI(Over, tag, a, b, c);

                // added by Felix to see numerical stats in dev mode while hovering
                /*if (SceneManager.GetComponent<GameManager>().developerMode)
                {
                    SceneManager.GetComponent<GameManager>().ShowDevStats(a, b, c);
                }*/
           // }
        }
    }



    private void CreateGrid()
    {
        

        //build Grid
        nodes = new Node[width, height];
        var name = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 worldPosition = new Vector3(x: i - width/2, y: 0, z: j - height/2);
                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                obj.name = "Cell" + name;
                nodes[i, j] = new Node(isPlaceable: true, worldPosition, obj, activeFix: false);
                //Debug.Log(nodes[i, j].cellPosition);
                name++;

                //Anzahl an Zellen wird belegt, damit es für die WinningCondition überprüft werden kann
                allCells = width * height;
            }
        }

        //setze city in die mitte
        int half = width * height / 2;
        GameObject middle = GameObject.Find("Cell" + half);
        //Debug.Log("Centerpiece number: " + middle);
        Instantiate(City, new Vector3(0, 0.1f,0) /*new Vector3(x: width/2, y: 0, z: height/2)*/, Quaternion.identity);
        nodes[width / 2, height / 2].isPlaceable = false;
        nodes[width / 2, height / 2].activeFix = true;
        City.GetComponent<ClickTile>().setFix = true;


        //set rocks on random positions
        for (int i = 0; i < 5; i++)
        {
            int r_width = Random.Range(0, width);
            int r_height = Random.Range(0, height);
            //Node random_cell = nodes[Random.Range(0, width), Random.Range(0, height)];
            Debug.Log(nodes[r_width, r_height].isPlaceable);
            if (nodes[r_width, r_height].isPlaceable)
            {
                Instantiate(cube, (nodes[r_width, r_height].cellPosition), Quaternion.identity);
                nodes[r_width, r_height].isPlaceable = false;
                nodes[r_width, r_height].activeFix = true;
                Debug.Log("im a rock");
            }
            else //if cell is not placeable, set stone counter to before value
            {
                i--;
            }
        }



    }
}



public class Node
{
    public bool isPlaceable;
    public Vector3 cellPosition;
    public Transform obj;
    public bool activeFix;

    public Node(bool isPlaceable, Vector3 cellPosition, Transform obj, bool activeFix)
    {
        this.isPlaceable = isPlaceable;
        this.cellPosition = cellPosition;
        this.obj = obj;
        this.activeFix = activeFix;
    }
}