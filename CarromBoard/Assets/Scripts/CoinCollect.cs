using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Runtime.CompilerServices;

public class CoinCollect : MonoBehaviour
{
    public int point = 0;
    public TextMeshProUGUI pointtext;

    public GameObject score1, score2;
    int decideInteger = 0;

    public GameObject[] player;
    public bool onstrike, ons2;
    public striker strikerscript;
    public oppstriker oppstrikerscript;
    public static CoinCollect instance;
    public bool redcoinbool = false, checkbool, timerbool;
    public float counttime;
    public TextMeshProUGUI counttimeText;
    public GameObject OGredcoin;
    public GameObject[] holes;
    public float t;
    public bool Backupcoinbool;


   /* public AudioSource AudioSource;
    public AudioClip clip1,clip2,clip3;*/

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        counttimeText.gameObject.SetActive(false);
      //  AudioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (redcoinbool == true && !timerbool)
        {
            counttime -= Time.deltaTime;
            counttimeText.text = Mathf.RoundToInt(counttime).ToString();
        }


        if (checkbool = true && redcoinbool == true)
        {
            redcointimer();

        }
        else
        {
            t = 20;

            redcoinbool = false;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("coin")))
        {
          //  AudioSource.PlayOneShot(clip1);
            Manager.instance.ChecknumberofCoins(other.gameObject);
            Destroy(other.gameObject);

            OnStiker();
           
            if (!redcoinbool)
            {
                StartCoroutine(packettime());
            }
            if ((redcoinbool == true) && (counttime >= 0 && counttime < 20))
            {
                Debug.Log("supershot");
                StartCoroutine(superShot());
                counttimeText.gameObject.SetActive(false);
                checkbool = false;
                foreach (GameObject hole in holes)
                {
                    hole.GetComponent<CoinCollect>().redcoinbool = false;
                    hole.GetComponent<CoinCollect>().counttime = 20;
                }
            }
            


        }
       
        if (other.gameObject.CompareTag("Red"))
        {
          //  AudioSource.PlayOneShot(clip1);
            Manager.instance.ChecknumberofCoins(other.gameObject);
            OGredcoin.SetActive(false);
            counttimeText.gameObject.SetActive(true);

            foreach (GameObject hole in holes)
            {
                hole.GetComponent<CoinCollect>().redcoinbool = true;
            }

            OnStiker();
            checkbool = true;


        }


    }
    public void OnStiker()
    {
        onstrike = true;
        ons2 = true;
        if (onstrike == true)
        {
            player = GameObject.FindGameObjectsWithTag("striker");
            strikerscript.strikerReset();
            onstrike = false;
           
           
        }
        if (ons2 == true)
        {
            player = GameObject.FindGameObjectsWithTag("striker");
            oppstrikerscript.strikerReset();
            ons2 = false;
        }
    }
    IEnumerator packettime()
    {
        yield return new WaitForSeconds(3);
        decideInteger = Manager.instance.count;

        if (decideInteger % 2 == 0)
        {
            score1.GetComponent<score>().counter = score1.GetComponent<score>().counter + 10;
        }
        else
        {
            score2.GetComponent<score>().counter = score2.GetComponent<score>().counter + 10;
        }

    }
    IEnumerator superShot()
    {
        OnStiker();

        yield return new WaitForSeconds(2);
        decideInteger = Manager.instance.count;

        if (decideInteger % 2 == 0)
        {
            score1.GetComponent<score>().counter = score1.GetComponent<score>().counter + 30;

        }
        else
        {
            score2.GetComponent<score>().counter = score2.GetComponent<score>().counter + 30;

        }
        Manager.instance.ChecknumberofCoins(OGredcoin.gameObject);
    }
    void redcointimer()
    {
        t -= Time.deltaTime;

        if (Backupcoinbool)
        {
            t = 0;
            Debug.Log("Backup Coin Not Pocket");
        }

        if (t > 0  ) return;
    

        Debug.Log("20minover");
        counttimeText.gameObject.SetActive(false);
       
        onstrike = false;
        ons2 = false;
        foreach (GameObject hole in holes)
        {
            hole.GetComponent<CoinCollect>().timerbool = true;
            hole.GetComponent<CoinCollect>().counttime = 20;
        }

        if (checkbool == true)
        {
            OGredcoin.SetActive(true);
            OGredcoin.transform.position = new Vector3(0, 0, 0);

            
           // t = 20;
            foreach (GameObject hole in holes)
            {
                hole.GetComponent<CoinCollect>().timerbool = false;
                hole.GetComponent<CoinCollect>().Backupcoinbool = false;
                hole.GetComponent<CoinCollect>().redcoinbool = false;
            }
            if (Manager.instance.redcoinrespwnbool == false)
            {
                Manager.instance.AddCoin(OGredcoin.gameObject);
            }
        }
    } 

}
