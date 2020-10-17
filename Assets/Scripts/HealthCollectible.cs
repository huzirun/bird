using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour//继承的概念，是派生类，所以会有继承的成员函数
{
    // Start is called before the first frame update
    //该脚本是挂载到触发器上的。
    public GameObject pickupParticlePrefab;//unity界面传入。

    //collision 就是与触发器发生碰撞的对象。就是场景中的ruby对象。
    private void OnTriggerEnter2D(Collider2D collision)//是继承自父类的方法，参数与返回值、名称、描述都是确定的。
    {
        RubyController rubyController = collision.GetComponent<RubyController>();
        //类的实例化过程
        //GetComponent 获取自己身上的组件，<>想要获取的组件的类型

        // 别的物体碰撞了，其他的对象没有rubyController这个组件，返回none.那下面函数就会报错.会有下面的操作。
        if(rubyController != null)//所碰撞的游戏对象没有rubycontroller这个组件，即不是ruby。
        {
                //rubyController.ChangeHealth(1);后续的接口。
                //这里，把类实例化以后，调用对象的成员函数，这个函数是改变这个对象的成员变量的值。
                //好像是能够直接改变场景中那个ruby对象的值。
                GameObject pickupParticleObject= Instantiate(pickupParticlePrefab,
                    transform.position, Quaternion.identity);//激活特效。
                
                //将场景中的对象销毁。
                Destroy(pickupParticleObject, 1);//延时销毁
                Destroy(gameObject);//这个就指代该组件所挂载的对象。
        }
    }
}
