using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using admob;
using google.service.game;




public class GameManager : MonoBehaviour {
    public GameObject[] cards = new GameObject[4];
    public List<GameObject> cardList = new List<GameObject>();
    public List<GameObject> cardsArray = new List<GameObject>();
    public Swipe swipeController;
    //this x for random
    int x;

    Admob ad;

    bool adFlag;
    GoogleGame game;



    private AudioSource audioSrce;

    public float swipeSpeed;
    public float sendingDistance;
    static int ct;
    int highScore;
    int sendingObject;

    float gameTime;
    int swipeTime;

    float timer;

   

    public static  bool isGameOver;

    Vector3 startPositionToSort;
    Vector3 sendPosition;

    public  Text timeText;
    public  Text endGame;
    public  Text scoreText;

    public GameObject playAgainButton;
    public GameObject homePageButton;
    public GameObject resumeButton;
    public GameObject highScoreEffect;

    string path = "Assets/info.txt";
    string gamePath = "Assets/gameInfo.txt";
    // Use this for initialization
    void Start () {

        game = GoogleGame.Instance();
        game.gameEventHandler += onGameEvent;
       
        adFlag = true;
        audioSrce = GetComponent<AudioSource>();
        highScore = PlayerPrefs.GetInt("highscore", highScore);
        gameTime = PlayerPrefs.GetInt("gameTime", (int)gameTime);
        swipeTime = PlayerPrefs.GetInt("swipeTime", swipeTime);
        
        isGameOver = false;
        ct = 0;
        timer = 30;
        sendingObject = ct;
        startPositionToSort = new Vector3(0, 1, 0);
        sendPosition= new Vector3(0, 1, 0);
        MakeList();
        Sort();
        sendingObject = ct;
        playAgainButton.active = false;
        homePageButton.active = false;
        resumeButton.active = false;
        initAdmob();

        Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.TOP_CENTER, 0);
       
        
    }
    void initAdmob()
    {

        ad = Admob.Instance();
        ad.interstitialEventHandler += onInterstitialEvent;
        ad.bannerEventHandler += onBannerEvent;
        ad.initAdmob("ca-app-pub-7657412837781109/5184954636", "ca-app-pub-7657412837781109/4023916208");

    }
    void onInterstitialEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobEvent---" + eventName + "   " + msg);
        if (eventName == AdmobEvent.onAdLoaded)
        {
            Admob.Instance().showInterstitial();
        }
    }
    void onBannerEvent(string eventName, string msg)
    {
        Debug.Log("handler onAdmobBannerEvent---" + eventName + "   " + msg);
    }
   
    
    // Update is called once per frame
    void Update () {
        // this win with counter
        /* if (ct == 50)
        {
          Win();
        }*/
       
        if (timer <= 0 || isGameOver)
        {
            timer = 0;
            
            GameOver();
            
        }
        if (!isGameOver)
        {
            playAgainButton.SetActive(false);
            homePageButton.SetActive(false);
            highScoreEffect.SetActive(false);
            if (swipeController.SwipeLeftUp)
            {

                sendPosition = new Vector3(0, 1, 0);
                sendPosition += (Vector3.left+Vector3.up) * sendingDistance;
                sendingObject = ct;
                ct++;
                MoveForward(ct);
                audioSrce.Play();
                swipeTime++;
            }
            if (swipeController.SwipeRigthUp)
            {

                sendPosition = new Vector3(0, 1, 0);
                sendPosition += (Vector3.right+Vector3.up) * sendingDistance;
                sendingObject = ct;
                ct++;
                MoveForward(ct);
                audioSrce.Play();
                swipeTime++;
            }
            if (swipeController.SwipeLeftDown)
            {

                sendPosition = new Vector3(0, 1, 0);
                sendPosition += (Vector3.left+Vector3.down) * sendingDistance;
                sendingObject = ct;
                ct++;
                MoveForward(ct);
                audioSrce.Play();
                swipeTime++;
            }
            if (swipeController.SwipeRightDown)
            {

                sendPosition = new Vector3(0, 1, 0);
                sendPosition += (Vector3.down+Vector3.right) * sendingDistance;
                sendingObject = ct;
                ct++;
                MoveForward(ct);
                audioSrce.Play();
                swipeTime++;
            }
            cardsArray[sendingObject].transform.position = Vector3.MoveTowards(cardsArray[sendingObject].transform.position, sendPosition, swipeSpeed * Time.deltaTime);
            //decrease the timer here
            timer -= Time.deltaTime;
            gameTime += Time.deltaTime;
            timeText.text =""+(int)timer;
            scoreText.text = "SCORE\n" + ct;
        }

    }
    void Sort()
    {
        for(int i = 0; i <cardList.Count; i++)
        {
            cardsArray[i]=Instantiate(cardList[i], startPositionToSort, Quaternion.Euler(0,0,45));

            startPositionToSort = new Vector3(0, 1, startPositionToSort.z + 10);
        }

    }
    void MakeList()
    {
        for(int i = 0; i < 200; i++)
        {
            x = UnityEngine.Random.Range(0, 1000)%4;
            cardList.Add(cards[x]);
        }
    }
    void MoveForward(int a)
    {
        for(int i = a; i <cardsArray.Count;i++)
        {
            cardsArray[i].transform.position = new Vector3(0, 1, cardsArray[i].transform.position.z - 10);
        }

    }
   public  void GameOver()
    {
        
       Admob.Instance().removeAllBanner();
        if (ad.isInterstitialReady())
        {
            if (adFlag)
            {
                ad.showInterstitial();
                ad.loadInterstitial();
                adFlag = false;
                
            }
            
           
        }
        else
        {
            if (adFlag)
            {
                ad.loadInterstitial();

            }
            
        }
        //**********************************************************************************
        var clones = GameObject.FindGameObjectsWithTag("Card");
        foreach (var clone in clones)
        {
            Destroy(clone);
        }
        if (highScore < ct)
        {  
          
            highScore = ct;
            highScoreEffect.active=true;
            game.submitLeaderboardScore("CgkIst_x8acJEAIQAQ", highScore);

        }
        
       
        PlayerPrefs.SetInt("highscore", highScore);
        PlayerPrefs.SetInt("gameTime", (int)gameTime);
        PlayerPrefs.SetInt("swipeTime", swipeTime);
        PlayerPrefs.Save();

        isGameOver = true;
        Time.timeScale = 1;
        endGame.text = "YOUR SCORE\n" + ct+"\nHIGH SCORE\n"+highScore;
        scoreText.text = "GAME OVER";
        timeText.text = "";
        playAgainButton.active = true;
        homePageButton.active = true;
        

    }

   public void PlayAgain()
    {
        isGameOver = false;
        Time.timeScale = 1;
        endGame.text = "";
        scoreText.text = "";
        timeText.text = "";
        ct = 0;
        timer = 30;
        sendingObject = ct;
        x = 0;
        startPositionToSort = new Vector3(0, 1, 0);
        sendPosition = new Vector3(0, 1, 0);
        cardList.Clear();
        MakeList();
        Sort();
        adFlag = true;
        Admob.Instance().showBannerRelative(AdSize.SmartBanner, AdPosition.TOP_CENTER, 0);
 }
    public void GoHome()
    {
        SceneManager.LoadScene(0);
        
        isGameOver = false;
        Time.timeScale = 1; 
        endGame.text = "";
        scoreText.text = "";
        timeText.text = "";
        ct = 0;
        timer = 30;
        sendingObject = ct;
        x = 0;
        startPositionToSort = new Vector3(0, 1, 0);
        sendPosition = new Vector3(0, 1, 0);
        cardList.Clear();
        MakeList();
        Sort();
        adFlag = true;
        
    }
    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            resumeButton.active = true;
        }
       
    }
    public void soundClose()
    {
        if (audioSrce.volume > 0)
        {
            audioSrce.volume = 0;
        }
        else
        {
            audioSrce.volume = 1;
        }

    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        resumeButton.active = false;
    }
    void onGameEvent(int result_code, string eventName, string data)
    {
        Debug.Log(eventName + "-----------" + data);
    }

}
