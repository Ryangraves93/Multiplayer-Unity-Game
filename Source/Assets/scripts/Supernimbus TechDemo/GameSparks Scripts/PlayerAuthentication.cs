using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAuthentication : MonoBehaviour
{

    public Text userNameInput, passwordInput;

    public GameObject PlayScreen, SignInScreen;

    /*public void AuthorizePlayerButton()
    {
        
        Debug.Log("Authorizing Player ...");
        new GameSparks.Api.Requests.AuthenticationRequest()
            .SetUserName(userNameInput.text)
            .SetPassword(passwordInput.text)
            .Send((response) =>
           {
               if (!response.HasErrors)
               {
                   Debug.Log("Player Authenticated ... \n User Name " + response.DisplayName);
                   PlayScreen.SetActive(true);
                   SignInScreen.SetActive(false);
                   Debug.Log("User name!" + userNameInput.text);
                   //GameSparksManager.Instance.SetUsername(userNameInput.text);


               }
               else
               {
                   Debug.Log("Error Authenticating Player ... \n " + response.Errors.JSON.ToString());

               }
           });


    }*/
}
