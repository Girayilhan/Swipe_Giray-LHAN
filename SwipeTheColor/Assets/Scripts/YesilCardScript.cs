using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YesilCardScript : MonoBehaviour {
   public  bool isSwiped;
    float timer;
  
    // Use this for initialization
    void Start () {
        isSwiped = false;
       
	}
	
	// Update is called once per frame
	void Update () {
        
        if (isSwiped)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                Destroy(gameObject);
            }
        }
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag != "Yesil" && other.transform.tag != "Card")
        {
            Destroy(gameObject);
            GameManager.isGameOver = true;

        }
        else if(other.transform.tag=="Yesil")
        {
            isSwiped = true;
        }
    }
}
