using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPool : MonoBehaviour
{
    public static PowerPool instance;
    public GameObject powerPrefap;
    public float powerAmount;
    
    public List<GameObject> powerPool;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        powerPool = new List<GameObject>();
        for (int x = 0; x < powerAmount; x++)
        {
            GameObject pobj = Instantiate(powerPrefap);
            pobj.transform.SetParent(transform);
            pobj.transform.localScale = transform.localScale;
            pobj.SetActive(false);
            powerPool.Add(pobj);
        }
    }
    public GameObject GetPowerPool()
    {
        for (int i = 0; i < powerPool.Count; i++)
        {
            if (!powerPool[i].activeInHierarchy)
            {
                powerPool[i].transform.position = transform.position;
                return powerPool[i];
            }
        }
        return null;
    }
    public void ActivatePower()
    {
        for (int i = 0; i < powerPool.Count; i++)
        {
            if (powerPool[i].activeInHierarchy)
            {
                SpawnBullet.instance.OffBullets();
                powerPool[i].SetActive(false);
                break;
            }
            else
            {
                SoundManager.PlayerSound("NoPower");
            }
        }
    }
}
