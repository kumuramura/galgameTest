public class ScriptData
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
    public int music;//在此处使用什么短铃声
 

    public ScriptData(int type,string backpic)
    {
        this.type = type;
        this.backpic = backpic;
    }//设定单个背景图的构造函数，类型为0

    public ScriptData(int type,string name,string log,string picname, string backpic)
    {
        this.type = type;
        this.name = name;
        this.log = log;
        this.picname = picname;
        this.backpic = backpic;
    }//新场景下的对话构造函数，类型为1

    public ScriptData(int type, string name, string log, string picname)
    {
        this.type = type;
        this.name = name;
        this.log = log;
        this.picname = picname;
    }//旧场景下的对话构造函数，类型为2，该类型为选项跳转和音乐的限定数据，选项跳转和音乐跳转只能跳到该类型上
    //因为玩家可能在该处保存，弃用该类型，通用1

    public ScriptData(int type, string option1, string option2,int jump1,int jump2,string name,string log,
        string picname,string backpic)
    {
        this.type = type;
        this.option1 = option1;
        this.option2 = option2;
        JumpTo1 = jump1;
        JumpTo2 = jump2;

        //新加内容
        this.name = name;
        this.log = log;
        this.picname = picname;
        this.backpic = backpic;

    }//设定处于选择时的函数，类型为3

    public ScriptData(int type, string name, string log, string picname,int afterJump)
    {
        this.type = type;
        this.name = name;
        this.log = log;
        this.picname = picname;
        this.afterJump = afterJump;
    }//选择本文的最后一条，场景下的对话构造函数，类型为4

    public ScriptData(int type, string backpic,string log)
    {
        this.type = type;
        this.log = log;
        this.backpic = backpic;
    }//设定单个背景图的构造函数，类型为5
    //有bug，弃用

    public ScriptData(int type, int music)
    {
        this.type = type;       
        this.music = music;
    }//设定有音乐的构造函数，类型为6
    //在读到此类型的数据后，文本index+2，跳过该数据的文本的读取
    //music以三位数的方式命名，再用tostring变成string索取
    //后续会开发反向读取该对话的bgm，通过index--；


}
