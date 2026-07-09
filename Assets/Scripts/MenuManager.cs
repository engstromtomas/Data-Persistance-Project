using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    public string playerName;                   // Variable to store the player name
    public TMP_InputField inputField;           // Reference to the TMP_InputField component for player name input



    private void Awake()
    {
        if (Instance != null)                    // Conditional statement for destroying dublicates of the MainManager if it already exists
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void SetPlayerName()                   // Method to set the player name from the input field
    {
        playerName = inputField.text;
    }



    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }
}
