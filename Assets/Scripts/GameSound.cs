using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSound : MonoBehaviour
{

    public GameObject happySound;
    public GameObject sadSound;

    public GameObject goodFoodSoundGO;
    public GameObject badFoodSoundGO;
    public GameObject[] messagesSoundsGO;
    private static AudioSource happySource;
    private static AudioSource sadSource;
    public static AudioSource[] messages;

    private static AudioSource goodFoodSound;
    private static AudioSource badFoodSound;



    private void Awake() {
        happySource = happySound.GetComponent<AudioSource>();
        sadSource = sadSound.GetComponent<AudioSource>();
        goodFoodSound = goodFoodSoundGO.GetComponent<AudioSource>();
        badFoodSound = badFoodSoundGO.GetComponent<AudioSource>();


        messages = new AudioSource[messagesSoundsGO.Length];
        for (int i = 0; i < messagesSoundsGO.Length; i++) {
            messages[i] = messagesSoundsGO[i].GetComponent<AudioSource>();
        }
    }

    public static void PlayGoodFoodSound() {
        goodFoodSound.Play();
    }
    public static void PlayBadFoodSound() {
        badFoodSound.Play();
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

    public static void PlayMessage(int mNumber) {
        messages[mNumber].Play();
    }

    public static bool happyIsPlayed() {
        return happySource.isPlaying;
    }
    public static bool sadIsPlayed() {
        return sadSource.isPlaying;
    }
}
