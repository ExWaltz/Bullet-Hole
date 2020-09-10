using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmController : MonoBehaviour
{
    public static BgmController instance;
    public static AudioSource bgmScr;
    void Awake()
    {
        if(instance == null)
        {

            instance = this;
            DontDestroyOnLoad(this);
            bgmScr = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
