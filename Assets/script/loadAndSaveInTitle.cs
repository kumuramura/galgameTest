using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;
public class loadAndSaveInTitle : MonoBehaviour
{

    public static int index =0;
    public static ScriptData currentLogInTitle;
    public static int JumpFromTitle = 0;
     void Start()
    {
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

                GameObject savePic = GameObject.Find(i.ToString());
                savePic.GetComponent<Image>().sprite = Resources.Load("picture/" + "HasSaved", typeof(Sprite)) as Sprite;

                GameObject savelog = GameObject.Find("存档说明" + i);
                savelog.GetComponent<Text>().text = "index=" + save.index + "\r\n" + save.log+ "\r\n" + save.SaveTime;               
            }
        }
        
    }

    void Update()
    {
        LoadGame();
    }

    public void LoadGame()
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

               
                index = save.index;
                switch (save.type)
                {
                    case 0:
                    currentLogInTitle = new ScriptData(save.type, save.backpic);
                        break;
                    case 1:
                    currentLogInTitle = new ScriptData(save.type, save.name, save.log, save.picname, save.backpic);
                        break;
                    case 2:
                    currentLogInTitle = new ScriptData(save.type, save.name, save.log, save.picname);
                        break;
                    case 3:
                    currentLogInTitle = new ScriptData(save.type, save.option1, save.option2, save.JumpTo1,
                            save.JumpTo2, save.name, save.log, save.picname, save.backpic);
                        break;
                    case 4:
                    currentLogInTitle = new ScriptData(save.type, save.name, save.log, save.picname);
                        break;
                    case 5:
                    currentLogInTitle = new ScriptData(save.type, save.backpic);
                        break;
                }


                Debug.Log("Game Loaded, index is " + index);
                JumpFromTitle = 1;
                SceneManager.LoadScene(1);


            }
            else
            {
                Debug.Log("No game saved!");
            }
    }


    public void setImage(Image image, string picName)
    {
        image.sprite = Resources.Load("picture/demo/" + picName, typeof(Sprite)) as Sprite;
    }//设置角色图片和背景图片的函数

    public void setText(Text text, string content)
    {
        text.text = content;
    }
}
