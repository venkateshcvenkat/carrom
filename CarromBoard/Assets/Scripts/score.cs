using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class score : MonoBehaviour
{
    public int counter = 0;
    public TextMeshProUGUI scoreText;

    private void Update()
    {
        scoreText.text = counter.ToString();
    }
}
