using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] float Y = 1f;
    [SerializeField] float speed = 1f;
    
    [SerializeField] bool isCow = false;
    
    Vector3 rotationVector;
    float randomFloat;
    /*private void Start()
    {
        rotationVector = new Vector3(0f, Y, 0f);
    }*/
    void FixedUpdate()
    {
        if (isCow == false)
        {
            transform.Rotate(new Vector3(0f, Y, 0f), speed * Time.deltaTime, Space.World);
        }
        else
        {
            randomFloat = Random.Range(0f, 60f);
            transform.Rotate(new Vector3(0f, Y, 0f), randomFloat * Time.deltaTime, Space.Self);

        }
    }
}

