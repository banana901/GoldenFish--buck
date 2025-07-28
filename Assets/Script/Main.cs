using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using Unity.Mathematics;



public class Main : MonoBehaviour
{
    public TimeManager timeManager;
    public GameObject finishTxt;
    public GameObject poi;
    public GameObject hand;
    public GameObject rainBow;
    public GameObject posi;

    public GameObject feverPrefab;
    public Transform startPoint;
    public Transform centerPoint;
    public Transform endPoint;

    public ScoreManager scoreManager;

    public Slider mySlider;
    [SerializeField] Slider handSlider;
    public Transform canvasTransform;

    private enum HandState
    {
        Poi,
        Hand
    }

    private HandState currentHandState = HandState.Poi;






    enum Mode
    {
        Title, Game, Finish
    };

    Mode mode = Mode.Title;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

        switch (mode)
        {
            case Mode.Title:
                Title();
                break;
            case Mode.Game:
                Game();
                break;
            case Mode.Finish:
                Finish();
                RePlay();
                break;

        }

        HandleHandSwitch();
    }

    private void HandleHandSwitch()
    {
        if (mode == Mode.Title) return;  // ゲーム中以外は何もしない
        switch (currentHandState)
        {
            case HandState.Poi:

                poi.SetActive(true);
                hand.SetActive(false);
                rainBow.SetActive(false);

                if (mySlider.value >= mySlider.maxValue)
                {
                    // ここで切り替え前に位置を揃える
                    hand.transform.position = poi.transform.position;
                    handSlider.value = 100;
                    currentHandState = HandState.Hand;
                    Instantiate(feverPrefab, canvasTransform,false);

                }
                break;

            case HandState.Hand:
                poi.SetActive(false);
                hand.SetActive(true);
                rainBow.SetActive(true);


                if (handSlider.value <= handSlider.minValue)
                {

                    // 手を表示する前に位置をポイと揃える
                    poi.transform.position = hand.transform.position;
                    // 手 → ポイ に切り替えるときに即座にゲージを0にする
                    mySlider.value = 0;
                    currentHandState = HandState.Poi;
                }
                break;
        }
    }

    




    public void Title()
    {
        finishTxt.SetActive(false);

        mode = Mode.Game;

    }

    public void Game()
    {
        if (timeManager.countDown <= 0)
        {

            mode = Mode.Finish;

        }

    }

    public void Finish()
    {
        finishTxt.SetActive(true);
        poi.SetActive(false);




    }

    public void RePlay()
    {
        //Zきーが押されていない場合は戻る
        if (!Input.GetKeyDown(KeyCode.Space)) { return; }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        mode = Mode.Title;
    }
}
