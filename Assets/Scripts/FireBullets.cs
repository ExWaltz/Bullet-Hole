using System.Collections;
using UnityEngine;

public class FireBullets : MonoBehaviour
{
    public static FireBullets instance;
    public float Timer = 0f, MaxTime = 50f, eWave = 1f, enemies = 4f, calDis = 0f;
    private GameObject obj;
    private Vector2 distance;
    private bool isSpawning = false;
    private bool isSpawned = false;
    public bool isRes = false;
    int powerOfset = 0;
    float Score = 0f;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        calculateDis();

        Invoke("Attack", 0.1f);
        CalScore();





    }
    void CalScore()
    {
        if (!GameMaster.instance.isDeath)
        {
            Score += Time.deltaTime;
            float xScore = (int)Score;
            
            GameMaster.instance.Score.text = xScore.ToString();
            if((int)Score % 10 == 0 && !isSpawned)
            {
                isSpawned = true;
                GameObject Obj = PowerPool.instance.GetPowerPool();
                if (Obj == null)
                {
                    powerOfset = 0;
                    return;
                }
                Obj.transform.localPosition = new Vector3(Obj.transform.localPosition.x + powerOfset * 100, 0f, 0f);
                powerOfset++;
                
                Obj.SetActive(true);
                
            }else if((int)Score % 12 == 0)
            {
                isSpawned = false;
            }
        }
    }
    void Attack()
    {
        if (isRes)
        {


            Timer = 0f;
            MaxTime = 4f;
            eWave = 1f;
            enemies = 4f;
            Score = 0f;
            isRes = false;
            isSpawning = false;
            StopAllCoroutines();

        }
        if (!isSpawning && !isRes)
        {
            float rTime = Timer;
            float x = Mathf.Clamp(rTime, rTime, 4f);

            if (Timer > MaxTime)
            {
                Timer = 0f;
                Debug.Log("reset");

                isSpawning = false;
                Timer = Random.Range(0f, MaxTime);
            }
            else if (Timer > MaxTime - 1)
            {
                StartCoroutine(SpawnB3(-x + 5f, enemies, eWave));
                StartCoroutine(SpawnB2(-x + 5f, enemies, eWave));
                isSpawning = true;
                SoundManager.PlayerSound("Fire");
            }
            else if (Timer > MaxTime - 2)
            {
                StartCoroutine(SpawnB4(-x + 5f, enemies, eWave));
                StartCoroutine(SpawnB2(-x + 5f, enemies, eWave));
                isSpawning = true;
                SoundManager.PlayerSound("Fire");
            }
            else if (Timer > MaxTime / 2f)
            {

                StartCoroutine(SpawnB2(-x + 5f, enemies, eWave));
                isSpawning = true;
                SoundManager.PlayerSound("Fire");

            }
            else
            {
                StartCoroutine(SpawnB1(-x + 5f, enemies, eWave));
                isSpawning = true;
                SoundManager.PlayerSound("Fire");
            }
            MaxWave();
            Timer = Random.Range(0f, MaxTime);
            isSpawning = true;

            



        }
    }
    void MaxWave()
    {
        if (eWave > 2f)
        {
            eWave = 3f;
            enemies = 10f;
        }
        else
        {
            eWave++;
            enemies++;
        }
    }
    void calculateDis()
    {
        distance = (Vector2)PlayerMovement.instance.player.transform.position - (Vector2)transform.position;
        
        calDis = Mathf.Atan2(distance.y, distance.x) * Mathf.Rad2Deg - 90f;
    }

    IEnumerator SpawnB1(float time, float enemyNum, float enemyWave)
    {

        for (int x = 0; x < enemyWave; x++)
        {

            for (int i = 0; i < enemyNum; i++)
            {

                genBullet(calDis, transform);
                genBullet(calDis + Random.Range(-10f, 10f), transform);
                genBullet(90f + calDis, transform);
                genBullet(90f + calDis + Random.Range(-10f, 10f), transform);
                genBullet(180f + calDis, transform);
                genBullet(180f + calDis + Random.Range(-10f, 10f), transform);
                genBullet(270f + calDis, transform);
                genBullet(270f + calDis + Random.Range(-10f, 10f), transform);

            }
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(time);
        isSpawning = false;
    }

    IEnumerator SpawnB2(float time, float enemyNum, float enemyWave)
    {
        for (int x = 0; x < enemyWave; x++)
        {
            for (int i = 0; i < enemyNum; i++)
            {
                genBullet(calDis, transform);
                genBullet(90f + calDis, transform);
                genBullet(180f + calDis, transform);
                genBullet(270f + calDis, transform);

                yield return new WaitForSeconds(0.1f);

            }
            yield return new WaitForSeconds(0.3f);

        }
        yield return new WaitForSeconds(time);
        isSpawning = false;
    }

    IEnumerator SpawnB3(float time, float enemyNum, float enemyWave)
    {
        for (int x = 0; x < enemyWave; x++)
        {
            for (int i = 0; i < enemyNum * 3f; i++)
            {
                
                genBullet(Mathf.Cos(i*0.2f) * 90f, transform);
                genBullet(Mathf.Sin(i*0.1f) * 90f, transform);
                genBullet((Mathf.Cos(i * 0.2f) * 90f) + 180f, transform);
                genBullet((Mathf.Sin(i * 0.2f) * 90f) + 180f, transform);
                yield return new WaitForSeconds(0.1f);
            }
            
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(time);
        isSpawning = false;
    }
    IEnumerator SpawnB4(float time, float enemyNum, float enemyWave)
    {
        for (int x = 0; x < enemyWave; x++)
        {
            for (int i = 0; i < enemyNum * 6f; i++)
            {
                genBullet(i * 10f, transform);
                genBullet(i * -10f, transform);
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.3f);
        }
        yield return new WaitForSeconds(time);
        isSpawning = false;
    }

    void genBullet(float dangle, Transform tf)
    {
        obj = SpawnBullet.instance.GetBulletPool();
        if (obj == null) return;
        obj.transform.position = tf.position;
        obj.transform.rotation = Quaternion.Euler(0f, 0f, dangle);
        obj.SetActive(true);
    }
}
