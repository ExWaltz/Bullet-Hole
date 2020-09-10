using UnityEngine;

public class ResPoint : MonoBehaviour
{
    public static ResPoint instance;
    private void Awake()
    {
        instance = this;
    }
}
