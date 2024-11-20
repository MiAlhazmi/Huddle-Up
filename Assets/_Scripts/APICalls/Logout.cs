using System.Collections;
using System.IO;
using Model;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace APICalls
{
    public class Logout : MonoBehaviour
    {
        private const string saveFileName = "userData.json";
        public string logoutMessage;
        private UserInfo currentUser; // Stores the received userInfo object (optional) of type UserInfo

        public string LogoutMessage
        {
            get => logoutMessage;
            set => logoutMessage = value;
        }

        public UserInfo CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }
        
        public void OnLogoutButtonClick()
        {
            string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
            if (File.Exists(filePath))
            {
                try
                {
                    string dataFromFile = File.ReadAllText(filePath);
                    UserSaveData savedData = JsonUtility.FromJson<UserSaveData>(dataFromFile);
                    currentUser = savedData.userInfo;

                    StartCoroutine(LogoutRequest(currentUser.Username));
                }
                catch (System.Exception e)
                {
                    Debug.LogError("Error loading user data: " + e.Message);
                    // Handle potential loading errors (e.g., delete corrupted data or prompt user to re-login)
                }
            }
        }
        
        IEnumerator LogoutRequest(string username)
        {
            // Create LoginRequest object
            var logoutObject = new UsernameObject(username);
            string jsonData = JsonUtility.ToJson(logoutObject);

            var url = "http://localhost:8080/api/auth/logout"; // Backend URL
            UnityWebRequest request = new UnityWebRequest(url, "POST");

            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error during login request: error " + request.error);
                logoutMessage = "Logout failed!";
                Debug.Log(logoutMessage);
            }
            else
            {
                string response = request.downloadHandler.text;
                Debug.Log(response);
                // Assuming JSON response with a Result object (check backend implementation)
                Result<Null> logoutResult = JsonUtility.FromJson<Result<Null>>(response);

                if (logoutResult.success)
                {
                    logoutMessage= logoutResult.message;
                    Debug.Log(logoutMessage);
                    SaveUserData(new UserInfo(null,0,0),false);

                    // Handle successful login (e.g., transition to another scene)
                    SceneManager.LoadScene("Login");
                }
                else
                {
                    logoutMessage = "Login failed! " + logoutResult.message;
                    Debug.Log(logoutMessage);
                }
            }
        }
        
        private void SaveUserData(UserInfo userInfo, bool isLoggedIn)
        {
            string dataToSave = JsonUtility.ToJson(new UserSaveData(userInfo, isLoggedIn));

            try
            {
                File.WriteAllText(Path.Combine(Application.persistentDataPath, saveFileName), dataToSave);
                Debug.Log(Application.persistentDataPath);
                Debug.Log("User data saved successfully!");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Error saving user data: " + e.Message);
                // Handle potential saving errors (e.g., display a message to the user)
            }
        }
    }
}
