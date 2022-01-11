using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotorRotator : MonoBehaviour
{
    #region variables
    [Header("Rotator Settings:")]
    [SerializeField] bool X_Axis = false;
    [SerializeField] bool Y_Axis = true;
    [SerializeField] bool Z_Axis = false;
    private int x = 0;
    private int y = 0;
    private int z = 0;
    [SerializeField] float minRotationSpeed = 0f;
    [SerializeField] float maxRotationSpeed = 200f;
    [SerializeField] float minDelayBeforeNewSpeed = 5f;
    [SerializeField] float maxDelayBeforeNewSpeed = 50f;
    [SerializeField] bool rotateHeadRandomly = true;
    [SerializeField] float turningSpeed = 4f;
    [SerializeField] float minDelayBeforeNewDirection = 20f;
    [SerializeField] float maxDelayBeforeNewDirection = 90f;
    [SerializeField] GameObject stemOfTurbine;
    float randomSpeed;
    float randomDegreesRotation;

    [Header("Rotator Sounds:")]
    [SerializeField] AudioSource rotation_Sound; // ex.: Whrrrr...*wind*
    #endregion

    private void Awake()
    {
        if (rotateHeadRandomly /*&& stemOfTurbine != null*/)
        {
            StartCoroutine("FaceNewDirection");
        }
        StartCoroutine("NewRotationSpeed");

        if (X_Axis)
        {
            x += 1;
        }
        if (Y_Axis)
        {
            y += 1;
        }
        if (Z_Axis)
        {
            z += 1;
        }
    }

    IEnumerator FaceNewDirection()
    {
        randomDegreesRotation = Random.Range(-360f, 360f);
        yield return new WaitForSeconds(Random.Range(minDelayBeforeNewDirection, maxDelayBeforeNewDirection));
        StartCoroutine("FaceNewDirection");
    }
    IEnumerator NewRotationSpeed()
    {
        randomSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
        yield return new WaitForSeconds(Random.Range(minDelayBeforeNewSpeed, maxDelayBeforeNewSpeed));
        StartCoroutine("NewRotationSpeed");
    }
    void FixedUpdate()
    {
        transform.Rotate(new Vector3(x, y, z), randomSpeed * Time.deltaTime, Space.Self);

        // play sound if wheel is moving:
        // (accellerate sound according to rotation-speed)
        if (rotation_Sound != null)
        {
            if(randomSpeed != 0f)
            {
                rotation_Sound.pitch = 0.015f * randomSpeed; // max 3 Pitch on max speed (200)
                rotation_Sound.volume = 0.005f * randomSpeed; // max 1 Volume on max speed (200)
                rotation_Sound.enabled = true;
            }else
            {
                rotation_Sound.enabled = false;
            }
        }

        // turn head (if enabled) towards target degree of rotation:
        if (rotateHeadRandomly && Mathf.Round(transform.eulerAngles.z) != randomDegreesRotation)
        {
            if(randomDegreesRotation <= 0f)
            {
                stemOfTurbine.transform.Rotate(new Vector3(0, 0, 1), -turningSpeed * Time.deltaTime, Space.Self);
            }else
            {
                stemOfTurbine.transform.Rotate(new Vector3(0, 0, 1), turningSpeed * Time.deltaTime, Space.Self);
            }
        }
    }
}
