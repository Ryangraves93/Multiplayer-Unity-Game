using GameSparks.Api.Requests;
using GameSparks.Core;
using GameSparks.Api.Responses;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*Class is written to handle any login functionality with gamesparks authentication
  and registration using the gamesparks API*/
public class LoginPanel : MonoBehaviour
{
    [SerializeField]
    private InputField userNameInput;
    [SerializeField]
    private InputField passwordInput;
    [SerializeField]
    private Button loginButton;
    [SerializeField]
    private Button registerButton;
    [SerializeField]
    private Text errorMessageText;

    /* Intialize event listeners for login and registration functionality. */
    private void Awake()
    {
        loginButton.onClick.AddListener(Login);
        registerButton.onClick.AddListener(Register);
    }

    /* Creates a new authentication request for user authentication and creates
      a callback on success or error. */
    private void Login()
    {
        AuthenticationRequest request = new AuthenticationRequest();
        request.SetUserName(userNameInput.text);
        request.SetPassword(passwordInput.text);
        request.Send(OnLoginSuccess, OnLoginError);
   
    }

    /* Loads the next scene index and retrieves player data from Gamesparks
       back end. */
    private void OnLoginSuccess(AuthenticationResponse response)
    {
        GameSparksManager.Instance.LoadPlayerData();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /* Parses the error from the scriptdata and displays it to the user. */
    private void OnLoginError(AuthenticationResponse response)
    {
        GSData scriptData = response.Errors;

        string errorData = "Login error - " + scriptData.GetString("DETAILS").ToLower();
        errorMessageText.text = errorData;
    }

    /* Creates a new registration request for user registration and creates
      a callback on success or error. */
    private void Register()
    {
        RegistrationRequest request = new RegistrationRequest();
        request.SetUserName(userNameInput.text);
        request.SetDisplayName(userNameInput.text);
        request.SetPassword(passwordInput.text);
        request.Send(OnRegistrationSuccess, OnRegistrationError);
    }

    /* Login player immediately after registration. A simple implementation done
       for the sake of time of a tech demo. Could be expanded to have the user login
       and confirm details. */

    private void OnRegistrationSuccess(RegistrationResponse response)
    {
        Login();
    }

    /* Parses the error from the scriptdata and displays it to the user. */

    private void OnRegistrationError(RegistrationResponse response)
    {
        GSData scriptData = response.Errors;

        string errorData = "Registration error - " + scriptData.GetString("USERNAME").ToLower();
        errorMessageText.text = errorData;
    }
}
