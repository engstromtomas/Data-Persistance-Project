using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using TMPro;
using TMPro.Examples;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    public string activePlayerName;                 // Variable to store the active player name
    public int activeScore;                         // Variable to store the active score
    public string bestScorePlayerName;              // Variable to store the name of the player with the highest score
    public int bestScore;                           // Variable to store the highest score

    private void Awake()
    {
        if (Instance != null)                       // Conditional statement for destroying dublicates of the MainManager if it already exists
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    [System.Serializable]
    class SaveData
    {
        public string bestScorePlayerName;
        public int bestScore;
    }

    public void SavePlayerName()                    // Save the player name to a JSON file
    {
        SaveData data = new SaveData();
        data.bestScorePlayerName = bestScorePlayerName;
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerName()                    // Load the player name from the JSON file
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScorePlayerName = data.bestScorePlayerName;
            bestScore = data.bestScore;
        }
    }
}

