using UnityEngine;
using System.Collections;
using UnityStandardAssets.Effects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlMngrController : MonoBehaviour {
    public int reqExplosions = 1, maxPierceAmmo = 1, maxBounceAmmo = 0, maxExplosionAmmo = 0;
    [HideInInspector]
    public int currentPierceAmmo, currentBounceAmmo, currentExplosionAmmo, currentExplosions, currentExplosionTriggers;
    public float countDownTime = 3;
    public int nextScene=-1;
    public Text scoreText;
    public Text piercingText;
    public Text bouncingText;
    public Text explosiveText;
    public Image uiCursor;

    GameObject winExplosion;
    GameObject loseExplosion;
    AudioPlayer audioPlayer;
    private bool ending = false;

    void Awake()
    {
        winExplosion = Resources.Load("Prefabs/LevelEndExplosion") as GameObject;
        loseExplosion = Resources.Load("Prefabs/LevelLoseExplosion") as GameObject;
        audioPlayer = GetComponent<AudioPlayer>();
    }

    void Start()
    {
        currentPierceAmmo = 0;
        currentBounceAmmo = 0;
        currentExplosionAmmo = 0;
    }
	
	void Update () {        
        if (!ending && currentExplosions >= reqExplosions)
        {
            ending = true;
            if (bulletTotal())
            {
                nextLevel();
            }
            else
            {
                restartLevel();
            }
        }
        piercingText.text = (maxPierceAmmo - currentPierceAmmo).ToString();
        bouncingText.text = (maxBounceAmmo - currentBounceAmmo).ToString();
        explosiveText.text = (maxExplosionAmmo - currentExplosionAmmo).ToString();
        scoreText.text = currentExplosions.ToString();
	}

    bool bulletTotal()
    {
        return (currentPierceAmmo<=maxPierceAmmo && currentBounceAmmo <= maxBounceAmmo && currentExplosionAmmo<=maxExplosionAmmo);
    }

    void nextLevel()
    {
        GameObject explosion = winExplosion;
        explosion.GetComponent<ParticleSystemMultiplier>().multiplier = 20;
        explosion.transform.position = Camera.main.transform.position;
        Instantiate(explosion, explosion.transform.position, Quaternion.identity);
        audioPlayer.Play("wow");
        GetComponent<SceneFadeScript>().EndScene(nextScene);
    }

    public void restartLevel()
    {
        GameObject explosion = loseExplosion;
        explosion.GetComponent<ParticleSystemMultiplier>().multiplier = 20;
        explosion.transform.position = Camera.main.transform.position;
        Instantiate(explosion, explosion.transform.position, Quaternion.identity);
        audioPlayer.Play("2SED4AIRHORN");
        GetComponent<SceneFadeScript>().EndScene(SceneManager.GetActiveScene().buildIndex);
    }
}
