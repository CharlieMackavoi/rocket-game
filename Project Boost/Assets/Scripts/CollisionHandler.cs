using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelDelayTimer = 2f;
    [SerializeField] AudioClip crashRocket;
    [SerializeField] AudioClip wonTheLevel;

    [SerializeField] ParticleSystem crashRocketParticles;
    [SerializeField] ParticleSystem wonTheLevelParticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start(){
        audioSource = GetComponent<AudioSource>();
    }

    void Update(){
        CheatKeys();
    }

    void CheatKeys(){
        if(Input.GetKey(KeyCode.L)){
            LoadNextLevel(); //Load next level on command
        }
    }
    void OnCollisionEnter(Collision other){
        if(!isTransitioning){
            switch(other.gameObject.tag){
                    case "Friendly":
                        Debug.Log("This is friendly");
                        break;
                    case "Finish":
                        StartNextLevel();
                        break;
                    default:
                        StartCrashSequence();
                        break;
            }
        
        }

    }


    void StartCrashSequence(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashRocket);
        crashRocketParticles.Play();
        GetComponent<Movement>().enabled = false;
        //Invoke delays calling a method (In this case so the player realizes they crashed.)
        Invoke("ReloadLevel", levelDelayTimer);
    }

    void StartNextLevel(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(wonTheLevel);
        wonTheLevelParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelDelayTimer);
    }

    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
            if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
                nextSceneIndex = 0;
            }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
