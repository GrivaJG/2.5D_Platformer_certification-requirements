using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _uiScore;

    private int _score = 0;


    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("UIManager is NULL");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }


    public void UpdateScore()
    {
        _score++;

        _uiScore.text = "Score: " + _score.ToString();
    }
}
