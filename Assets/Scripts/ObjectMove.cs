using UnityEngine;

public class ObjectMove : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Vector3 movementPos;
    
    [SerializeField]
    Vector3 movementVector;

    float movementFactor; // variable to interpolate the movement
    
    [SerializeField]
    float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        endPos = startPos + movementVector;
    }

    // Update is called once per frame
    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPos, endPos, movementFactor);
    }
}
