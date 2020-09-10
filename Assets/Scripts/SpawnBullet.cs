using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    public static SpawnBullet instance;
    public GameObject bulletPrefap;
    public Rigidbody2D rb;
    public int bulletAmount;
    public bool isDyanamic = false;

    public List<GameObject> bulletPool;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        bulletPool = new List<GameObject>();

        rb = bulletPrefap.GetComponent<Rigidbody2D>();
        

        for (int i = 0; i < bulletAmount; i++)
        {
            
            GameObject obj = Instantiate(bulletPrefap);
            obj.transform.parent = transform;
            obj.SetActive(false);
            bulletPool.Add(obj);
        }
        
    }

    public GameObject GetBulletPool()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if(!bulletPool[i].activeInHierarchy)
            {
                return bulletPool[i];
            }
        }
        if(isDyanamic)
        {
            GameObject obj = Instantiate(bulletPrefap);
            obj.transform.parent = transform;
            obj.transform.position = transform.position;
            
            bulletPool.Add(obj);
            return obj;
        }
        return null;
    }

    public void OffBullets()
    {
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].activeInHierarchy)
            {
                bulletPool[i].SetActive(false);
            }
        }
    }

}
