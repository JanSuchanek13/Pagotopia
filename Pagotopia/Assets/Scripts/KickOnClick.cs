using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class KickOnClick : MonoBehaviour
{
    AudioSource kickingSound;
    [SerializeField] AudioSource mooh; //example cow could say "moooh" // currently filled & accessed by "Buttons"-script
    Rigidbody myRigid;
    GameObject mainCam;
    [SerializeField] float kickSpeed = 100f;
    bool isCow = false;

    public NavMeshAgent navMeshAgent;

    // for respawning
    Vector3 spawn_Position;
    float blinkIntervall;

    // this part is to make a cow wobble after being kicked:
    public bool wobbleEnabled = false;
    [SerializeField] Vector3 minScale = new Vector3(0.75f, 0.75f, 0.75f);
    [SerializeField] Vector3 maxScale = new Vector3(1.25f, 1.25f, 1.25f);
    [SerializeField] float speed = 2f;

    private Vector3 _difScale;
    private Vector3 _startScale;

    GameObject ackerGameObject;

    private void Start()
    {
        myRigid = gameObject.GetComponent<Rigidbody>();
        mainCam = GameObject.Find("Main Camera");
        ackerGameObject = GameObject.Find("Gras (lauffläche)");
        GameObject SceneManager = GameObject.Find("SceneManager");

        if (gameObject.tag == "cow")
        {
            isCow = true;
        }
        if (SceneManager.GetComponent<Buttons>().kickingSound != null)
        {
            kickingSound = SceneManager.GetComponent<Buttons>().kickingSound;
        }
        if (SceneManager.GetComponent<Buttons>().mooh != null)
        {
            mooh = SceneManager.GetComponent<Buttons>().mooh;
        }
        
        // this part is to make a cow wobble after being kicked:
        _startScale = transform.localScale;
        _difScale = maxScale - minScale;
        //Debug.Log(_difScale.ToString());

        // this is to save a respawn position if you fell out of the map:
        spawn_Position = GameObject.Find("Spawn_Position").GetComponent<Transform>().position;
        blinkIntervall = SceneManager.GetComponent<Buttons>().blinkIntervall;
    }
    private void OnMouseDown()
    {
        float randomDuration = Random.Range(2f, 5f);
        if (isCow && mooh != null)
        {
            Invoke("Mooh", .3f);
            //wobbleEnabled = true;
            
        }
        if (kickingSound != null)
        {
            kickingSound.Play();
        }
        if (GetComponent<NavMeshAgent>() != null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            navMeshAgent.enabled = false;
            Invoke("GetBackUp", randomDuration);
        }


        kickSpeed = Random.Range(1f, 10f);
        myRigid.AddForce(mainCam.transform.forward * kickSpeed, ForceMode.VelocityChange);
        
        // lifting the target up seems to not work with the MeshNav = redundant
        //myRigid.AddForce(new Vector3(0f, 100f, 0f), ForceMode.VelocityChange);
    }
    void Mooh()
    {
        mooh.pitch = Random.Range(1f, 2f);
        mooh.Play();
    }

    void GetBackUp()
    {
        navMeshAgent.enabled = true; // should turn MeshNav back on
        wobbleEnabled = false; // ends wobble when getting back up
        if (isCow && mooh != null)
        {
            Invoke("Mooh", .1f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // checks if you have fallen off the map
        if (other.CompareTag("DropZone"))
        {
            // destroy balls that fall out of bounds
            if (CompareTag("Ball") && ackerGameObject.GetComponent<SpawnOnClick>().spawnCounter != 0)
            {
                ackerGameObject.GetComponent<SpawnOnClick>().spawnCounter = 0;
                //gameObject.SetActive(false);
                //Destroy(gameObject);

                // in case we only want to allow one ball:
            }
            wobbleEnabled = false; // ends wobble when falling out of map
            Invoke("ResetPosition", 3f);
        }
    }
    // in case you knock a cow out of the map
    void ResetPosition()
    {
        transform.position = spawn_Position;
        
        // only works on ball right now...
        /*BlinkOff();
        Invoke("BlinkOff", (blinkIntervall * 2f));
        Invoke("BlinkOff", (blinkIntervall * 4f));*/

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        Invoke("GetBackUp", 2f);
    }
    void BlinkOff()
    {
        GetComponent<MeshRenderer>().enabled = false;
        Invoke("BlinkOn", blinkIntervall);
    }
    void BlinkOn()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    // the wobble happens here
    void FixedUpdate()
    {
        if (wobbleEnabled)
        {
            float rx = minScale.x + Mathf.PingPong(Time.time * speed, maxScale.x);
            float ry = minScale.y + Mathf.PingPong(Time.time * speed, maxScale.y);
            float rz = minScale.z + Mathf.PingPong(Time.time * speed, maxScale.z);

            rx = rx * _startScale.x;
            ry = ry * _startScale.y;
            rz = rz * _startScale.z;

            Vector3 newScale = new Vector3(rx, ry, rz);
            transform.localScale = newScale;
        }
    }
}
