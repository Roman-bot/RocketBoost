using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    InputAction actionJump;

    [SerializeField]
    InputAction actionDirection;

    Rigidbody playerBody;

    [SerializeField]
    float speed;

    AudioSource audioSourceRocket;

    [SerializeField]
    AudioClip audioClipRocket;

    [SerializeField]
    ParticleSystem mainParticle;

    [SerializeField]
    ParticleSystem rightSideParticle;

    [SerializeField]
    ParticleSystem leftSideParticle;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        actionJump.Enable();
        actionDirection.Enable();
        playerBody = GetComponent<Rigidbody>();
        audioSourceRocket = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerFly();
        PlayerRotate();
    }

    void PlayerFly()
    {
        if (actionJump != null && actionJump.IsPressed())
        {
            //Debug.Log("Key is pressed");
            //playerBody.linearVelocity = new Vector3(0,1,0);
            playerBody.AddRelativeForce(Vector3.up * speed * Time.fixedDeltaTime);
            audioSourceRocket.clip = audioClipRocket;
            audioSourceRocket.Play();
            mainParticle.Play();
        }
        else
        {
            audioSourceRocket.Stop();
            mainParticle.Stop();
        }
    }

    void PlayerRotate()
    {
        float inputRotate = actionDirection.ReadValue<float>();

        if (actionDirection != null && actionDirection.IsPressed())
        {
            //Debug.Log("Key is pressed " + inputRotate);
            float directionRotate = inputRotate * Time.fixedDeltaTime * 100;
            gameObject.transform.Rotate(Vector3.back, directionRotate);
            if (inputRotate > 0)
            {
                rightSideParticle.Play();
                leftSideParticle.Stop();
            }
            else if(inputRotate < 0)
            {
                leftSideParticle.Play();
                rightSideParticle.Stop();
            }
        }
    }

}
