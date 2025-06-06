using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    AudioClip audioClipCrash;
    [SerializeField]
    AudioClip audioSourceFinal;

    [SerializeField]
    ParticleSystem finishParticle;

    bool isControl = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondDebugKeys(); 
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isControl) 
            return;

        switch (collision.gameObject.tag)
        {
            case "Friednly": 
                Debug.Log("Friendly");
                break;
            
            case "Obstacle":
                Debug.Log("Obstacle");
                audioSource.clip = audioClipCrash;
                audioSource.Play();
                finishParticle.Play();
                break;

            case "Finish":
                Debug.Log("Finish");
                WinLevel();
                break;

            case "Ground":
                Debug.Log("Ground");
                GameOver();
                break;

            default:
                Debug.Log("Unknow tag");
                break;

        }
    }

    void GameOver()
    {
        audioSource.clip = audioClipCrash;
        audioSource.Play();
        GetComponent<Movement>().enabled = false;
        isControl = false;
        Invoke("ReloadScene", 2f);
    }

    void WinLevel()
    {
        audioSource.clip = audioSourceFinal;
        audioSource.Play();
        GetComponent<Movement>().enabled = false;
        isControl = false;
        finishParticle.Play();
        Invoke("LoadNextScene",2f);
    }

    void LoadNextScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = sceneIndex + 1;


        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
    
        SceneManager.LoadScene(nextScene);

    }

    void ReloadScene()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    void RespondDebugKeys()
    {
        if (Keyboard.current.lKey.isPressed)
        {
            LoadNextScene();
        }
    }
}
