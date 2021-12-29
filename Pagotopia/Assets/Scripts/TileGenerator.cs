using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] arrayOfAllTilePrefabs;

    public Transform spawnPos1;
    public Transform spawnPos2;
    public Transform spawnPos3;
    int randomInt;

    public GameObject tile1;
    public GameObject tile2;
    public GameObject tile3;
    [SerializeField] float respawnDelay = 2f;

    int minOrientation = 1;
    int maxOrientation = 4;

    private void Start()
    {
        Invoke("GenerateTile1", respawnDelay);
        Invoke("GenerateTile2", respawnDelay);
        Invoke("GenerateTile3", respawnDelay);
    }

   /* private void Update() // for testing //delete me?! (spawns new on rightclick)
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            tile1.GetComponent<Stats>().UpdateStats();
            Debug.Log("Mouse was pressed");
        }
    }*/

    public void NextSet()
    {
        Invoke("GenerateTile1", respawnDelay);
        Invoke("GenerateTile2", respawnDelay);
        Invoke("GenerateTile3", respawnDelay);
    }
    

    public void GenerateTile1()
    {
        int randomOrientation = Random.Range(minOrientation, maxOrientation);
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y - ((randomOrientation - 1) * 90), rot.z);

        randomInt = Random.Range(0, arrayOfAllTilePrefabs.Length);
        tile1 = Instantiate(arrayOfAllTilePrefabs[randomInt], spawnPos1.position, Quaternion.Euler(rot));
        //tile1.GetComponent<Stats>().RandomizeStats(tile1, spawnPos1.position); // added ", spawnPos1.position" --> also in others

        // Debug.Log("a tile was created");
    }
    public void GenerateTile2()
    {
        int randomOrientation = Random.Range(minOrientation, maxOrientation);
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y - ((randomOrientation - 1) * 90), rot.z);

        randomInt = Random.Range(0, arrayOfAllTilePrefabs.Length);
        tile2 = Instantiate(arrayOfAllTilePrefabs[randomInt], spawnPos2.position, Quaternion.Euler(rot));
        //tile2.GetComponent<Stats>().RandomizeStats(tile2, spawnPos2.position);

        // Debug.Log("a tile was created");
    }
    public void GenerateTile3()
    {
        int randomOrientation = Random.Range(minOrientation, maxOrientation);
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y - ((randomOrientation - 1) * 90), rot.z);

        randomInt = Random.Range(0, arrayOfAllTilePrefabs.Length);
        tile3 = Instantiate(arrayOfAllTilePrefabs[randomInt], spawnPos3.position, Quaternion.Euler(rot));
        //tile3.GetComponent<Stats>().RandomizeStats(tile3, spawnPos3.position);

        // Debug.Log("a tile was created");
    }

    public void DestroyRemainingTiles()
    {
        if(tile1.GetComponent<Stats>().wasPlaced == false)
        {
            Destroy(tile1);
        }
        if (tile2.GetComponent<Stats>().wasPlaced == false)
        {
            Destroy(tile2);
        }
        if (tile3.GetComponent<Stats>().wasPlaced == false)
        {
            Destroy(tile3);
        }
        return;
    }
}
