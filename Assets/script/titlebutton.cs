using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class titlebutton : MonoBehaviour
{

    public CanvasGroup cgbackground;
    public CanvasGroup cg;
    public CanvasGroup files;

    public Image Fullcg;
  
    
    private bool filesIsOpen = false;

    void Update()
    {
        if(filesIsOpen==true)
        {
            if(Input.GetMouseButtonDown(1))
            {
                files.alpha = 0;
                files.interactable = false;
                files.blocksRaycasts = false;
                filesIsOpen = false;
            }
        }
        
        
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void loadGame()
    {
        files.alpha = 1;
        files.interactable = true;
        files.blocksRaycasts = true;
        filesIsOpen = true;
    }

    public void  CloseLoad()
    {
        files.alpha = 0;
        files.interactable = false;
        files.blocksRaycasts = false;
        filesIsOpen = false;
    }

    public void openCGmode()
    {
        cgbackground.alpha = 1;
        cgbackground.interactable = true;
        cgbackground.blocksRaycasts = true;
        
    }

    public void closeCGmode()
    {
        cgbackground.alpha = 0;
        cgbackground.interactable = false;
        cgbackground.blocksRaycasts = false;
    }

    public void openCG()
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
        var button = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;

        
        Fullcg.sprite = Resources.Load("picture/"+button.name, typeof(Sprite)) as Sprite;
      
        //print(button.name);
    }

    public void closeCG()
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }

    public void exitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
