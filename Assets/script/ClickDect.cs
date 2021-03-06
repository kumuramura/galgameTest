
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ClickDect : MonoBehaviour
{
    public CanvasGroup menu;
    public CanvasGroup setting;//设置界面
    public CanvasGroup savingPage;//存档界面
    public CanvasGroup testRecords;//文字记录

    public static int savingmode = 0;
    public Text autoColor;
    public Toggle r18;
    
    private int autosituation = 0;//0为手动，1为自动

    public void Awake()
    {
        
        if (!PlayerPrefs.HasKey("r18"))
        {

            PlayerPrefs.SetInt("r18", 1);
            r18.isOn = true;
           //r18启动
            PlayerPrefs.Save();
        }
        else
        {
           if(PlayerPrefs.GetInt("r18")==0)
            {
                //r18关闭
                r18.isOn = false;
            }
           else
            {
                //r18启动
                r18.isOn = true;
            }

        }
    }



    void Update()
    {
        if (Input.GetMouseButtonDown(1) && menu.alpha == 0)
        {
            menuOn();
        }
        
        else if (Input.GetMouseButtonDown(1) && menu.alpha == 1&&setting.alpha==0&&savingPage.alpha==0)
        {
            menuOff();
        }
       
    }


    public void menuOn()
    {
        UIvisible(menu);    
    }

    public void menuOff()
    {
        UIUnvisible(menu);
    }

    public void settingOn()
    {
        UIvisible(setting);
    }

    public void settingOff()
    {
        UIUnvisible(setting);
    }

    public void exitToTitle()
    {
        SceneManager.LoadScene(0);
    }

    public void savingpageUp()
    {
        UIvisible(savingPage);
        savingmode = 1;
    }

    public void savingpageDown()
    {
        UIUnvisible(savingPage);
        savingmode = 0;
    }

    public void loadingpageUp()
    {
        UIvisible(savingPage);
        savingmode = -1;
    }

    public void TextRecordUp()
    {
        UIvisible(testRecords);
    }
    public void TextRecordDown()
    {
        UIUnvisible(testRecords);
    }

    public void auto()
    {
        if(autosituation == 0)
        {
            autoColor.color = new Color32(80, 130, 230, 250);
            autosituation = 1;
            
            //开启自动模式
        }
        else if (autosituation == 1)
        {
            autoColor.color = new Color32(144, 135, 135, 250);
            autosituation = 0;
            
            //关闭自动模式
        }
    }

    public void ToggleR18()
    {
        if(r18.isOn)
        {
            PlayerPrefs.SetInt("r18", 1);
            //r18启动
        }
        else
        {
            PlayerPrefs.SetInt("r18", 0);
            //r18关闭
        }
        PlayerPrefs.Save();
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


}
