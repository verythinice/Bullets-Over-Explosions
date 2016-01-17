using UnityEngine;
using System.Collections;
using UnityStandardAssets.Effects;

public class LvlMngrController : MonoBehaviour {
    public int currentExplosionTriggers = 0;
    public int maxPierceAmmo = 1, maxBounceAmmo, maxExplosionAmmo;
    public int currentPierceAmmo = 1, currentBounceAmmo, currentExplosionAmmo, reqExplosions = 1, currentExplosions;
    public float countDownTime = 3;
    public int nextScene=0;
    private bool ending = false;
	
	void Update () {
        Debug.Log(currentExplosions);
        if (!ending && currentExplosions >= reqExplosions)
        {
            ending = true;
            nextLevel();
        }
        else if (bulletTotal() == 0 && currentExplosionTriggers == 0 && currentExplosions < reqExplosions)
        {           
			Invoke ("restartLevel", 1f);
            //restartLevel();
        }
	}

    int bulletTotal()
    {
        return currentPierceAmmo + currentBounceAmmo + currentExplosionAmmo;
    }

    void nextLevel()
    {
        GameObject explosion = Resources.Load("Prefabs/LevelEndExplosion") as GameObject;
        explosion.GetComponent<ParticleSystemMultiplier>().multiplier = 20;
        explosion.transform.position = new Vector3(0, 0, 0);
        Instantiate(explosion, explosion.transform.position, Quaternion.identity);
        GetComponent<SceneFadeScript>().EndScene(nextScene);
    }

    void restartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
