using UnityEngine;
using TMPro;
public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;
    public GameObject transistion;
    public Animator anim;

    void Awake()
    {
        instance = this;

        
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneTransition.instance.MovingScenes(0);
    }
    public void Game()
    {
        Time.timeScale = 1f;
        SceneTransition.instance.MovingScenes(1);
    }
    public void Credits()
    {
        Time.timeScale = 1f;
        SceneTransition.instance.MovingScenes(2);
        
    }
    public void MainOpt()
    {
        int animState = anim.GetInteger("OptState");
        if(animState == 0 && !anim.IsInTransition(0))
        {
            Time.timeScale = 0f;
            anim.SetInteger("OptState", 1);
            
        }else if(animState == 1 && !anim.IsInTransition(0))
        {
            Time.timeScale = 1f;
            SettingSetter.SaveSetKey();
            anim.SetInteger("OptState", 0);
            SettingSetter.ShowText();

        }
        
    }

}
