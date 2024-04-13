using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoginSceneController : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    public Button loginButton;
    public Button registerButton;

    void Start()
    {
        // Add click listeners to the buttons
        loginButton.onClick.AddListener(OnLoginButtonClicked);
        registerButton.onClick.AddListener(OnRegisterButtonClicked);
    }

    void OnLoginButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        // Perform authentication (replace with your authentication logic)
        bool isAuthenticated = AuthenticateUser(username, password);

        if (isAuthenticated)
        {
            // Load the MainMenu scene
            SceneManager.LoadScene("MainMenu_Scene");
        }
        else
        {
            // Display error message or handle authentication failure
            Debug.Log("Authentication failed. Please check your credentials.");
        }
    }

    void OnRegisterButtonClicked()
    {
        // Load the Register scene
        SceneManager.LoadScene("Register");
    }

    bool AuthenticateUser(string username, string password)
    {
        // Placeholder authentication logic (replace with your authentication logic)
        // For demonstration purposes, assume username and password are correct if they are not empty
        return !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password);
    }
}