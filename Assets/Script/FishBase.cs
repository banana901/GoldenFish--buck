using UnityEngine;

public abstract class FishBase : MonoBehaviour, IFish
{
    [Header("移動速度")]
    public float speed = 8f;

    protected Vector3 movePosition;
    protected Vector3 deletePosition;

    protected Rigidbody2D rb;
    [Header("キャラの滞在時間")]
    public float countDown = 5.0f;

    public int score = 100;
    public GameObject defeatEffect;

    public Sprite[] moveSprites;//歩く用のスプライトを入れる

    public float MoveAniTime = 0.1f;
    public float MoveAniTimer = 0f;
    int MoveAniSpriteNum = 0;
    public SpriteRenderer spriteRenderer;





    // ===========================  Lifecyle  ===========================
    public virtual void Start()
    {
        movePosition = GetRandomPosition();
        deletePosition = GetRandomDeletePosition();
        rb = GetComponent<Rigidbody2D>();

    }

    public virtual int GetScore()
    {
        return score;
    }

    public virtual void Move()//移動アニメーションを管理
    {
        if (MoveAniTimer >= MoveAniTime)
        {
            MoveAniSpriteNum = (MoveAniSpriteNum + 1) % moveSprites.Length;
            spriteRenderer.sprite = moveSprites[MoveAniSpriteNum];
            MoveAniTimer = 0;
            spriteRenderer.color = Color.white;
            ;
        }
        else
        {
            MoveAniTimer += Time.deltaTime;
        }



    }
    public virtual void OnDefeated()
    {

        ScaleEffect se = GetComponent<ScaleEffect>();

        if (se != null)
        {
            se.enabled = true;
        }

        if (defeatEffect != null)
        {
            Instantiate(defeatEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }


    }

    protected virtual void Update()
    {
        countDown -= Time.deltaTime;

        if (Vector3.Distance(transform.position, movePosition) < 0.01f)
        {
            if (countDown <= 0)
            {
                // 寿命が尽きたらdeletePositionに移動
                movePosition = deletePosition;
            }
            else
            {
                movePosition = GetRandomPosition();
            }
        }

        // deletePositionに到達したらオブジェクト破棄
        if (countDown <= 0 && Vector3.Distance(transform.position, deletePosition) < 0.01f)
        {
            Destroy(gameObject);
        }

        MoveTowardsTarget();
        Move();
    }


    // ===========================  Movement  ===========================
    protected virtual void MoveTowardsTarget()
    {
        transform.position =
        Vector3.MoveTowards(transform.position, movePosition, speed * Time.deltaTime);

        Vector3 dir = movePosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }

    // ===========================  Utility  ===========================
    /// <summary>子クラスで領域やアルゴリズムを変えたいときはここを override</summary>
    protected virtual Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 4f), 0f);
    }

    protected virtual Vector3 GetRandomDeletePosition()
    {
        return new Vector3(Random.Range(-15, 15), Random.Range(-15, 15), 0);


    }
}
