using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    public Animator animator;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
            
    }
    public void MoveScenes(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
    }
    public void MovingScenes(int sceneInt)
    {
        StartCoroutine(LoadScene(sceneInt));
    }
    IEnumerator LoadScene(int sceneInt)
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1.5f);
        MoveScenes(sceneInt);
    }
}
