using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllor : MonoBehaviour {

    public Vector3 m_PlanetPos;
    public GameObject[] m_PlanetObjects;
    public GameObject m_enemy;

    public float startWait = 2.0f;
    public float spawnWait = 1.0f;
    public float deltaWait = 4.0f;

    public int numPerWave = 10;

    private bool m_GameOver = false;

    //private enemyController m_enemyController;

    // Use this for initialization
    void Start () {
        //InvokeRepeating("CreatePlanets", 1.0f, 5.0f);
        StartCoroutine(SpawnWaves());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BeginCreate()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for (int i = 0; i < numPerWave; ++i)
            {
                if(0 == i % 3)
                {
                    CreateEnemy();
                }
                if (m_GameOver)
                {
                    break;
                }
                Spawn();
                yield return new WaitForSeconds(spawnWait);
            }
            
            yield return new WaitForSeconds(deltaWait);

            if(m_GameOver)
            {
                break;
            }
        }
        
    }

    /// <summary>
    /// 创建一个小行星
    /// </summary>
    void Spawn()
    {
        GameObject go = m_PlanetObjects[Random.Range(0, m_PlanetObjects.Length)];
        Vector3 vecPos = new Vector3(Random.Range(-m_PlanetPos.x, m_PlanetPos.x), m_PlanetPos.y, m_PlanetPos.z);
        Instantiate(go, vecPos, Quaternion.identity);
    }

    /// <summary>
    /// 创建敌机
    /// </summary>
    void CreateEnemy()
    {
        Vector3 vecPos = new Vector3(Random.Range(-m_PlanetPos.x, m_PlanetPos.x), m_PlanetPos.y, m_PlanetPos.z);
        Instantiate(m_enemy, vecPos, Quaternion.identity);
        //m_enemy.GetComponent<enemyController>().CreateBulletInterface();
    }

    /// <summary>
    /// 创建10个小行星
    /// </summary>
    void CreatePlanets()
    {
        for(int i = 0; i < 10; ++i)
        {
            GameObject go = m_PlanetObjects[Random.Range(0, m_PlanetObjects.Length)];
            Vector3 vecPos = new Vector3(Random.Range(-m_PlanetPos.x, m_PlanetPos.x), m_PlanetPos.y, Random.Range(16.0f, 24.0f)/*m_PlanetPos.z*/);
            Instantiate(go, vecPos, Quaternion.identity);
        }
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
        m_GameOver = true;
    }

    /// <summary>
    /// 游戏开始
    /// </summary>
    public void GameStart()
    {
        m_GameOver = false;
    }
    
}
