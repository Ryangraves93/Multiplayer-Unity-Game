using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUi : MonoBehaviour
{

    public GameObject SignUp, HomeMenu, SignIn;
    public void DisplaySignup()
    {
        HomeMenu.SetActive(false);
        SignUp.SetActive(true);
    }

    public void DisplaySignIn()
    {
        HomeMenu.SetActive(false);
        SignIn.SetActive(true);
    }
}
