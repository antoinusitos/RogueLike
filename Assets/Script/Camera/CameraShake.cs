using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

    public static CameraShake Instance;

    private float tick;
    private float endShake;
    private bool isShaking = false;
    private Vector3 amt;
    private Vector3 posAddShake;
    private Vector3 rotAddShake;
    private float mag;
    private float rough;
    private bool stopable;

    [Header("Tweak value")]
    public Vector3 positionInfluence;
    public Vector3 rotationInfluence;

    // Use this for initialization
    void Awake () {
        // First we check if there are any other instances conflicting
        if (Instance != null && Instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }

        // Here we save our singleton instance
        Instance = this;

    }
	
	// Update is called once per frame
	void Update () {
        posAddShake = Vector3.zero;
        rotAddShake = Vector3.zero;

        Debug.Log("NE PAS OUBLIER DE SUP la touche A pour shake");
        if (Input.GetKeyUp(KeyCode.A))
        {
            initShake(Time.timeScale, 10f, 0.6f, GameObject.FindGameObjectWithTag("Player").transform.forward, new Vector3(0.6f, 0.6f, 0.6f));
            //initShake(1f, 10f, 0.1f, GameObject.FindGameObjectWithTag("Player").transform.forward, new Vector3(0.6f, 0.6f, 0.6f));
        }

        if (endShake >= 0 && isShaking == true)
        {
            posAddShake += this.multiplyVector3(UpdateShake(mag, rough), positionInfluence);
            rotAddShake += this.multiplyVector3(UpdateShake(mag, rough), positionInfluence); 
            endShake -= Time.unscaledDeltaTime;
        } else
        {
            isShaking = false;
        }

        transform.localPosition = posAddShake;
        transform.localEulerAngles = rotAddShake;
    }

    Vector3 UpdateShake(float magnitude, float roughness)
    {
        amt.x = Mathf.PerlinNoise(tick, 0) - 0.5f;
        amt.y = Mathf.PerlinNoise(0, tick) - 0.5f;
        amt.z = Mathf.PerlinNoise(tick, tick) - 0.5f;
        tick += Time.unscaledDeltaTime * roughness;
        return amt * magnitude;
    }

    Vector3 multiplyVector3(Vector3 a, Vector3 b)
    {
        a.x *= b.x;
        a.y *= b.y;
        a.z *= b.z;

        return a;
    }

    /// <summary>
    /// Shake shake with magnitude end roudhness by default this method shake during 1 second
    /// </summary>
    /// <param name="magnitude"></param>
    /// <param name="roughness"></param>
    public void initShake(float magnitude, float roughness) {

        if (isShaking == true) return;

        this.mag = magnitude;
        this.rough = roughness;
        tick = Random.Range(-100, 100);
        endShake = 1f;
        isShaking = true;
    }

    /// <summary>
    /// Skake during fixed time.
    /// </summary>
    /// <param name="magnitude"></param>
    /// <param name="roughness"></param>
    /// <param name="timeShake"></param>
    public void initShake(float magnitude, float roughness, float timeShake)
    {
        if (isShaking == true) return;

        this.mag = magnitude;
        this.rough = roughness;
        tick = Random.Range(-100, 100);
        endShake = timeShake;
        isShaking = true;
    }

    /// <summary>
    /// Shake with a specific influence rotation and influence position
    /// </summary>
    /// <param name="magnitude"></param>
    /// <param name="roughness"></param>
    /// <param name="timeShake"></param>
    /// <param name="influencePos"></param>
    /// <param name="influenseRot"></param>
    public void initShake(float magnitude, float roughness, float timeShake, Vector3 influencePos, 
        Vector3 influenceRot)
    {
        if (isShaking == true) return;

        this.mag = magnitude;
        this.rough = roughness;
        this.positionInfluence = influencePos;
        this.rotationInfluence = influenceRot;
        tick = Random.Range(-100, 100);
        endShake = timeShake;
        isShaking = true;
    }

    /// <summary>
    /// Shake with a specific influence rotation and influence position
    /// </summary>
    /// <param name="magnitude"></param>
    /// <param name="roughness"></param>
    /// <param name="timeShake"></param>
    /// <param name="influencePos"></param>
    /// <param name="influenseRot"></param>
    public void initShakeStopable(float magnitude, float roughness, float timeShake, Vector3 influencePos,
        Vector3 influenceRot, bool stopable)
    {
        this.mag = magnitude;
        this.rough = roughness;
        this.positionInfluence = influencePos;
        this.rotationInfluence = influenceRot;
        tick = Random.Range(-100, 100);
        endShake = timeShake;
        isShaking = true;
    }
}
