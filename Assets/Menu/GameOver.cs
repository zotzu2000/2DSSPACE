using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //儲存分數的欄位
    string SaveScore = "SaveScore";
    public Text ScoreText;
    string SaveHeightScore = "SaveHeightScore";
    string SaveLevelID = "SaveLevelID";
    public Text HeightScoreText;

    [Header("下一關的按鈕")]
    public Button NextButton;
    // Start is called before the first frame update
    void Start()
    {
        ScoreText.text = "Score:" + PlayerPrefs.GetFloat(SaveScore);

        HeightScoreText.text = "Height Score:" + PlayerPrefs.GetFloat(SaveHeightScore) + PlayerPrefs.GetFloat(SaveLevelID);
        //如果目標分數>目前的分數=失敗
        if (PlayerPrefs.GetFloat(SaveHeightScore + PlayerPrefs.GetFloat(SaveLevelID)) > PlayerPrefs.GetFloat(SaveScore))
        {
            NextButton.interactable = false;
        }
        //如果目標分數<目前得分數=成功
        else
        {
            NextButton.interactable = true;
        }
        //鼠標顯示
        Cursor.visible = true;

    }
    //下一關
    public void NextGame()
    {
        if (PlayerPrefs.GetFloat(SaveLevelID) >= Level.OpenLevelID)
            Level.OpenLevelID++;
        Application.LoadLevel("Level");
    }
    //重新遊戲
    public void ReGame()
    {
        Application.LoadLevel("Game");
    }
    //回首頁
    public void Menu()
    {
        Application.LoadLevel("Menu");
    }
}
