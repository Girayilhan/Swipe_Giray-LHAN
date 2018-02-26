using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreTextScript : MonoBehaviour {
    public Text text;
    public Color cl1, cl2, cl3, cl4;
    float timer;
    string temp;
    // Update is called once per frame
    private void Start()
    {
        text = GetComponent<Text>();
    }
    void Update () {
        timer += Time.deltaTime;
       
        if(timer%4 >0 && timer % 4 < 1)
        {
            
            text.color = cl1;
          
           
        }
        if (timer % 4 > 1 && timer % 4 < 2)
        {
            text.color = cl2;
           
        }
        if (timer % 4 > 2 && timer % 4 < 3)
        {
            text.color = cl3;
           
        }
        if (timer % 4 > 3 && timer % 4 < 4)
        {
            text.color = cl4;
           
        }
    }
}
