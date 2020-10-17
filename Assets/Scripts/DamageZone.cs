using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    // Start is called before the first frame update
    //该脚本是挂载到触发器上的。

    //collision 就是与触发器发生碰撞的对象。就是场景中的ruby对象。

    //private void OnTriggerEnter2D(Collider2D collision)//该函数是继承自父类的方法，参数与返回值、名称、描述都是确定的。
    //{
    //    //Debug.Log("与我们发生碰撞的对象是：" + collision);
    //    RubyController rubyController = collision.GetComponent<RubyController>();
    //    //这是一个类的实例化过程，因为在RubyController定义的时候。是public
    //    //GetComponent 获取自己身上的组件，<>想要获取的组件的类型


    //    // 别的物体碰撞了，其他的对象没有rubyController这个组件，返回none.那下面函数就会报错.会有下面的操作。
    //    if (rubyController != null)//所碰撞的游戏对象没有rubycontroller这个组件，即不是ruby。
    //    {
    //        rubyController.ChangeHealth(-1);
    //        //这里，把类实例化以后，调用对象的成员函数，这个函数是改变这个对象的成员变量的值。
    //        //并没有改变游戏场景中的ruby对象的值
    //    }
    //}

    //实现持续掉血，触发检测的其他几个方法，其他的几个API.
    private void OnTriggerStay2D(Collider2D collision)
    {
        RubyController rubyController = collision.GetComponent<RubyController>();
        //这是一个类的实例化过程，因为在RubyController定义的时候。是public
        //GetComponent 获取自己身上的组件，<>想要获取的组件的类型

        // 别的物体碰撞了，其他的对象没有rubyController这个组件，返回none.那下面函数就会报错.会有下面的操作。
        if (rubyController != null)//所碰撞的游戏对象没有rubycontroller这个组件，即不是ruby。
        {
            rubyController.ChangeHealth(-1);
            //这里，把类实例化以后，调用对象的成员函数，这个函数是改变这个对象的成员变量的值。
            //并没有改变游戏场景中的ruby对象的值
        }

    }

}
