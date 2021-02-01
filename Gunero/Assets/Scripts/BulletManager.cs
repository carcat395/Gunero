using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public static List<GameObject> enemyBullets = new List<GameObject>();
    public static List<GameObject> playerBullets = new List<GameObject>();

    public static GameObject GetEnemyBulletFromPool()
    {
        for (int i = 0; i < enemyBullets.Count; i++)
        {
            if (!enemyBullets[i].activeSelf)
            {
                enemyBullets[i].GetComponent<Bullet>().ResetTimer();
                enemyBullets[i].SetActive(true);
                return enemyBullets[i];
            }

        }
        return null;
    }

    public static GameObject GetPlayerBulletFromPool()
    {
        for (int i = 0; i < playerBullets.Count; i++)
        {
            if (!playerBullets[i].activeSelf)
            {
                playerBullets[i].GetComponent<Bullet>().ResetTimer();
                playerBullets[i].SetActive(true);
                return playerBullets[i];
            }

        }
        return null;
    }
}
