using System.IO;

namespace APICalls
{
    using System.Collections;
    using System.Collections.Generic;
    using Model;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.SceneManagement;
    
    public class TagSwapStats : MonoBehaviour
    {
        public TMP_Text GamesPlayed;
        public TMP_Text Wins;
        public TMP_Text Loses;
        //public TMP_Text ErrorText;
        
        public string username;


        private const string saveFileName = "userData.json";

        public string Username
        {
            get => username;
            set => username = value;
        }

        private UserInfo currentUser; // Stores the received userInfo object (optional) of type UserInfo

        public UserInfo CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }


        public void OnTagSwapButtonClick()
        {
            string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
            if (File.Exists(filePath))
            {
                try
                {
                    string dataFromFile = File.ReadAllText(filePath);
                    UserSaveData savedData = JsonUtility.FromJson<UserSaveData>(dataFromFile);
                    currentUser = savedData.userInfo;
                    
                    username = currentUser.Username;
                    
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error loading user data: " + e.Message);
                    // Handle potential loading errors (e.g., delete corrupted data or prompt user to re-login)
                }
            }
            StartCoroutine(TagSwapRequest(username));
        }

        // ReSharper disable Unity.PerformanceAnalysis
        IEnumerator TagSwapRequest(string username)
        {
            string jsonData = JsonUtility.ToJson(username);

            var url = "http://localhost:8080/api/tagSwap/tagSwapStats"; // Backend URL
            UnityWebRequest request = new UnityWebRequest(url, "POST");

            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                string response = request.downloadHandler.text;

                Debug.Log(response);

                // Assuming JSON response with a Result object (check backend implementation)
                Result<TagSwapResponse> tagSwapResult = JsonUtility.FromJson<Result<TagSwapResponse>>(response);
                TagSwapResponse tagSwap = tagSwapResult.Data;
                
                if (tagSwapResult.success)
                {
                    GamesPlayed.text = tagSwap.gamesPlayed.ToString();
                    GamesPlayed.color = Color.green;

                    Wins.text = tagSwap.wins.ToString();
                    Wins.color = Color.green;

                    // Loses.text = tagSwap.loses.ToString();
                    Loses.text = "heyy";
                    Loses.color = Color.green;
                    Debug.Log(Loses.text);
                    Debug.Log(Wins.text);
                    Debug.Log(GamesPlayed.text);
                }
                else
                {
                    Debug.Log(response.ToString());
                    
                    //  ErrorText.text = tagSwapResult.message;
                    // ErrorText.color = Color.red;
                }
            }
        }
    }
}