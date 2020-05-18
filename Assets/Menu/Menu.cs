using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine;未包含UI程式庫，如果要宣告UI相關變數需要使用UI程式庫using UnityEngine.UI;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    bool ControlSound = false;
    [Header("聲音的按鈕")]
    public Image SoundButtonImage;
    [Header("聲音開啟圖")]
    public Sprite SoundOpenSprite;
    [Header("聲音關閉圖")]
    public Sprite SoundCloseSprite;
    [Header("聲音拉霸")]
    public Slider SoundSlider;
    [Header("下拉選單")]
    public Dropdown ScreenSizeDropdown;
    //Player 儲存/取值資料欄位的名稱
    string SaveAudioSlider = "SaveAudioSlider";

    private void Start()
    {
        Control_Sound();
        // SoundSlider.value = SaveData.SaveAudioSlider;
        SoundSlider.value = PlayerPrefs.GetFloat(SaveAudioSlider);
        AudioListener.volume = SoundSlider.value;
    }
    private void Update()
    {
        AudioListener.volume = SoundSlider.value;
        // print("ScreenSizeDropdown:" + ScreenSizeDropdown.value);
        /*if (ScreenSizeDropdown.value == 0)
        {
            // 設定遊戲執行檔的解析度Screen.SetResolution(寬度, 高度, 是否全螢幕)
            Screen.SetResolution(480, 800, false);
        }
        if (ScreenSizeDropdown.value == 1)
        {
            Screen.SetResolution(1080, 1920, false);
        }
        if (ScreenSizeDropdown.value == 2)
        {
            Screen.SetResolution(720, 1280, false);

        }
        */
        switch (ScreenSizeDropdown.value)
        {
            case 0:
                Screen.SetResolution(480, 800, false);
                break;
            case 1:
                Screen.SetResolution(1080, 1920, false);
                break;
            case 2:
                Screen.SetResolution(720, 1280, false);
                break;
        }
    }
    #region 下一關
    public void NextScence() 
    {
        //  Application.LoadLevel("場景名稱");
        //  Application.LoadLevel(場景名稱ID);
        //  Application.LoadLevel讀取當前關卡名稱
        //  Application.LoadLevel(Application.loadedLevel);重新遊戲
        // 儲存聲音Slider的value
        // SaveData.SaveAudioSlider = SoundSlider.value;
        // 儲存浮點數 PlayerPrefs.SetFloat(儲存欄位名稱,儲存浮點數)
        // 儲存字串 PlayerPrefs.SetString(儲存欄位名稱,儲存字串)
        // 儲存整數 PlayerPrefs.SetInt(儲存欄位名稱,儲存整數)
        PlayerPrefs.SetFloat(SaveAudioSlider, SoundSlider.value);
        Application.LoadLevel(1);
    }
    #endregion
    #region 遊戲關閉
    public void Quit()
    {
        //  輸出成遊戲執行檔才可以進行測試
        Application.Quit();
    }
    #endregion
    #region 控制聲音
    public void Control_Sound()
    {
        ControlSound = !ControlSound;   // !相反的意思
        if (ControlSound == true)
        {
            // AudioListener.pause = false; 整體遊戲聲音開啟
            AudioListener.pause = false;
            //  聲音的按鈕圖換成 開聲音的圖片
            SoundButtonImage.sprite = SoundOpenSprite;
        }
        else
        {
            // AudioListener.pause = true; 整體遊戲聲音關閉
            AudioListener.pause = true;
            // 聲音的按鈕圖換成 關聲音的圖片
            SoundButtonImage.sprite = SoundCloseSprite;
        }
    }
    #endregion

    public void ChangeAudioSlider()
    {
        if (SoundSlider.value == 0)
        {
            ControlSound = true;
            Control_Sound();
        }
        else
        {
            ControlSound = false;
            Control_Sound();
        }
    }
}
