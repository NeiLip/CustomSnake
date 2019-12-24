using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{

    public GameObject happySound;
    public GameObject sadSound;
    private static AudioSource happySource;
    private static AudioSource sadSource;



    private void Awake() {
        happySource = happySound.GetComponent<AudioSource>();
        sadSource = sadSound.GetComponent<AudioSource>();
    }


    public static void PlayHappy() {
        happySource.Play();
    }

    public static void StopHappy() {
        happySource.Stop();
    }

    public static void PlaySad() {
        sadSource.Play();
    }
    public static void StopSad() {
        sadSource.Stop();
    }

    public static bool happyIsPlayed() {
        return happySource.isPlaying;
    }
    public static bool sadIsPlayed() {
        return sadSource.isPlaying;
    }
}
