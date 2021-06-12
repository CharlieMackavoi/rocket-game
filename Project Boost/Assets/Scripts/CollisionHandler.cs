using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float levelDelayTimer = 1f;
    void OnCollisionEnter(Collision other){
        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("This is friendly");
                break;
            case "Finish":
                StartNextLevel();
                break;
            default:
                //Invoke delays calling a method (In this case so the player realizes they crashed.)
                StartCrashSequence();
                break;
        }

    }

    void StartCrashSequence(){
        //todo add SFX on crashing
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelDelayTimer);
    }

    void StartNextLevel(){
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
