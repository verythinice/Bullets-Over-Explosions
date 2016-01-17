using UnityEngine;
using System.Collections;
using UnityStandardAssets.Effects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlMngrController : MonoBehaviour {
    public int reqExplosions = 1, maxPierceAmmo = 1, maxBounceAmmo, maxExplosionAmmo;
    [HideInInspector]
    public int currentPierceAmmo, currentBounceAmmo, currentExplosionAmmo, currentExplosions, currentExplosionTriggers;
    public float countDownTime = 3;
    public int nextScene=-1;
    public Text scoreText;
    public Text bulletText;
    private bool ending = false;

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

        bulletText.text = (currentPierceAmmo + currentBounceAmmo + currentExplosionAmmo).ToString();
        scoreText.text = currentExplosions.ToString();
	}

    bool bulletTotal()
    {
        return (currentPierceAmmo<=maxPierceAmmo && currentBounceAmmo <= maxBounceAmmo && currentExplosionAmmo<=maxExplosionAmmo);
    }

    void nextLevel()
    {
        GameObject explosion = Resources.Load("Prefabs/LevelEndExplosion") as GameObject;
        explosion.GetComponent<ParticleSystemMultiplier>().multiplier = 20;
        explosion.transform.position = Camera.main.transform.position;
        Instantiate(explosion, explosion.transform.position, Quaternion.identity);
        GetComponent<SceneFadeScript>().EndScene(nextScene);
    }

    void restartLevel()
    {
        GameObject explosion = Resources.Load("Prefabs/LevelLoseExplosion") as GameObject;
        explosion.GetComponent<ParticleSystemMultiplier>().multiplier = 20;
        explosion.transform.position = Camera.main.transform.position;
        Instantiate(explosion, explosion.transform.position, Quaternion.identity);
        GetComponent<SceneFadeScript>().EndScene(SceneManager.GetActiveScene().buildIndex);
    }
}
