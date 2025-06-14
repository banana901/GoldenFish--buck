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
    public TimeManager timeManager;

    private bool isInTrigger = false;
    private Collider2D targetFish = null;

    Animator anim;

    bool isbroken = false;

    [SerializeField] float breakTime = 3f;



    public GameObject timeTextPrefab;     // ← Inspector でプレハブをセット
    public Transform worldCanvas;         // ← World Space Canvas を指定

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
                IFish fish = targetFish.GetComponent<IFish>();
                if (fish != null)
                {
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
        if (other.GetComponent<IFish>() != null)
        {
            if (!fishInRange.Contains(other))
                fishInRange.Add(other);

            targetFish = GetClosestFish(); // 最も近い魚を選ぶ
            isInTrigger = true;
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
