using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileGenerator : MonoBehaviour
{
    #region variables

    public GameObject[] arrayOfAllTilePrefabs;

    public Transform spawnPos1;
    public Transform spawnPos2;
    public Transform spawnPos3;
    int randomInt;

    public GameObject tile1;
    public GameObject tile2;
    public GameObject tile3;
    [SerializeField] float respawnDelay = 2f;

    private int minOrientation = 1;
    private int maxOrientation = 4;
    #endregion

    private void Start()
    {
        Invoke("GenerateTile1", respawnDelay);
        Invoke("GenerateTile2", respawnDelay);
        Invoke("GenerateTile3", respawnDelay);
    }

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
    }
    public void GenerateTile2()
    {
        int randomOrientation = Random.Range(minOrientation, maxOrientation);
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y - ((randomOrientation - 1) * 90), rot.z);

        randomInt = Random.Range(0, arrayOfAllTilePrefabs.Length);
        tile2 = Instantiate(arrayOfAllTilePrefabs[randomInt], spawnPos2.position, Quaternion.Euler(rot));
    }
    public void GenerateTile3()
    {
        int randomOrientation = Random.Range(minOrientation, maxOrientation);
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(rot.x, rot.y - ((randomOrientation - 1) * 90), rot.z);

        randomInt = Random.Range(0, arrayOfAllTilePrefabs.Length);
        tile3 = Instantiate(arrayOfAllTilePrefabs[randomInt], spawnPos3.position, Quaternion.Euler(rot));
    }

    public void DestroyRemainingTiles()
    {
        if(tile1.GetComponent<VillageStats>().wasPlaced == false && tile1.GetComponent<ProductionStats>().wasPlaced == false)
        {
            Destroy(tile1);
        }
        if (tile1.GetComponent<VillageStats>().wasPlaced == false && tile1.GetComponent<ProductionStats>().wasPlaced == false)
        {
            Destroy(tile2);
        }
        if (tile1.GetComponent<VillageStats>().wasPlaced == false && tile1.GetComponent<ProductionStats>().wasPlaced == false)
        {
            Destroy(tile3);
        }
        return;
    }
}
