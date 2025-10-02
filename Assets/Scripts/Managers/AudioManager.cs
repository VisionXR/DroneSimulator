using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource ButtonClickAS;
    public AudioSource DroneCrashAS;
    public AudioSource CoinCollectedAS;


    [Header("Scriptable Objects")]
    public DroneDataSO droneData;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        droneData.CamBtnClickiedEvent += PlayBtnClickSound;
        droneData.PowerButtonClickedEvent += PlayBtnClickSound;
        droneData.DroneCollidedEvent += PlayDroneCrashSound;
        droneData.DroneTouchedEvent += PlayCoinCollectedSound;
    }

    private void OnDisable()
    {
        droneData.CamBtnClickiedEvent -= PlayBtnClickSound;
        droneData.PowerButtonClickedEvent -= PlayBtnClickSound;
        droneData.DroneCollidedEvent -= PlayDroneCrashSound;
        droneData.DroneTouchedEvent -= PlayCoinCollectedSound;
    }
    public void PlayBtnClickSound(bool state1,bool state2)
    {
        if (ButtonClickAS != null && (state1 || state2))
        {
            ButtonClickAS.Play();
        }
    }

    public void PlayBtnClickSound(bool state)
    {
        if (ButtonClickAS != null && state)
        {
            ButtonClickAS.Play();
        }
    }

    public void PlayDroneCrashSound()
    {
        if (DroneCrashAS != null)
        {
            DroneCrashAS.Play();
        }
    }

    public void PlayCoinCollectedSound(GameObject tag)
    {
        if (CoinCollectedAS != null && tag.CompareTag("Coin"))
        {
            CoinCollectedAS.Play();
        }
    }
}
