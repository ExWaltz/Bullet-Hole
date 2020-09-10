using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static GameMaster instance;
    public GameObject playerPrefap, ExplosionPrefap;

    public TMPro.TextMeshProUGUI Score, Highscore;
    PlayerMovement playerMove;
    public float resDelay = 2f;
    public bool isDeath = false;

    private void Awake()
    {
        instance = this;
        if(Highscore != null)
        {
            Highscore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        }
    }

    private void Start()
    {
        playerMove = PlayerMovement.instance.player.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        
        if(isDeath)
        {
            Invoke("SpawnPlayer", resDelay);
            OnGameObject();
            isDeath = false;
            if(Score == null) return;
            Score.text = "0";
            
        }
        EscOptions();
        SaveScore();
    }
    void SaveScore()
    {
        if(Highscore == null) return;
        if(PlayerPrefs.GetInt("Highscore") <= int.Parse(Score.text))
        {
            PlayerPrefs.SetInt("Highscore", int.Parse(Score.text));
            Highscore.text = PlayerPrefs.GetInt("Highscore", 0).ToString();
        }
    }
    public void EscOptions()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CanvasManager.instance.MainOpt();
        }
    }
    void SpawnPlayer()
    {
        Score.text = "0";
        PlayerMovement.instance.player.transform.position = ResPoint.instance.transform.position;
        PlayerMovement.instance.soulMR.enabled = false;
        PlayerMovement.instance.player.SetActive(true);
        SpawnBullet.instance.OffBullets();
        FireBullets.instance.isRes = true;


    }
    public void OnEnd()
    {
        ExplosionPrefap.SetActive(false);
    }
    public void OnGameObject()
    {
        ExplosionPrefap.transform.position = PlayerMovement.instance.player.transform.position;
        ExplosionPrefap.SetActive(true);
        Invoke("OnEnd", 0.5f);
    }
    public static void ErrorList(string str)
    {
        switch(str)
        {
            case "Null_Gm":
                Debug.Log("Missing GameObject");
                return;
            case "Null_Cp":
                Debug.Log("Missing Component");
                return;
        }
    }
}
