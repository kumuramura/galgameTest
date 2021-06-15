using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class LoadScript : MonoBehaviour{
    public static LoadScript instance;
    public Text speakingName;//改变名字颜色
    int index;
    bool SaveCheck = false;
    List<string> txt;//定义一个txt的list
    string[] datas;
    ScriptData currentLog;//记录当前Log
    public AudioSource nowMusic;

    //用此初始化
     void Awake()
    {
        instance = this;
        index = 0;

        string path = "./data";
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

     public void loadscripts(string txtFileName)
    {
        txt = new List<string>();

        TextAsset txtofscript = Resources.Load(txtFileName) as TextAsset;
        datas = txtofscript.text.Split('\n');
        for (int i = 0; i < datas.Length; i++)
        {
            txt.Add(datas[i]);
        }

        index = 0;
    }//加载txt文件

    public ScriptData loadNext()
    {
        if (index < txt.Count)
        //如果剧本还没到底
        {
            string[] datas = txt[index].Split('#');//第index个剧本，用#分开，放入datas数组

            int type = int.Parse(datas[0]);//parse对string强制转换，解析出对话类型

            if (SaveCheck == true)
            {
                SaveCheck = false;
                ChangeNameColor(currentLog.name);
                if(nowMusic.isPlaying==true)
                   {
                      nowMusic.Stop();
                   }
                return currentLog;
            }//保存,忘了savecheck有什么用了
            //改颜色就是用于保存（？后的改颜色
            else if(loadAndSaveInTitle.JumpFromTitle==1)
            {
                loadAndSaveInTitle.JumpFromTitle = 0;
                currentLog = loadAndSaveInTitle.currentLogInTitle;
                index = loadAndSaveInTitle.index;
                ChangeNameColor(currentLog.name);
                
                return currentLog;
            }
            //该部分else if 的作用是
            //如果玩家的从标题读取存档，从而跳转到指定对话
            //那么我们把存档中的index跟log读取出来，返回到当前游戏界面
            else if (type == 0)
            {
                string backpic = datas[1];
                index++;
                //index加一，读取后续对话
                currentLog= new ScriptData(type, backpic);
                
                return currentLog;
            }//0就构造为背景图
            else if(type==1)
            {
                string name = datas[1];
                string log = datas[2];
                string picname = datas[3];
                string backpic = datas[4];
                index++;
                currentLog = new ScriptData(type, name, log, picname, backpic);
                
                ChangeNameColor(currentLog.name);
                return currentLog;
                //为1就构造完整，建议所有对话，统一用1进行构造
            }
            else if(type==2)
            {
                string name = datas[1];
                string log = datas[2];
                string picname = datas[3];
                index++;
                currentLog = new ScriptData(type, name, log, picname);
                
               ChangeNameColor(currentLog.name);
                return currentLog;//为2就无背景图构造
            } //不要使用，此处保留是为了以后有新内容做替代
           
            else if(type==3)
            {
                string option1 = datas[1];//选项1的内容
                string option2 = datas[2];//选项2的内容
                int jump1 = int.Parse(datas[3]);//选了1，后跳多少格
                int jump2 = int.Parse(datas[4]);//选了2，后跳多少格
                string name = datas[5];//老四样
                string log = datas[6];
                string picname = datas[7];
                string backpic = datas[8];

                string[] nextLog1 = txt[index + jump1].Split('#');//跳转的文案
                string[] nextLog2 = txt[index + jump2].Split('#');//跳转的文案2

               ChangeNameColor(currentLog.name);//总感觉这里可以删
                
                //这里选项跳转的类型都为1
                if (GameManager.choosen == 1)
                {
                    index += jump1+1;//根据选项跳到指定位置，还要再+1
                    currentLog = new ScriptData(int.Parse(nextLog1[0]), nextLog1[1], nextLog1[2], nextLog1[3],nextLog1[4]);
                    
                    ChangeNameColor(nextLog1[1]);//再次改一次颜色

                    return new ScriptData(int.Parse(nextLog1[0]), nextLog1[1], nextLog1[2], nextLog1[3],nextLog1[4]);
                }
                else if(GameManager.choosen == 2)
                {
                    index += jump2+1;
                    currentLog = new ScriptData(int.Parse(nextLog2[0]), nextLog2[1], nextLog2[2], nextLog2[3],nextLog2[4]);
                    
                    ChangeNameColor(nextLog1[1]);

                    return new ScriptData(int.Parse(nextLog2[0]), nextLog2[1], nextLog2[2], nextLog2[3],nextLog2[4]);
                }

                currentLog = new ScriptData(type, option1, option2, jump1, jump2,name,log,picname,backpic);
                
                return currentLog;
            }//选项
            else if(type==4)
            {
                string name = datas[1];
                string log = datas[2];
                string picname = datas[3];
               
                index+=int.Parse(datas[4]);
                currentLog = new ScriptData(type, name, log, picname);
                

                ChangeNameColor(currentLog.name);
                return currentLog;
                //为4就构造完整但下一跳会变
                //作用是选项1的对话结束后，跳转到公共对话
                //当然也可以用作对话的其他方式跳转，目前还没用到过
                //这里要保证，在这句话的时候背景不改变！
            }
            else if(type==6)
            {
                
                int music = int.Parse(datas[1]);
                index +=2;

                string[] nextLog = txt[index+1].Split('#');
                //6类型的下一个类型尽量是1类型
                currentLog =new ScriptData(int.Parse(nextLog[0]), nextLog[1], nextLog[2], nextLog[3],nextLog[4]);

                AudioClip clip = Resources.Load<AudioClip>("music/铃声" +music);
               //nowMusic播放
                if(nowMusic.isPlaying==true)
                   {
                      nowMusic.Stop();
                   }
                   nowMusic.clip = clip;
                   nowMusic.Play();

                return currentLog;
            }//返回一个音乐
            else
            {

                return null;
            }//返回空
           
            
            
        }
        else
        {
            return null;
        }
    }
    

    private Save CreateSaveGameObject()
    {
        Save save = new Save();
        save.index = index;
        save.type = currentLog.type;
        save.name = currentLog.name;
        save.log = currentLog.log;
        save.picname = currentLog.picname;
        save.backpic = currentLog.backpic;
        save.option1 = currentLog.option1;
        save.option2 = currentLog.option2;
        save.JumpTo1 = currentLog.JumpTo1;
        save.JumpTo2 = currentLog.JumpTo2;
        save.afterJump = currentLog.afterJump;
        save.SaveTime=DateTime.Now.ToString();

        return save;
    }

    public void SaveGame()
    {
        if (ClickDect.savingmode == 1)
        {
            var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            button.GetComponent<Image>().sprite = Resources.Load("picture/" + "HasSaved", typeof(Sprite)) as Sprite;

            GameObject savelog = GameObject.Find("存档说明" + button.name);
            savelog.GetComponent<Text>().text = "index=" + index + "\r\n" + currentLog.log+ "\r\n"+DateTime.Now.ToString();
            
            Save save = CreateSaveGameObject();
            
            BinaryFormatter bf = new BinaryFormatter();
            
            FileStream file = File.Create("./data"
                + "/gamesave" + button.name + ".save");

            bf.Serialize(file, save);
            file.Close();

            //index = 0;
            //currentLog = null;
            Debug.Log("Game Saved, index is " + index);
        }
    }

    


    public void LoadGame()
    {
        if (ClickDect.savingmode == -1)
        {
            var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            if (File.Exists("./data"
                + "/gamesave" + button.name + ".save"))
            {

                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open("./data"
                + "/gamesave" + button.name + ".save", FileMode.Open);
                Save save = (Save)bf.Deserialize(file);
                file.Close();

                SaveCheck = true;

                index = save.index;
                switch (save.type)
                {
                    case 0:
                        currentLog = new ScriptData(save.type, save.backpic);
                        break;
                    case 1:
                        currentLog = new ScriptData(save.type, save.name, save.log, save.picname, save.backpic);
                        break;
                    case 2:
                        currentLog = new ScriptData(save.type, save.name, save.log, save.picname);
                        break;
                    case 3:
                        currentLog = new ScriptData(save.type, save.option1, save.option2, save.JumpTo1,
                            save.JumpTo2, save.name, save.log, save.picname, save.backpic);
                        break;
                    case 4:
                        currentLog = new ScriptData(save.type, save.name, save.log, save.picname);
                        break;
                    case 5:
                        currentLog = new ScriptData(save.type, save.backpic);
                        break;
                }


                Debug.Log("Game Loaded, index is " + index);


            }
            else
            {
                Debug.Log("No game saved!");
            }
        }
    }

    private void UIvisible(CanvasGroup UiVisual)
    {
        UiVisual.alpha = 1;
        UiVisual.blocksRaycasts = true;
        UiVisual.interactable = true;
    }

    private void UIUnvisible(CanvasGroup UiVisual)
    {
        UiVisual.alpha = 0;
        UiVisual.blocksRaycasts = false;
        UiVisual.interactable = false;
    }

    private void ChangeNameColor(String CharacterName)
    {
        if (CharacterName == "海羽")
                {
                    speakingName.color=new Color32(149,49,135,255);
                }
                else if(CharacterName == "伦")
                {
                    speakingName.color=new Color32(16,58,145,255);
                }
    }
}
