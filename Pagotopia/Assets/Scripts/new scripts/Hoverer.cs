using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverer : MonoBehaviour
{
    [SerializeField] float hoverHeight = .5f;
    [SerializeField] float speed = .5f;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        Vector3 targetPosition = startPosition + Vector3.up * hoverHeight * Mathf.PingPong(Time.time, 1f) * speed;
        transform.position = targetPosition;
    }
}
