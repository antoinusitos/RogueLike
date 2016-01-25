using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    private static EnemyManager instance = null;

    public GameObject enemy;

    public static EnemyManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnEnemy(GameObject[] tab)
    {
        int rand = -1;
        while(rand == -1)
        {
            rand = Random.Range(0, tab.Length);
            if (tab[rand] != null)
            {
                Instantiate(enemy, tab[rand].transform.position + new Vector3(0, 1, 0), Quaternion.identity);

            }
            else
            {
                rand = -1;
            }
        }
    }
}
