using UnityEngine;

public class Audiomanager : MonoBehaviour
{

    [Header("AUDIOSOURCE")]
    [SerializeField] AudioSource SFXSource;

    public AudioClip PunchAudio;
    public AudioClip SpecialAudio;
    public AudioClip SpecialUIAudio;

    public void PlaySfx(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
