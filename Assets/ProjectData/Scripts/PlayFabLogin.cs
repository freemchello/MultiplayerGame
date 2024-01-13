using PlayFab;
using PlayFab.ClientModels;
using System;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class PlayFabLogin : MonoBehaviour
{
    public UnityEvent ButtonEvent;

    private const string AuthGuidKey = "1593578264";

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image dot;


    public void Start()
    {
        button.onClick.AddListener(ButtonClicked);

        var needCreation = PlayerPrefs.HasKey(AuthGuidKey);
        var id = PlayerPrefs.GetString(AuthGuidKey, Guid.NewGuid().ToString());

        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "BC440";
        }

        var request = new LoginWithCustomIDRequest
        {
            CustomId = id,
            CreateAccount = !needCreation
        }; 
        PlayFabClientAPI.LoginWithCustomID(request,
            result =>
            {
                PlayerPrefs.SetString(AuthGuidKey, id);
                OnLoginSuccess(result);
            },  OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log Out";
        label.text = "Welcome to PlayFab";
        dot.color = Color.green;
        label.color = Color.yellow;
        Debug.Log("Congratulations, you made successful API call!");
    }
    private void OnLoginFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log In";
        label.text = "Playfab Disconnected";
        dot.color = Color.red;
        label.color = Color.black;
        Debug.LogError($"Something went wrong: {errorMessage}");
    }

    private void ButtonClicked()
    {
        ButtonEvent.Invoke();
    }
}