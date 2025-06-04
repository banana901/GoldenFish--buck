using UnityEngine;

public abstract class FishBase : MonoBehaviour
{
    [Header("移動d速度")]
    public float speed = 8f;

    protected Vector3 movePosition;
    protected Vector3 deletePosition;

    protected Rigidbody2D rb;
    public float countDown = 5.0f;

    // ===========================  Lifecyle  ===========================
    public virtual void Start()
    {
        movePosition = GetRandomPosition();
        deletePosition = GetRandomDeletePosition();
        rb = GetComponent<Rigidbody2D>();
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
