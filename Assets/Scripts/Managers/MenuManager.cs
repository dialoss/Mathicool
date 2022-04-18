using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public Image soundIcon;
    public Sprite[] icons;
    public LayerMask iconLayer;
    private bool isMusicMuted;
    public GameObject audioObject;
    private AudioSource audio;
    private void Awake()
    {
        isMusicMuted = PlayerPrefs.GetInt("isMusicMuted", 0) == 1 ? true : false;
        GameObject[] music = GameObject.FindGameObjectsWithTag("Music");
        if (music.Length == 1)
        {
            DontDestroyOnLoad(audioObject);
        }
        else
        {
            Destroy(music[1]);
        }
        audio = GameObject.FindGameObjectWithTag("Music").GetComponent<AudioSource>();
        setDefaults();
    }
    private void Update()
    {   
        audio.mute = isMusicMuted;
        soundIcon.sprite = icons[PlayerPrefs.GetInt("isMusicMuted", 0)];
        soundIcon.color = new Color(1, 1, 1, 0.8f);
    }
    private void raycastUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResults);
        for (int i = 0; i < raycastResults.Count; i++)
        {
            if (raycastResults[i].gameObject.name == "Music") return;
        }
    }
    public void setMusicMute()
    {
        isMusicMuted = !isMusicMuted;
        PlayerPrefs.SetInt("isMusicMuted", isMusicMuted ? 1 : 0);
    }
    public void loadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void resetGame()
    {
        PlayerPrefs.DeleteAll();
    }
    public void add()
    {
        PlayerPrefs.SetInt("allStars", PlayerPrefs.GetInt("allStars") + 3);
    }
    private void setDefaults()
    {
        if (PlayerPrefs.GetInt("add", 0) == 0) PlayerPrefs.SetInt("add", 1);
        if (PlayerPrefs.GetInt("div", 0) == 0) PlayerPrefs.SetInt("div", 1);
        if (PlayerPrefs.GetInt("mult", 0) == 0) PlayerPrefs.SetInt("mult", 1);
        if (PlayerPrefs.GetInt("sub", 0) == 0) PlayerPrefs.SetInt("sub", 1);
    }
}
