using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour
{
    public int count = 0;
    public GameObject x, y;
    public static Manager instance;
    public List<GameObject> list = new List<GameObject>();
    public bool redcoinrespwnbool;
    public GameObject gameoverText, panel, leavepanel,linerender;
    public score score1;
    public score score2;
    public int value1, value2;
    public TextMeshProUGUI wintext;
    public ParticleSystem VFX;

    AudioSource audioSource;
    public AudioClip audioClip;
    bool vfxbool;
    public float timer;
    public GameObject Radialslider_1,Radialslider_2;
   
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        gameoverText.SetActive(false);
        panel.SetActive(false);
        leavepanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        Radialslider_1.SetActive(true);
        Radialslider_2.SetActive(false);
    }

    void Update()
    {

        if (list.Count <= 0)
        {
            Debug.Log("Game Over!");
            gameoverText.SetActive(true);
            timer += Time.deltaTime;
            linerender.SetActive(false);

            if (timer >= 5)
            {

                value1 = score1.counter;
                value2 = score2.counter;
                if (value1 > value2)
                {
                    Debug.Log("1");
                    wintext.text = "Congratulations player-1 Won the Match" + " Score : " + value1;
                    panel.SetActive(true);
                    StartCoroutine(Cracks());

                }
                else if (value1 < value2)
                {
                    Debug.Log("2");
                    wintext.text = "Congratulations player-2 Won the Match " + "  Score : " + value2;
                    panel.SetActive(true);
                    StartCoroutine(Cracks());
                }
                else
                {
                    Debug.Log("3");
                    wintext.text = "Both Player are Equal Points";
                    panel.SetActive(true);

                }

            }
            GameObject g2 = GameObject.FindGameObjectWithTag("striker");
            if (g2.activeSelf)
            {
                g2.SetActive(false);
            }
           
            GameObject g1 = GameObject.FindGameObjectWithTag("striker");
            if (g1.activeSelf)
            {
                g1.SetActive(false);
            }
          

            return;
        }

        if (CoinCollect.instance.onstrike == false && CoinCollect.instance.ons2 == false)
        {
            if (count % 2 == 0)
            {
                x.SetActive(true);
                y.SetActive(false);
                Radialslider_1.SetActive(true);
                Radialslider_2.SetActive(false);

            }
            else
            {
                x.SetActive(false);
                y.SetActive(true);
                Radialslider_1.SetActive(false);
                Radialslider_2.SetActive(true);
            }
        }

      
    }
    public void ChecknumberofCoins(GameObject obj)
    {

        list.Remove(obj);


    }
    public void AddCoin(GameObject obj)
    {
        list.Add(obj);
        redcoinrespwnbool = true;
    }

    IEnumerator Cracks()
    {
        yield return new WaitForSeconds(1.5f);

        if (!vfxbool)
        {
            VFX.Play();
            audioSource.PlayOneShot(audioClip);
            timer = 0;
            vfxbool = true;
        }

    }
    public void leavepannal()
    {
        bool d=leavepanel.activeInHierarchy;
        leavepanel.SetActive(!d);
    }
   
    public void RestartScene()
    {
        SceneManager.LoadScene("Carrom");
    }
    public void Exit()
    {
        Application.Quit();
    }
}
