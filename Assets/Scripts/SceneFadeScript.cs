using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneFadeScript : MonoBehaviour
{
    public Image FadeImg;
    public Image LevelStartImg;
    public float fadeSpeed = 1.5f;
    public float timeUntilStart = 1.0f;
    private int SceneNumber;
    private bool sceneStart = true;
    private float startTime=0;
    private bool fading;
    private PlayerController player;
    //public bool sceneStarting = true;


    void Awake()
    {
        LevelStartImg.color = Color.white;
        FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
        FadeImg.color = new Color(0,0,0,0);
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        player.enabled = false;
    }

    void FixedUpdate()
    {
        if (sceneStart)
        {
            if(startTime<= timeUntilStart)
            {
                startTime += Time.deltaTime;
            }
            else
            {
                sceneStart = false;
                LevelStartImg.color = Color.clear;
                player.enabled = true;
            }
        }

        if (fading)
        {
            FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
        }

        // If the screen is almost black...
        if (FadeImg.color.a >= .99f)
            // ... reload the level
            SceneManager.LoadScene(SceneNumber);
    }

    void FadeToBlack()
    {
        // Lerp the colour of the image between itself and black.
        //FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeSpeed * Time.deltaTime);
        fading = true;
    }


    //void StartScene()
    //{
    //    // Fade the texture to clear.
    //    FadeToClear();

    //    // If the texture is almost clear...
    //    if (FadeImg.color.a <= 0.05f)
    //    {
    //        // ... set the colour to clear and disable the RawImage.
    //        FadeImg.color = Color.clear;
    //        FadeImg.enabled = false;

    //        // The scene is no longer starting.
    //        sceneStarting = false;
    //    }
    //}


    public void EndScene(int SceneNumber)
    {
        // Make sure the RawImage is enabled.
        FadeImg.enabled = true;

        // Start fading towards black.
        FadeToBlack();
        if (SceneNumber == -1)
        {
            this.SceneNumber = SceneManager.GetActiveScene().buildIndex + 1;
        }
        else
        {
            this.SceneNumber = SceneNumber;
        }
    }
}
