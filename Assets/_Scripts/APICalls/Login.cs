using System.IO;

namespace APICalls
{
    using System.Collections;
    using System.Text;
    using Model;
    using UnityEngine;
    using UnityEngine.Networking;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using static System.Text.Encoding; // for UnityWebRequest
    using TMPro;
    public class Login : MonoBehaviour
    {
        public TMP_InputField usernameField;
        public TMP_InputField passwordField;
        public TMP_Text loginMessage; // Text object to display login message
        
        private const string saveFileName = "userData.json";

        private UserInfo currentUser; // Stores the received userInfo object (optional) of type UserInfo

        public UserInfo CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }


        public void OnLoginButtonClick()
        {
            string username = usernameField.text;
            string password = passwordField.text;
            StartCoroutine(LoginRequest(username, password));
        }

        IEnumerator LoginRequest(string username, string password)
        {
            // Create LoginRequest object
            var loginObject = new LoginObject(username, password);
            string jsonData = JsonUtility.ToJson(loginObject);

            var url = "http://localhost:8080/api/auth/login"; // Backend URL
            UnityWebRequest request = new UnityWebRequest(url, "POST");

            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error during login request: error " + request.error);
                loginMessage.text = "Login failed!";
                loginMessage.color = Color.red;
            }
            else
            {
                string response = request.downloadHandler.text;
                
                Debug.Log(response);
                
                // Assuming JSON response with a Result object (check backend implementation)
                Result<UserInfo> loginResult = JsonUtility.FromJson<Result<UserInfo>>(response);

                if (loginResult.success && loginResult.Data != null)
                {
                    loginMessage.text = loginResult.message;
                    loginMessage.color = Color.green;

                    // Optionally store user info for future use
                    currentUser = loginResult.Data; // Now currentUser is of type UserInfo
                    
                    SaveUserData(currentUser,true);

                    // Handle successful login (e.g., transition to another scene)
                    SceneManager.LoadScene("MainMenu_Scene");
                }
                else
                {
                    loginMessage.text = "Login failed! " + loginResult.message;
                    loginMessage.color = Color.red;
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
        
                    if (savedData.isLoggedIn)
                    {
                        // Skip login if flag is true (handle transition to next scene)
                        SceneManager.LoadScene("MainMenu_Scene");
                    }
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
