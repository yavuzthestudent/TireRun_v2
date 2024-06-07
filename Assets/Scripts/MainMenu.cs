using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{
    public InputField nicknameInput;
    public static string nickname;
    public Text warningText;
    public bool isWarningActive = false;

    public void PlayTheGame(){
        if(nicknameInput.text != null && !string.IsNullOrWhiteSpace(nicknameInput.text))
        {
            nickname = nicknameInput.text;
            SceneManager.LoadScene("Level");
        }
        else
        {
            if(!isWarningActive)
            {    
                StartCoroutine(WarningTextAnim());
                isWarningActive = true;
            }
        }
    }
    public void QuitTheGame(){
        Application.Quit(); 
    }

    IEnumerator WarningTextAnim()
    {
        while(true)
        {
            warningText.text = "Enter A Valid Nickname";
            yield return new WaitForSeconds(0.38f);
            warningText.text = "";
            yield return new WaitForSeconds(0.38f);
        }
    }
}