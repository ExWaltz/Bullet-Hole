using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]public static AudioClip Fire, Death, NoPower;
    static AudioSource auSrc;
    void Start()
    {
        Fire = Resources.Load<AudioClip>("Shot");
        Death = Resources.Load<AudioClip>("Death1");
        NoPower = Resources.Load<AudioClip>("Fire");
        auSrc = GetComponent<AudioSource>();
        
    }
    public static void PlayerSound(string clip)
    {
        
        switch(clip)
        {
            case "Fire":
                auSrc.PlayOneShot(Fire);
                break;
            case "Death":
                auSrc.PlayOneShot(Death);
                break;
            case "NoPower":
                auSrc.PlayOneShot(Death);
                break;

        }
    }

}
