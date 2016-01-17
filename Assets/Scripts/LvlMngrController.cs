using UnityEngine;
using System.Collections;
using UnityStandardAssets.Effects;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LvlMngrController : MonoBehaviour {
    public int reqExplosions = 1, maxPierceAmmo = 1, maxBounceAmmo = 0, maxExplosionAmmo = 0;
    [HideInInspector]
    public int currentPierceAmmo, currentBounceAmmo, currentExplosionAmmo, currentExplosions, currentExplosionTriggers;
    public float timeUntilFadeout = 1;
    public int nextScene=-1;
    public Text scoreText;
    public Text piercingText;
    public Text bouncingText;
    public Text explosiveText;
    public Image uiCursor;

    PlayerController player;
    GameObject winExplosion;
    GameObject loseExplosion;
    AudioPlayer audioPlayer;
    private bool ending = false;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
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
        ManageUICursor();
        if (!ending && currentExplosions >= reqExplosions)
        {
            ending = true;
            if (bulletTotal())
            {
                StartCoroutine(nextLevelDelay());
            }
            else
            {
                StartCoroutine(restartLevelDelay());
            }
        }
        piercingText.text = (maxPierceAmmo - currentPierceAmmo).ToString();
        bouncingText.text = (maxBounceAmmo - currentBounceAmmo).ToString();
        explosiveText.text = (maxExplosionAmmo - currentExplosionAmmo).ToString();
        scoreText.text = currentExplosions.ToString() + "/" + reqExplosions.ToString();
	}

    bool bulletTotal()
    {
        return (currentPierceAmmo<=maxPierceAmmo && currentBounceAmmo <= maxBounceAmmo && currentExplosionAmmo<=maxExplosionAmmo);
    }

    IEnumerator nextLevelDelay()
    {
        float currentTime = 0;
        while (currentTime < timeUntilFadeout)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        nextLevel();
    }

    IEnumerator restartLevelDelay()
    {
        float currentTime = 0;
        while (currentTime < timeUntilFadeout)
        {
            currentTime += Time.deltaTime;
            yield return null;
        }
        restartLevel();
    }

    void nextLevel()
    {
        GameObject explosion = winExplosion;
        explosion.GetComponent<ParticleSystemMultiplier>().multiplier = 20;
        explosion.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        Instantiate(explosion, explosion.transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraShakeScript>().screenShake(1.0f, 1.0f);
        audioPlayer.Play("wow");
        GetComponent<SceneFadeScript>().EndScene(nextScene);
    }

    public void restartLevel()
    {
        GameObject explosion = loseExplosion;
        explosion.GetComponent<ParticleSystemMultiplier>().multiplier = 20;
        explosion.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        Instantiate(explosion, explosion.transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraShakeScript>().screenShake(1.0f, 1.0f);
        audioPlayer.Play("2SED4AIRHORN");
        GetComponent<SceneFadeScript>().EndScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ManageUICursor()
    {
        switch (player.currentBullet)
        {
            case PlayerController.bulletEnum.piercing:
                uiCursor.rectTransform.anchoredPosition = new Vector2(85f, -30f);
                break;
            case PlayerController.bulletEnum.bouncing:
                uiCursor.rectTransform.anchoredPosition = new Vector2(185f, -30f);
                break;
            case PlayerController.bulletEnum.exploding:
                uiCursor.rectTransform.anchoredPosition = new Vector2(285f, -30f);
                break;
        }
    }
}
