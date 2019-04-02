using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CinematiqueDebut : MonoBehaviour
{
    public GameObject sound;
    public GameObject music;
    void FinCinematique()
    {
        SceneManager.LoadScene(1);
    }
    void StartAlarm()
    {
        sound.SetActive(true);
        music.SetActive(true);
    }

    void StopAlarm()
    {
        sound.SetActive(false);
    }

}
