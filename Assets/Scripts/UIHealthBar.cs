using System.Collections;
using System.Collections.Generic;
using UnityEngine;//命名空间，相当于引入包的作用，加入这一行代码就可以使用unity中的一些类了。
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Image mask;
    //这里添加的是convas父对象中血条下的image_mask子对象中的image组件。
    //该组件是是UI中image对象固有的。
    float OriginalSize;//width的原尺寸。

    // Start is called before the first frame update
    void Start()
    {
        OriginalSize = mask.rectTransform.rect.width;//取到width的值。//怎么赋值？
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //设置当前UI血条的显示值。【该方法是要在别的脚本中调用的，所以需要公开】
    public void SetValue(float fillPercent)
    {
        //直接改变游戏场景中对象的组件中的参数，一定要用专门的函数，
        //不能直接把接受width的originalsize变量改变，
        //能不能直接改变mask.rectTransform.rect.width给其赋值？
        mask.rectTransform.SetSizeWithCurrentAnchors(
            RectTransform.Axis.Horizontal, OriginalSize * fillPercent);
    }
}
