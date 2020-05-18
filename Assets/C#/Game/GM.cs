using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    [Header("所有敵機物件")]
    public GameObject[] Enemy;
    [Header("設定每幾秒要產出敵機")]
    public float CreatTime;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemy", CreatTime, CreatTime);
    }
    void CreateEnemy()
    {
        //生成位置Random
        Vector3 pos = new Vector3(Random.Range(-1f,2.5f), transform.position.y, transform.position.z);
        //動態生成
        Instantiate(Enemy[Random.Range(0, Enemy.Length)], transform.position, transform.rotation);
    }
   
}
