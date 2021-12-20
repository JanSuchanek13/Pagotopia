using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButtonAnimation : MonoBehaviour
{
    float x = .1f;
    private void OnMouseDown()
    {
        MoveButton();
        //call reset Funktion
        Invoke("MoveButton", 1f);
    }

   
    void MoveButton()
    {
        x = x * -1;
        gameObject.transform.position = gameObject.transform.position + new Vector3(0, x, 0);
        
    }
}
