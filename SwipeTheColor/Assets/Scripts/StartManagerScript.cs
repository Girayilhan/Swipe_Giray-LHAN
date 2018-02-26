using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using google.service.game;

public class StartManagerScript : MonoBehaviour {
    public float spawnRate;
    int i;
    public GameObject srt1;
    public GameObject srt2;
    public GameObject srt3;
    public GameObject srt4;
    bool first;
    GoogleGame game;
    Vector3 spawnPoint;
    // Use this for initialization
   
    void Start () {
        game = GoogleGame.Instance();
        game.gameEventHandler += onGameEvent;
        game.login(true, false);
            
       

        spawnPoint = new Vector3(4, 0, 2);
         i = 0;
        StartCoroutine(SpawnS());
	}
    private void Update()
    {
        
    }
    void onGameEvent(int result_code, string eventName, string data)
    {
        Debug.Log(eventName + "-----------" + data);
    }

    void Spawn()
    {
        i = i % 4;
        if (i == 0)
        {
            Instantiate(srt1, spawnPoint, Quaternion.identity);
        }
        else if (i == 1)
        {
            Instantiate(srt2, spawnPoint, Quaternion.identity);
        }
        else if (i == 2)
        {
            Instantiate(srt3, spawnPoint, Quaternion.identity);
        }
        else if (i == 3)
        {
            Instantiate(srt4, spawnPoint, Quaternion.identity);
        }
        i++;
    }
    public void StartGame()
    {

        SceneManager.LoadScene(1);
    }
    public  void ShowLeaderboardsUI()
    {
        game.showLeaderboard("CgkIst_x8acJEAIQAQ");
    }


    IEnumerator SpawnS()
    {


        while (true)
        {
            Spawn();


            yield return new WaitForSeconds(spawnRate);


        }

    }
}
