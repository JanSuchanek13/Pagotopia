using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlaceObjectsOnGrid : MonoBehaviour
{
    public Transform City;
    public Transform gridCellPrefab;
    public Transform cube;
    public Transform onMousePrefab;
    public Vector3 smoothMousePosition;
    [SerializeField] private int height;
    [SerializeField] int width;

    private Vector3 mousePosition;
    private Tile[,] nodes;
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
            if (node.new_activeFix == false)
            {
                if (node.new_obj.GetComponent<ActivateCell>().Active == true)
                {
                    node.new_isPlaceable = true;
                    node.new_activeFix = true;
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
                
                    if (node.new_cellPosition == mousePosition && node.new_isPlaceable)
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
                            sceneManager.GetComponent<GameManager>().hoverInfoEnabled = true;
                            Debug.Log("hover Info ON (tile was placed)");

                            StatsDisplay.GetComponent<StatUIDisplay>().ResetStatBars();
                            StatsDisplay.GetComponent<StatUIDisplay>().ResetBonusStatBars();

                            node.new_isPlaceable = false;
                            isOnGrid = true;
                            curObject.transform.position = node.new_cellPosition + new Vector3(x: 0, y: 0.1f, z: 0);
                            onMousePrefab = null;

                            PlopSound.Play();
                                if(curObject.GetComponent<Stats>().isCow == true)
                                {
                                    moohSound.Play();
                                }
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
        nodes = new Tile[width, height];
        var name = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 worldPosition = new Vector3(x: i - width/2, y: 0, z: j - height/2);
                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                obj.name = "Cell" + name;
                nodes[i, j] = new Tile(new_isPlaceable: false, worldPosition, obj, new_activeFix: false);
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
        nodes[width / 2, height / 2].new_isPlaceable = false;
        nodes[width / 2, height / 2].new_activeFix = true;
        City.GetComponent<ClickTile>().setFix = true;


        //Set rocks on random positions
        /*
        for (int i = 0; i < 3; i++)
        {
            Instantiate(cube, new Vector3( Tile[Random.Range(0, allCells)]. ), Quaternion.identity);
            Debug.Log("im a rock");
        }
        */
    }
}



public class Tile
{
    public bool new_isPlaceable;
    public Vector3 new_cellPosition;
    public Transform new_obj;
    public bool new_activeFix;

    public Tile(bool new_isPlaceable, Vector3 new_cellPosition, Transform new_obj, bool new_activeFix)
    {
        this.new_isPlaceable = new_isPlaceable;
        this.new_cellPosition = new_cellPosition;
        this.new_obj = new_obj;
        this.new_activeFix = new_activeFix;
    }
}