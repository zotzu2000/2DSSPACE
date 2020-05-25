using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Range(0f, 1f)] public float Speed;
    [Header("選擇操控玩家的方式")]
    public ControlType Control;
    [Header("手機搖桿物件")]
    public GameObject JoystickObject;
    //判斷是否有使用手機搖桿
    bool UseJoystick;
    //判斷滑鼠是否點擊玩家物件
    bool MouseClick;
    public enum ControlType
    {
        鍵盤 = 0, 手機陀螺儀 = 1, 滑鼠 = 2, 手機搖桿 = 3
    }
    [Header("玩家血量")]
    public float PlayerHP;
    //程式中計算玩家的血量數值
    float ScripHP;
    [Header("玩家血條")]
    public Image HPBar;
    [Header("打死敵機加分")]
    public float AddScore;
    float ScriptScore;
    public Text ScoreText;

    //儲存分數的欄位
    string SaveScore = "SaveScore";
    // Start is called before the first frame update
    void Start()
    {
        //程式中血量=屬性面板中調整的玩家血量數值
        ScripHP = PlayerHP;
    }
    //敵機子彈打到玩家，玩家進行扣血
    public void HurtPlayer(float hurt)
    {
        //玩家血量遞減
        ScripHP -= hurt;
        //限制玩家的血量介於0-自己設定的數值
        ScripHP = Mathf.Clamp(ScripHP, 0, PlayerHP);
        //玩家血條的數值=程式中血量/自己設定的血量數值
        HPBar.fillAmount = ScripHP / PlayerHP;
        //如果玩家血量<=0
        if (ScripHP <= 0)
        {
            PlayerPrefs.SetFloat(SaveScore, ScriptScore);
            //跳到遊戲結束畫面
            Application.LoadLevel("GameOver");
        }
    }
    //玩家子彈打到怪物，玩家進行加分
    public void Score()
    {
        ScriptScore += AddScore;
        ScoreText.text = "Score:" + ScriptScore;
    }
    // Update is called once per frame
    void Update()
    {
        #region 鍵盤控制說明
        /*//Input.GetKey("a")按下A鍵if條件內的腳本持續執行
        if (Input.GetKey("a"))
        {
            transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
        }
        //Input.GetKeyUp("d")按下D鍵且放開，if條件內的腳本執行一次
        //Input.GetKeyUp("d")按下D鍵，if條件內的腳本執行一次
        if (Input.GetKeyUp("d"))
        {
            transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
        }*/
        //Input.GetAxis("Horizontal")沒有按按鍵的時候回傳值為0
        //Input.GetAxis("Horizontal")按A或是左鍵的時候回傳值為-1
        //Input.GetAxis("Horizontal")按D或是右鍵的時候回傳值為1
        //Input.GetAxis("Vertical")沒有按按鍵的時候回傳值為0
        //Input.GetAxis("Vertical")按S或是下鍵的時候回傳值為-1
        //Input.GetAxis("Vertical")按W或是上鍵的時候回傳值為1
        #endregion
        // #if UNITY_STANDALONE
        if ((int)Control == 0)
            transform.Translate(Speed * Input.GetAxis("Horizontal"), Speed * Input.GetAxis("Vertical"), 0f);
        // #endif
        // #if UNITY_ANDROID
        #region 手機陀螺儀說明
        //Input.acceleration.x手機陀螺儀x軸 Input.acceleration.y手機陀螺儀y軸
        #endregion
        if ((int)Control == 1)
            transform.Translate(Speed * Input.acceleration.x, Speed * Input.acceleration.y, 0f);
        // #endif
        #region 滑鼠說明
        //滑鼠左鍵ID=0 右鍵ID=1 中間滾輪ID=2
        //if(Input.GetMouseButtonDown(0)如果按下滑鼠左鍵，if條件裡面的程式只會執行一次
        //if(Input.GetMouseButton(1)如果按下滑鼠右鍵，if條件裡面的程式持續執行，直到放開右鍵才停止
        //if(Input.GetMouseButtonUp(2)如果按下滑鼠中鍵且放開，if條件裡面的程式只會執行一次
        #endregion
        if ((int)Control == 2)
        {
            if (Input.GetMouseButton(0))
            {
                if (MouseClick)
                {
                    //Input.mousePosition抓取滑鼠座標位置
                    Debug.Log(Input.mousePosition);
                    //紀錄轉換後的座標值
                    Vector3 Point;
                    // Camera.main透過遊戲主攝影機(Tag呼叫MainCamera)
                    // ScreenToWorldPoint將遊戲視窗螢幕的座標轉換成在遊戲編輯器內的3維座標數值
                    Point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    transform.position = new Vector3(Point.x, Point.y, transform.position.z);
                    //鼠標隱藏
                    Cursor.visible = false;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                //鼠標顯示
                Cursor.visible = true;
            }
        }
        if ((int)Control == 3)
        {
            JoystickObject.SetActive(true);
        }
        else
        {
            JoystickObject.SetActive(false);
        }
        #region 限制位置說明
        /*if (transform.position.x >= 2.3f)
            transform.position = new Vector3(2.3f, transform.position.y, transform.position.z);
        if (transform.position.x <= -2.3f)
            transform.position = new Vector3(-2.3f, transform.position.y, transform.position.z);
        */
        //限制數值Mathf.Clamp(限制的項目,最小值,最大值)
        #endregion
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -2.3f, 2.3f), Mathf.Clamp(transform.position.y, -4.6f, 4.6f), transform.position.z);
    }

    //手指剛接觸搖桿
    public void UsingJoystick()
    {
        UseJoystick = true;
    }
    //手指離開搖桿
    public void UnUsingStick()
    {
        UseJoystick = false;
    }
    //手指正在拖動搖桿
    public void IsUsingJoystick(Vector3 pos)
    {
        if (UseJoystick)
            transform.Translate(Speed * pos.x, Speed * pos.y, 0f);
    }
    //按下滑鼠左鍵點到玩家
    void OnMouseDown()
    {
        MouseClick = true;
    }
    //放開滑鼠左鍵
    void OnMouseUp()
    {
        MouseClick = false;
    }
}
