using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Apple.ReplayKit;


public class Main : MonoBehaviour
{
    public TimeManager timeManager;
    public GameObject finishTxt;
    public GameObject poi;



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

    }

    public void Title()
    {
        finishTxt.SetActive(false);
        poi.SetActive(true);
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
