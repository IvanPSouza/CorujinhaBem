using System.Collections;
using UnityEngine;

public class ParabensAudioPlayer : MonoBehaviour
{
    [Header("Áudios")]
    public AudioSource audioSource;
    public AudioClip primeiroAudio;
    public AudioClip segundoAudio;

    [Header("Configuração")]
    public float delayEntreAudios = 1f;

    private void Start()
    {
        StartCoroutine(TocarAudios());
    }

    IEnumerator TocarAudios()
    {
        // Toca o primeiro áudio
        audioSource.clip = primeiroAudio;
        audioSource.Play();

        // Espera o primeiro áudio terminar
        yield return new WaitForSecondsRealtime(primeiroAudio.length);

        // Espera o delay definido
        yield return new WaitForSecondsRealtime(delayEntreAudios);

        // Toca o segundo áudio
        audioSource.clip = segundoAudio;
        audioSource.Play();
    }
}