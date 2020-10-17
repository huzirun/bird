using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3;

    private Rigidbody2D rigidbody2d;
    //轴向控制
    public bool vertical;
    //方向控制
    private int direction = 1;
    //方向改变时间间隔，常量
    public float changeTime = 3;
    //计时器
    private float timer;
    //动画控制器
    private Animator animator;

    private bool broken;//机器人是否故障

    private ParticleSystem smokeEffect;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        //animator.SetFloat("MoveX", direction);
        //animator.SetBool("Vertical", vertical);
        PlayMoveAnimation();
        broken = true;

        smokeEffect = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken)
        {
            //不会移动
            return;
        }
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            //animator.SetFloat("MoveX",direction);
            timer = changeTime;
            PlayMoveAnimation();
        }
        //Vector2 position = transform.position;
        Vector2 position = rigidbody2d.position;

        if (vertical)//垂直轴向
        {
            position.y = position.y + Time.deltaTime * speed * direction;
        }
        else//水平轴向
        {
            position.x = position.x + Time.deltaTime * speed * direction;
        }

        rigidbody2d.MovePosition(position);
    }
    //触发检测
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController rubyController = collision.gameObject.GetComponent<RubyController>();//注意这里跟trigger的不太相同。
        if (rubyController != null)
        {
            rubyController.ChangeHealth(-1);
        }
    }
    //控制移动动画的方法
    private void PlayMoveAnimation()
    {
        if (vertical)//垂直轴向控制
        {
            animator.SetFloat("MoveX", 0);
            animator.SetFloat("MoveY", direction);
        }
        else//水平轴向的控制
        {
            animator.SetFloat("MoveX", direction);
            animator.SetFloat("MoveY", 0);
        }

    }

    public void Fix()
    {
        broken = false;
        rigidbody2d.simulated = false;//不会碰撞检测
        animator.SetTrigger("Fixed");

        //停止烟雾效果
        smokeEffect.Stop();
    }
}
