using UnityEngine;

public class LookAt : MonoBehaviour
{
    private GameObject _objectToLookAt;
    void Awake()
    {
        _objectToLookAt = GameObject.Find("Main Camera");
    }
    void Update()
    {
        //transform.LookAt(_objectToLookAt.transform);
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
