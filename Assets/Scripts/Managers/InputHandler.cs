using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    private void Awake()
    {
        Object[] objects = Object.FindObjectsOfType<InputHandler>();
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != this)
            {
                if (objects[i].name == gameObject.name)
                {
                    Destroy(gameObject);
                }
                
            }
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        onEscButton();
    }
    private void onEscButton()
    {
        string curScene = SceneManager.GetActiveScene().name;
        if (Input.GetKey(KeyCode.Escape) && curScene != "Main Menu")
        {
            if (curScene == "Select Level" || curScene == "Shop") SceneManager.LoadScene("Main Menu");
            else if (curScene == "Game" || curScene == "Book") SceneManager.LoadScene("Select Level");
        }
    }
    public void loadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
