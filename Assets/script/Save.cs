using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class Save
{
    public int type;
    public string name;
    public string log;
    public string picname;
    public string backpic;
    public string option1;
    public string option2;
    public int JumpTo1;//选项1跳转
    public int JumpTo2;//选项2跳转
    public int afterJump;//到达选项后再跳转到正确文本行

    public int index = 0;

    public string SaveTime;
}
