using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;

    private GameObject hitPrarticlePrefab;
    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        hitPrarticlePrefab = Resources.Load<GameObject>("HitParticle");//这是获取Resources 文件夹下的文件对象 ，没有有就创建一个.
    }

    // 不需要用update方法，只需要施加一次力。公开成员，由ruby进行调用。
    public void Launch(Vector2 direction, float force)//力的方向与大小
    {
        rigidbody2d.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log(collision.gameObject.name);
        EnemyController enemyController = collision.gameObject.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.Fix();
        }
        GameObject hitParticleObject =  Instantiate(hitPrarticlePrefab, rigidbody2d.position, Quaternion.identity);
        Destroy(hitParticleObject, 1);//延时销毁特效。 
        Destroy(gameObject);
    }

    private void Update()
    {
        if (transform.position.magnitude > 50)
        {
            Destroy(gameObject);
        }
    }
}
