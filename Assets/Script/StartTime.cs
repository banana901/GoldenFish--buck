using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTime : MonoBehaviour
{
    [SerializeField] float startCountDown = 3f;
    public GameObject time_Object; // Textオブジェクト
 
    public GameObject EnemyManager;
    public GameObject timer;
    // Start is called before the first frame update
    void Start()
    {
       
        EnemyManager.SetActive(false);
        timer.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {
        startCountDown -= Time.deltaTime;

        // オブジェクトからTextコンポーネントを取得
        startCountDown = Mathf.Clamp(startCountDown, 0, 3);
        Text timelimit_Text = time_Object.GetComponent<Text>();
        timelimit_Text.text = startCountDown.ToString("f0");

        if (startCountDown <= 0)
        {
            timelimit_Text.text = startCountDown.ToString("START");
           
            EnemyManager.SetActive(true);
            timer.SetActive(true);

            Invoke("unko", 1f);


        }

    }

    void unko()
    {
        Text timelimit_Text = time_Object.GetComponent<Text>();
        timelimit_Text.enabled = false;
    }


}
