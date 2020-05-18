using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("下一個關卡的名稱")]
    public string NextSceneName;
    public void NextScence()
    {
        //如果場景名稱為Menu
       /* if (NextSceneName == "Menu")
        {
            //Destroy刪除物件
            Destroy(GameObject.Find("BGM").gameObject);
        }
        */
        if (NextSceneName == "Movie")
        {
            //GameObject.Find(物件名稱).SetActive判斷物件是否要開啟
            //GameObject.Find("BGM").SetActive(false);
            GameObject.Find("BGM").GetComponent<AudioSource>().enabled = false;
        }
        if (NextSceneName == "Game")
        {
            //GameObject.Find("BGM").SetActive(false);
            GameObject.Find("BGM").GetComponent<AudioSource>().enabled = true;
        }
        Application.LoadLevel(NextSceneName);

    }
}
