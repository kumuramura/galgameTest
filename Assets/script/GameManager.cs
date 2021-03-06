using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManager : MonoBehaviour
{
    public Text names;
    public Text talk;
    public Image background;
    public Image character;
    public Text choose1;
    public Text choose2;

    public Button btn1;
    public Button btn2;

    public CanvasGroup TalkingLogs;
    public CanvasGroup characterPic;
    public CanvasGroup xuan1;//选项的canvas

    public static int choosen = 0;

    public static int times = 1;

    
    public float charsPerSecond = 0.03f;//打字时间间隔
    private string words;//保存需要显示的文字

    private bool isActive = false;
    private float timer;//计时器

    private int currentPos = 0;//当前打字位置


    void Start()
    {
        LoadScript.instance.loadscripts("demo1");
        handleData(LoadScript.instance.loadNext());
        for (int i = 1; i <= 30; i++)
        {
            if (File.Exists("./data"
                + "/gamesave" + i + ".save"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open("./data"
                + "/gamesave" + i + ".save", FileMode.Open);
                Save save = (Save)bf.Deserialize(file);
                file.Close();

                GameObject savelog = GameObject.Find("存档说明" + i);
                savelog.GetComponent<Text>().text = "index=" + save.index + "\r\n" + save.log;

                GameObject savePic = GameObject.Find(i.ToString());
                savePic.GetComponent<Image>().sprite = Resources.Load("picture/" + "HasSaved", typeof(Sprite)) as Sprite;
            }
        }


        //字体逐个显示的准备工作
         timer = 0;
        isActive = true;

    }

    public void leftClick()
    {
        handleData(LoadScript.instance.loadNext());
    }

    public void MoreleftClick()
    {
        if(ClickDect.savingmode==-1)
        handleData(LoadScript.instance.loadNext());
    }//给读档用的

    public void chooseA()
    {
        choosen = 1;//选第一个       
        handleData(LoadScript.instance.loadNext());      
        choosen = 0;
    }

    public void chooseB()
    {
       choosen = 2;//选第二个
        handleData(LoadScript.instance.loadNext());
        choosen = 0;

    }

    public void setImage(Image image, string picName)
    {
        image.sprite = Resources.Load("picture/demo/" + picName, typeof(Sprite)) as Sprite;
    }//设置角色图片和背景图片的函数

    public void setText(Text text, string content)
    {
       
        text.text = content;
        
    }

    

    public void handleData(ScriptData data)
    {
        if (data == null)
            return;
        if (data.type==0)
        {
            setImage(background, data.backpic);
           
            UIUnvisible(TalkingLogs);
            UIUnvisible(characterPic);
            UIUnvisible(xuan1);
            
            //handleData(LoadScript.instance.loadNext()); ;
        }
        else if(data.type == 1)
        {
            UIvisible(TalkingLogs);
            UIvisible(characterPic);
            UIUnvisible(xuan1);
          
            setImage(background, data.backpic);
            setImage(character, data.picname);
            setText(names, data.name);
            setText(talk, data.log);
         
            
        }
        else if (data.type == 2|| data.type == 4)
        {
            UIvisible(TalkingLogs);
            UIvisible(characterPic);
            UIUnvisible(xuan1);          
            setImage(character, data.picname);
            setText(names, data.name);
            setText(talk, data.log);
            times = 1;


        }
        else if (data.type == 3)
        {
            setText(choose1, data.option1);
            setText(choose2, data.option2);
            setImage(background, data.backpic);
            setImage(character, data.picname);
            setText(names, data.name);
            setText(talk, data.log);
            UIvisible(xuan1);
           
        }
       

    }//用于设置数据


    public void UIvisible(CanvasGroup UiVisual)
    {
        UiVisual.alpha = 1;
        UiVisual.blocksRaycasts = true;
        UiVisual.interactable = true;
    }

    public void UIUnvisible(CanvasGroup UiVisual)
    {
        UiVisual.alpha = 0;
        UiVisual.blocksRaycasts = false;
        UiVisual.interactable = false;
    }



}  
