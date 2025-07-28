using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poi : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float poiSpeed;

    public GameObject defeatFish;

    public GameObject defeDatu;
     public GameObject hand;


    public int destoroyCount = 0;

    public ScoreManager scoreManager;
    public TimeManager timeManager;

    private bool isInTrigger = false;
    private Collider2D targetFish = null;

    Animator anim;

    bool isbroken = false;

    [SerializeField] float breakTime = 3f;



    public Slider mySlider;



    public GameObject timeTextPrefab;
    public Transform worldCanvas;

    public List<Collider2D> fishInRange = new List<Collider2D>();





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();





    }

    // Update is called once per frame
    void Update()
    {

        // タッチされているかチェック

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
        KeyMove();
        //MouseMove();


        if (!isbroken && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
          
            if (isInTrigger && targetFish != null)
            {
                IFish fish = targetFish.GetComponent<IFish>();
                if (fish != null)
                {
                    if (mySlider != null)
                    {
                        IncreaseSlider(5f);

                    }

                    timeManager.AddTime(fish.GetTime());
                    // 時間表示テキストを生成
                    GameObject textObj = Instantiate(timeTextPrefab, worldCanvas);
                    // 魚の少し上に表示（例：Y軸1.5上）
                    textObj.transform.position = targetFish.transform.position + new Vector3(0, 0.5f, 0);
                    fish.OnDefeated();
                    scoreManager.AddScore(fish.GetScore());





                }
            }
            else
            {
                isbroken = true;
                anim.SetBool("Break", true);


            }



        }
       
      

       




    }

    

    void KeyMove()
    {
        // 矢印キー入力
        float MoveX = Input.GetAxisRaw("Horizontal");
        float MoveY = Input.GetAxisRaw("Vertical");
        //　移動方向決定
        Vector2 inputDirection = new Vector2(MoveX, MoveY).normalized;
        //　移動
        rb.velocity = new Vector2(inputDirection.x * poiSpeed, inputDirection.y * poiSpeed);
    }

    void MouseMove()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        Vector3 target = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 target2D = new Vector2(target.x, target.y);
        rb.MovePosition(target2D); // Rigidbodyの方法で移動

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.GetComponent<IFish>() != null)
        {

            if (!fishInRange.Contains(other))
                fishInRange.Add(other);

            targetFish = GetClosestFish(); // 最も近い魚を選ぶ
            isInTrigger = true;

        }
    }

    void IncreaseSlider(float amount)
    {
        mySlider.value += amount;

        // 最大値を超えないように制限（任意）
        if (mySlider.value > mySlider.maxValue)
        {
            mySlider.value = mySlider.maxValue;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (fishInRange.Contains(other))
            fishInRange.Remove(other);

        if (fishInRange.Count == 0)
        {
            isInTrigger = false;
            targetFish = null;
        }
        else
        {
            targetFish = GetClosestFish();
        }
    }

    Collider2D GetClosestFish()
    {
        Collider2D closest = null;
        float minDist = float.MaxValue;

        foreach (var fish in fishInRange)
        {
            float dist = Vector2.Distance(transform.position, fish.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = fish;
            }
        }

        return closest;
    }

}
