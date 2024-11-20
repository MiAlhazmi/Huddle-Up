using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Model
{
    public class DisplayUsername : MonoBehaviour
    {
        public TMP_Text username;
        // public TMP_Text registeredData;
        public TMP_Text xp;
        
        private const string saveFileName = "userData.json";

        private UserInfo currentUser;

        // public TMP_Text RegisteredData
        // {
        //     get => registeredData;
        //     set => registeredData = value;
        // }

        public TMP_Text Xp
        {
            get => xp;
            set => xp = value;
        }

        public UserInfo CurrentUser
        {
            get => currentUser;
            set => currentUser = value;
        }

        public TMP_Text Username
        {
            get => username;
            set => username = value;
        }
        
        void Awake()
        {
            // Check if user data file exists and load data on awake
            string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
            if (File.Exists(filePath))
            {
                try
                {
                    string dataFromFile = File.ReadAllText(filePath);
                    UserSaveData savedData = JsonUtility.FromJson<UserSaveData>(dataFromFile);
                    currentUser = savedData.userInfo;

                    username.text = currentUser.username;
                    xp.text = currentUser.Xp.ToString();
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error loading user data: " + e.Message);
                    // Handle potential loading errors (e.g., delete corrupted data or prompt user to re-login)
                }
            }
        }
    }
}