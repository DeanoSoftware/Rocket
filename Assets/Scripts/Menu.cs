using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Menu : MonoBehaviour
{
    public TMP_Text textScore;

    private UserData userData;
    private int score = 0;

    private void Start()
    {
        userData = SaveController.Load();

        if (userData == null)
        {
            //Debug.Log("UserData is null");
            userData = new UserData();
            SaveController.Save(userData);
        } else
        {
            score = userData.HighScore;
            textScore.text = "Score " + score.ToString();
        }


    }
}
