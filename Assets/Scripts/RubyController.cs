using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public int speed = 3;//ruby的速度。

    //ruby生命值
    public int maxHealth = 5;//最大生命值的定义。
    private int currentHealth;//Ruby当前生命值。
    public int Health { get { return currentHealth; } }//属性的概念，只能够在外部访问currentHealth，而不能改变。

    //ruby无敌时间
    public float timeInvincible = 2.0f;//无敌时间长度
    private bool isInvincible;
    private float invincibleTimer;//无敌时间计时器。

    private bool canNotJump;
    private float jumpTimer = 0.5f;//跳跃计时器。

    private Vector2 lookDirection = new Vector2(1, 0);
    private Animator animator;

    public GameObject projectilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        //getcomponent是很重要的方法，获取自己身上的组件，<>想要获取的组件的类型，函数返回值的类型，多态的概念。
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        //Application.targetFrameRate = 10;//改变帧率，改变update函数调用次数。该项目的帧率默认是60次。

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //玩家输入监听，属于unity项目中与键盘方向键的接口;从-1到1变化，可以设置灵敏度，属于阶跃输入。
        float horizontal = Input.GetAxis("Horizontal");//给一个水平方向初速度。
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal,vertical);
        Vector2 forceUp = new Vector2(0, 8000);//跳跃力

        //控制动画
        if (!Mathf.Approximately(move.x,0)||!Mathf.Approximately(move.y, 0))//玩家输入的某个轴向值不为零
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        //控制位移
        //横向
        Vector2 position = transform.position;// this transform attached to this gameObject.
        position.x = position.x + speed * move.x * Time.deltaTime;
        rigidbody2d.MovePosition(position);//通过刚体组件改变位置
        //纵向跳跃
        if (move.y > 0 && !canNotJump)
        {
            canNotJump = true;
            jumpTimer = 1;
            rigidbody2d.AddForce(forceUp);
        }
        //跳跃冷却计时器
        if (canNotJump)
        {
            jumpTimer = jumpTimer - Time.deltaTime;//即每一帧经过的时间。
            if (jumpTimer <= 0)
            {
                canNotJump = false;
            }
        }
        if (isInvincible)
        {
            invincibleTimer = invincibleTimer - Time.deltaTime;//即每一帧经过的时间。
            if (invincibleTimer <= 0)
            {
                isInvincible = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            Launch();
        }
    }
    public void ChangeHealth(int amount)//血量变化的函数，由触发器调用。
    {
        //受到伤害时，才会变为无敌状态
        if (amount < 0)
        {
            if (isInvincible)
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");//触发受击参数。
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }

    private void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, 
            rigidbody2d.position, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
    }
}
