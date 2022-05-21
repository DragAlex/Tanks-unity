using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text Text;
    private string scorePlayer;
    private string scoreEnemy;

    void Start()
    {
        Text = GetComponent<Text>();
        if (!PlayerPrefs.HasKey("score1"))
            PlayerPrefs.SetInt("score1", 0);
        else
            scorePlayer = (PlayerPrefs.GetInt("score1").ToString());
        if (!PlayerPrefs.HasKey("score2"))
            PlayerPrefs.SetInt("score2", 0);
        else
            scoreEnemy = (PlayerPrefs.GetInt("score2").ToString());

        Text.text = PlayerPrefs.GetInt("score1").ToString() + ":" + PlayerPrefs.GetInt("score2").ToString();
    }
}
