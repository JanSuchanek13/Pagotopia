using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Looser : MonoBehaviour
{
    [SerializeField] GameObject looserUI;
    [SerializeField] GameObject statUI;
    [SerializeField] GameObject gameUI;
    [SerializeField] AudioSource windSound;
    [SerializeField] AudioSource ambienteSound;

    /*public void YouLose()
    {
        windSound.enabled = false;
        ambienteSound.enabled = false;
        statUI.SetActive(false);
        gameUI.SetActive(false);
        looserUI.SetActive(true);
    }*/
}
