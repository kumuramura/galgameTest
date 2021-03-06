using UnityEngine;
using UnityEngine.Video;


public class PlayMovie : MonoBehaviour
{
    public CanvasGroup titleMovie;
    public VideoPlayer titlePlayer;

    public static float timer = 2.95f;
    void Update()
    {
        if(timer>=0)
        timer -= Time.deltaTime;

        if(timer<=0)
        {
            titleMovie.alpha = 0;
            titleMovie.blocksRaycasts = false;
            titleMovie.interactable = false;
            //titlePlayer.enabled = false;
        }
    }
}
