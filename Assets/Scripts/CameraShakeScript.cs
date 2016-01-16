using UnityEngine;
using System.Collections;

public class CameraShakeScript : MonoBehaviour
{

    // How long the object should shake for.
    public float shake = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public Vector3 originalPos;

    private bool shakeStart;

    void Awake()
    {

    }

    void OnEnable()
    {
        originalPos = Camera.main.transform.localPosition;
    }

    void Update()
    {
        //if shakestart then set original and start shaking
        if (shake > 0 && !shakeStart)
        {
            shakeStart = true;
        }
        //if shake has started and still shake left
        else if (shake > 0 && shakeStart)
        {
            Camera.main.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shake -= Time.deltaTime * decreaseFactor;
        }
        else if (shake <= 0 && shakeStart)
        {
            shake = 0f;
            Camera.main.transform.localPosition = originalPos;
        }
        else
        {
            Camera.main.transform.localPosition = originalPos;
        }
    }

    //AND WELCOME TO THE JAM
    public void screenShake(float shakeAmount, float shake)
    {
        this.shake = shake;
        this.shakeAmount = shakeAmount;
    }
}
