using System.Collections.Generic;

namespace APICalls
{
    using System.Collections;
    using UnityEngine;
    using System.Text.RegularExpressions; // for password validation
    using System;
    using Model;
    using UnityEngine.Networking;
    using UnityEngine.UI; // for DateTime parsing
    using UnityEngine.SceneManagement;
    using TMPro;
    
    public class Signup : MonoBehaviour
    {
        public TMP_InputField usernameField;
        public TMP_InputField passwordField;
        public TMP_InputField emailField;
        public TMP_InputField dobField;

        public TMP_Text signupMessage; // Text object to display signup message

        public User currentUser; // Stores the received User object

        public User CurrentUser
        {
            get => currentUser;
            set => currentUser = value;
        }


        // Function to validate password format
        private static bool IsValidPassword(string password)
        {
            const string passwordPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-])(?=.{8,}$)";
            return Regex.IsMatch(password, passwordPattern);
        }

        // Function to try parsing date and handle exceptions
        public string ConvertDoBFormat(string dobString)
        {
            if (dobString.Length != 8)
            {
                signupMessage.text = "Invalid DoB format. Please use YYYYMMDD format";
                signupMessage.color = Color.red;
                // throw new ArgumentException("Invalid DoB format. Please use YYYYMMDD.");
            }

            // Extract year, month, and day (assuming valid format)
            string year = dobString.Substring(0, 4);
            string month = dobString.Substring(4, 2);
            string day = dobString.Substring(6, 2);

            // Try parsing the extracted values as integers
            int yearInt, monthInt, dayInt;
            if (!int.TryParse(year, out yearInt) || !int.TryParse(month, out monthInt) || !int.TryParse(day, out dayInt))
            {
                throw new ArgumentException("Invalid DoB format. Please use YYYYMMDD.");
            }

            // Validate date (month range 1-12, day range based on month)
            if (monthInt < 1 || monthInt > 12 || dayInt < 1 || dayInt > DateTime.DaysInMonth(yearInt, monthInt))
            {
                throw new ArgumentException("Invalid DoB. Please enter a valid date.");
            }

            // Check for date after Jan 1 2018
            DateTime cutoffDate = new DateTime(2018, 1, 1);
            DateTime parsedDate = new DateTime(yearInt, monthInt, dayInt);
            if (parsedDate > cutoffDate)
            {
                throw new ArgumentException("DoB cannot be after January 1st, 2018.");
            }

            // Format the date string with hyphens (if all validations pass)
            return year + "-" + month + "-" + day;
        }


        public void OnSignupButtonClick()
        {
            string username = usernameField.text;
            string password = passwordField.text;
            string email = emailField.text;
            string dobHolder = dobField.text;
            string dobString = ConvertDoBFormat(dobHolder);
            
            // Validate password format
            if (!IsValidPassword(password))
            {
                signupMessage.text = "Password is invalid. Please refer to password requirements.";
                signupMessage.color = Color.red;
                return; // Early exit if password is invalid
            }

            StartCoroutine(SignupRequest(username, password, email, dobString));
        }

        IEnumerator SignupRequest(string username, string password, string email, string dob) {
            // Create SignupObject (assuming SignupRequest class exists)
            SignupObject signupObject = new SignupObject(username, password, email, dob);

            string jsonData = JsonUtility.ToJson(signupObject);
            
            // string jsonData = $"{{\"username\":\"{signupObject.Username}\",\"password\":\"{signupObject.Password}\",\"email\":\"{signupObject.Email}\",\"dob\":\"{signupObject.Dob}\"}}";
            // Debug.Log(jsonData);           

            var url = "http://localhost:8080/api/auth/signup"; // Replace with your backend URL
            UnityWebRequest request = new UnityWebRequest(url, "POST");

            request.SetRequestHeader("Content-Type", "application/json");
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.LogError("Error during signup request: " + request.error);
                signupMessage.text = "Signup failed! Check the console for details.";
                signupMessage.color = Color.red;
            }
            else
            {
                string response = request.downloadHandler.text;
                Debug.Log(response);
                // Assuming JSON response with a Result object containing a User object
                Result<User> signupResult = JsonUtility.FromJson<Result<User>>(response);
                
                Debug.Log("success: " + signupResult.success + " data: " + signupResult.data + " Message: " + signupResult.message);
                
                Debug.Log(signupResult.Data);

                if (signupResult.Success)
                {
                    signupMessage.text =  "Signup successful! Please login.";
                    // Store the User object for future requests
                    currentUser = signupResult.Data;
                    
                    // Handle successful login (e.g., transition to another scene)
                    SceneManager.LoadScene("Login");
                }
                else
                {
                    signupMessage.text = "Signup failed! " + signupResult.message;
                    signupMessage.color = Color.green;
                }
            }
        }
    }
}
