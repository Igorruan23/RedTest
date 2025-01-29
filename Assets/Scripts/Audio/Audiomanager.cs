using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{

    [Header("AUDIOSOURCE")]
    [SerializeField] AudioSource SFXSource;

    public AudioClip PunchAudio;
    public AudioClip SpecialAudio;
    public AudioClip SpecialUIAudio;


    /// <summary>
    /// Object pool para evitar de um audio quando tocado não sobrescrever o outro
    /// </summary>
    private Queue<AudioSource> audioPool = new Queue<AudioSource>();
    //Coloquei quatro pois caso clique rapido os audios podem se sobrepor, logo isso garante mais segurança contra bugs.
    private int poolSize = 4;

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject tempAudio = new GameObject("PooledAudio");
            AudioSource tempSource = tempAudio.AddComponent<AudioSource>();
            tempAudio.transform.SetParent(transform);
            tempAudio.SetActive(false);
            audioPool.Enqueue(tempSource);
        }
    }

    public void PlaySfx(AudioClip clip)
    {
        //gera uma pool de audio source (so estarão ativados quando estiver tocando algo)
        AudioSource tempSource = audioPool.Dequeue();
        tempSource.gameObject.SetActive(true);
        tempSource.clip = clip;
        tempSource.Play();
        StartCoroutine(ReturnToPoolAfterPlay(tempSource, clip.length));
    }

    private IEnumerator ReturnToPoolAfterPlay(AudioSource source, float delay)
    {
        //coroutina para desativar o gameobject (audiosource) apos terminar de tocar o audio
        yield return new WaitForSeconds(delay);
        source.gameObject.SetActive(false);
        audioPool.Enqueue(source);
    }
}
