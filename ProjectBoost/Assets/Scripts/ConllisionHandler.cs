using UnityEngine;
using UnityEngine.SceneManagement;

public class ConllisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success; //audio
    [SerializeField] AudioClip crash; //audio
    [SerializeField] ParticleSystem successParticles; // effect
    [SerializeField] ParticleSystem crashParticles; // effect

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        { //dau vao nguoi dung
            LoadNextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; //toggle collision
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled)
        {
            return;
        }
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("This thing is friendly");
                    break;
                case "Finish":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;

            }
        }
    }

    void StartSuccessSequence()
    {   //
        isTransitioning = true;
        audioSource.Stop(); // dung nhac phia truoc
        audioSource.PlayOneShot(success); // phat doan nhac ten success
        successParticles.Play(); // chay effect
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay); //delay time
    }

    void StartCrashSequence()
    {
        // to do SFX up on crash
        //audio
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void LoadNextLevel()
    {

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
