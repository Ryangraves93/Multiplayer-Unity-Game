using Org.BouncyCastle.Asn1.Ocsp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPlayer : MonoBehaviour
{
    [SerializeField]
    private Text displayNameInput, userNameInput, passwordInput, passwordConfirmation;

    [SerializeField]
    private GameObject SignUpScreen, SignInScreen;

    public void RegisterPlayerButton()
    {
        Debug.Log("Registering Player...");
        new GameSparks.Api.Requests.RegistrationRequest()
            .SetDisplayName(displayNameInput.text)
            .SetUserName(userNameInput.text)
            .SetPassword(passwordInput.text)
            .Send((response) => { 

                if (!response.HasErrors)
                    {
                        SignUpScreen.SetActive(false);
                        SignInScreen.SetActive(true);
                        Debug.Log("Player Registered \n User Name : " + response.DisplayName);
                    }
                    else
                    {
                        Debug.Log("Error Registering Player... \n User Name: @" + response.Errors.JSON.ToString());
                    }
    });

    }
// Start is called before the first frame update
void Start()
{

}

// Update is called once per frame
void Update()
{

}
}
