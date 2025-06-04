using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TimeManager : MonoBehaviour
{
    //カウントダウン
    public float countDown = 5.0f;
    public GameObject time_Object; // Textオブジェクト



    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        // オブジェクトからTextコンポーネントを取得
        Text timelimit_Text = time_Object.GetComponent<Text>();
        countDown = Mathf.Clamp(countDown, 0, 60);
        // テキストの表示を入れ替える
        timelimit_Text.text = "制限時間" + countDown.ToString("f0");

    }
}
