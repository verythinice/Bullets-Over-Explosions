using UnityEngine;
using System.Collections;

public class LvlMngrController : MonoBehaviour {
    public int currentExplosionTriggers = 0;
    public int maxPierceAmmo = 1, maxBounceAmmo, maxExplosionAmmo;
    public int currentPierceAmmo = 1, currentBounceAmmo, currentExplosionAmmo, reqExplosions = 1, currentExplosions;	
	
	void Update () {
        Debug.Log(currentExplosions);
        if (currentExplosions >= reqExplosions)
        {
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
        Application.LoadLevel(Application.loadedLevel + 1);
    }

    void restartLevel()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
