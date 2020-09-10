using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuFire : MonoBehaviour
{
    bool isSpawn = false;
    void Update()
    {
        
        if (!isSpawn)
        {
            StartCoroutine(SpawnB3());
            Time.timeScale = 0.3f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
            isSpawn = true;
        }
        else
        {
            Time.timeScale += (1f/1f) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
        }
    }
    IEnumerator SpawnB3()
    {
        for (int x = 0; x < 3; x++)
        {
            for (int i = 0; i < 36; i++)
            {

                genBullet(Mathf.Cos(i * 0.2f) * 90f, transform);
                genBullet(Mathf.Sin(i * 0.1f) * 90f, transform);
                genBullet((Mathf.Cos(i * 0.2f) * 90f) + 180f, transform);
                genBullet((Mathf.Sin(i * 0.2f) * 90f) + 180f, transform);
                yield return new WaitForSeconds(0.1f);
            }
            for (int i = 0; i < 36 * 5f; i++)
            {
                genBullet(i * 10f, transform);
            }

            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2);
        isSpawn = false;
    }
    void genBullet(float dangle, Transform tf)
    {
        GameObject obj = SpawnBullet.instance.GetBulletPool();
        if (obj == null) return;
        obj.transform.position = tf.position;
        obj.transform.rotation = Quaternion.Euler(0f, 0f, dangle);
        obj.SetActive(true);
    }
}
