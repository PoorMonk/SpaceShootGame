using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public enum GAMESTATUS
    {
        GAMESTART,
        GAMEEND
    }

    private GAMESTATUS m_GameStatus;

    private GameObject m_GameStart;
    private GameObject m_GameOver;
    private GameObject m_GameWin;
    private GameObject m_GameLose;
    public GameObject m_Player;

    private int m_score = 0;
    public int m_targetScore = 1000;
    private GUIText m_GUITextSocre;

    private GameControllor m_GameController;

    private bool m_isWin = false;
    // Use this for initialization
    void Start () {
        m_GameStart = GameObject.Find("GameStart");
        m_GameOver = GameObject.Find("GameOver");
        m_GameWin = GameObject.Find("GamePass");
        m_GameLose = GameObject.Find("GameFailed");
        //m_Player = GameObject.Find("player");
        //m_Player = null;

        m_GameController = GameObject.FindWithTag("GameController").GetComponent<GameControllor>();
        m_GUITextSocre = GameObject.Find("GameScore").gameObject.GetComponent<GUIText>();
        ChangeGameStatus(GAMESTATUS.GAMESTART);
        ShowScore();
    }
	
	// Update is called once per frame
	void Update () {
        if (m_GameStatus == GAMESTATUS.GAMEEND && Input.GetKeyDown(KeyCode.R))
        {
            ChangeGameStatus(GAMESTATUS.GAMESTART);
            m_GameController.BeginCreate();
            //Application.LoadLevel(Application.loadedLevel);
        }
    }

    /// <summary>
    /// 切换游戏状态
    /// </summary>
    /// <param name="status">状态枚举值</param>
    public void ChangeGameStatus(GAMESTATUS status)
    {
        m_GameStatus = status;

        if (m_GameStatus == GAMESTATUS.GAMESTART)
        {
            m_isWin = false;
            m_GameOver.SetActive(false);
            //m_Player.SetActive(true);
            m_GameController.GameStart();
            CreatePlayer();
            ShowScore();
        }
        else if (m_GameStatus == GAMESTATUS.GAMEEND)
        {
            //m_Player.SetActive(false);
            m_GameOver.SetActive(true);
            m_score = 0;
            GameObject go = GameObject.FindWithTag("Player");
            if(go != null)
            {
               Destroy(go.gameObject);
            }
            
        }

        if(m_isWin)
        {
            m_GameWin.SetActive(true);
            m_GameLose.SetActive(false);
        }
        else
        {
            m_GameWin.SetActive(false);
            m_GameLose.SetActive(true);
        }
    }

    void CreatePlayer()
    {
        Instantiate(m_Player, Vector3.zero, Quaternion.identity);            
    }

    /// <summary>
    /// 增加分数
    /// </summary>
    /// <param name="iScore">分数</param>
    public void AddScore(int iScore)
    {
        m_score += iScore;
        ShowScore();

        if(m_targetScore < m_score)
        {
            m_isWin = true;
            ChangeGameStatus(GAMESTATUS.GAMEEND);
            m_GameController.GameOver();          
        }
    }

    /// <summary>
    /// 显示分数
    /// </summary>
    private void ShowScore()
    {
        m_GUITextSocre.text = "Score:" + m_score;
    }
}
