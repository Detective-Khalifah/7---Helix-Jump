using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnClickEvents : MonoBehaviour
{

    public TextMeshProUGUI muteSlash;

    // Start is called before the first frame update
    void Start()
    {
        muteSlash.gameObject.SetActive(GameManager.mute);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToggleMute()
    {
        GameManager.mute = !GameManager.mute;
        muteSlash.gameObject.SetActive(GameManager.mute);

    }


    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
