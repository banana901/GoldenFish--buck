using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poi : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float poiSpeed;

    public GameObject defeatFish;
    
    public GameObject defeDatu;

    public int destoroyCount = 0;

    public ScoreManager scoreManager; // ←スコアマネージャーをここに入れる

    private bool isInTrigger = false;
    private Collider2D targetFish = null;

    Animator anim;

    bool isbroken = false;

    [SerializeField] float breakTime = 3f;

  

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();





    }

    // Update is called once per frame
    void Update()
    {
        if (isbroken)
        {
            breakTime -= Time.deltaTime;
            if (breakTime <= 0f)
            {
                isbroken = false;
                anim.SetBool("Break", false);
                breakTime = 1f; // タイマーを戻す
            }
            

        }
        Move();


        if (!isbroken && Input.GetKeyDown(KeyCode.Space))
        {



            if (isInTrigger && targetFish != null)
            {
                if (targetFish.CompareTag("Fish"))
                {
                    Destroy(targetFish.gameObject);
                    Instantiate(defeatFish, transform.position, Quaternion.identity);
                    scoreManager.score_num += 100;
                }
                else if (targetFish.CompareTag("Datu"))
                {
                    Destroy(targetFish.gameObject);
                    Instantiate(defeDatu, transform.position, Quaternion.identity);
                    scoreManager.score_num += 1000;
                }
                else if (targetFish.CompareTag("GoldenGod"))
                {
                    Destroy(targetFish.gameObject);
                    Instantiate(defeDatu, transform.position, Quaternion.identity);
                    scoreManager.score_num += 5000;
                }
                else if (targetFish.CompareTag("Namazu"))
                {
                     ScaleEffect se = targetFish.GetComponent<ScaleEffect>();

                    se.enabled = true;
                   
                   
                    
                    scoreManager.score_num += 800;
                }
            }
            else
            {
                isbroken = true;
                anim.SetBool("Break", true);


            }



        }
        



    }

    void Move()
    {
        // 矢印キー入力
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");
        //　移動方向決定
        Vector2 inputDirection = new Vector2(MoveX, MoveY).normalized;
        //　移動
        rb.velocity = new Vector2(inputDirection.x * poiSpeed, inputDirection.y * poiSpeed);


    }

    void OnTriggerStay2D(Collider2D other)
    {
       
            isInTrigger = true;
            targetFish = other;
        

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isInTrigger = false;
        targetFish = null;

    }

}
