using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        Application.LoadLevel(1);
    }
	
}
