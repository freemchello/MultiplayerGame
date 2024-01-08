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

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image dot;

    public void Start()
    {
        button.onClick.AddListener(ButtonClicked);

        if (string.IsNullOrEmpty(PlayFabSettings.staticSettings.TitleId))
        {
            PlayFabSettings.staticSettings.TitleId = "BC440";
        }

        var request = new LoginWithCustomIDRequest
        {
            CustomId = "User1",
            CreateAccount = true
        };

        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }
    private void OnLoginSuccess(LoginResult result)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log Out";
        label.text = "Welcome";
        dot.color = Color.green;
        label.color = Color.yellow;
        Debug.Log("Congratulations, you made successful API call!");
    }
    private void OnLoginFailure(PlayFabError error)
    {
        var errorMessage = error.GenerateErrorReport();
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log In";
        label.text = "Goodbye";
        dot.color = label.color = Color.red;
        Debug.LogError($"Something went wrong: {errorMessage}");
    }

    private void ButtonClicked()
    {
        ButtonEvent.Invoke();
    }
}