using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    [SerializeField] Transform spawnPosition;
    [SerializeField] GameObject objectToSpawn;

    public int spawnCounter;

    /*[SerializeField] float hoverHeight = 2f;
    [SerializeField] float speed = 1f;
    private Vector3 startPosition;


    void Start()
    {
        startPosition = transform.position;
    }


    void Update()
    {
        Vector3 tp = transform.position;

        Vector3 targetPosition = startPosition + Vector3.up * hoverHeight * Mathf.PingPong(Time.time, 1f) * speed;
        transform.position = targetPosition;
    }


    public void StartHovering()
    {
        Debug.Log("i should start hovering");
        this.enabled = true;
    }
*/
    public void OnMouseDown()
    //übergibt angeklicktes Object an OnMouse Funktion aus PlaceObjectsOnGrid
    {
        Vector3 rot = transform.rotation.eulerAngles;

        // likely redundant
        //objectToSpawn = Instantiate(objectToSpawn, spawnPosition.position, Quaternion.Euler(rot));

        // limits the amount of spawnable balls to 1
        if (spawnCounter == 0)//objectToSpawn.CompareTag("Ball") && maxNumberOfBallsReached == false)
        {
            objectToSpawn = Instantiate(objectToSpawn, spawnPosition.position, Quaternion.Euler(rot));
            if (objectToSpawn.CompareTag("Ball"))
            {
                spawnCounter += 1;
            }
            //objectToSpawn = Instantiate(objectToSpawn, spawnPosition.position, Quaternion.Euler(rot));
        }
        /*else if (!objectToSpawn.CompareTag("Ball"))
        {
            objectToSpawn = Instantiate(objectToSpawn, spawnPosition.position, Quaternion.Euler(rot));
        }*/
        //Object = gameObject;
        //GameObject SceneManager = GameObject.Find("SceneManager");
        //SceneManager.GetComponent<PlaceObjectsOnGrid>().OnMouse(Object);
    }
}
