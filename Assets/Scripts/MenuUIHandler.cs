using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Drawing;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
 
    public TMP_InputField inputField;               // Reference to the TMP_InputField component for player name input


    public void NewPlayer(string text)
    {
        // add code here to handle when a text is entered in the input field
        MenuManager.Instance.activePlayerName = text;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


    public void SetPlayerName()                     // Method to set the player name from the input field
    {
        MenuManager.Instance.activePlayerName = inputField.text;
        Debug.Log("Player Name:" + MenuManager.Instance.activePlayerName);    // Log the player name to the console for debugging
    }
}
