using UnityEngine;
using System.Collections;
using UnityStandardAssets.Effects;
using UnityEngine.SceneManagement;

public class LvlMngrController : MonoBehaviour {
    public int reqExplosions = 1, maxPierceAmmo = 1, maxBounceAmmo, maxExplosionAmmo;
    [HideInInspector]
    public int currentPierceAmmo, currentBounceAmmo, currentExplosionAmmo, currentExplosions, currentExplosionTriggers;
    public float countDownTime = 3;
    public int nextScene=-1;
    private bool ending = false;
	
	void Update () {
        Debug.Log(currentExplosions);
        if (!ending && currentExplosions >= reqExplosions)
        {
            if (!bulletTotal())
            {
                ending = true;
                restartLevel();
            }
            else
            {
                ending = true;
                nextLevel();
            }
        }
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
