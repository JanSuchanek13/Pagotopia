using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonAnimation : MonoBehaviour
{
    float x = .1f;
    Color newCol = Color.gray;
    bool isDown;
    [SerializeField] AudioSource errorSound;
    private void OnMouseDown()
    {
        if (!isDown)
        {
            MoveButton();
            //call reset Funktion
            GameObject _sceneManager = GameObject.Find("SceneManager");
            _sceneManager.GetComponent<Buttons>().GetNewSetOfTiles();
            Invoke("MoveButton", 10f);
        }
        else
        {
            errorSound.Play();
        }
    }

   
    void MoveButton()
    {
        x = x * -1;
        gameObject.transform.position = gameObject.transform.position + new Vector3(0, x, 0);
        isDown = !isDown;
        //gameObject.GetComponent<SphereCollider>().enabled = !gameObject.GetComponent<SphereCollider>().enabled;   //falls man verhindern will das der button garnicht gedrückt werden kann wenn er unten ist

        //Farbe ändern
        //Color switch = gameObject.GetComponent<MeshRenderer>().material;
        Color oldCol = gameObject.GetComponent<MeshRenderer>().material.color;
        gameObject.GetComponent<MeshRenderer>().material.color = newCol;
        newCol = oldCol;
    }
}
