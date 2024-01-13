using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccountWindow : AccountDataWindowBase
{
    [SerializeField] private InputField _mailField;
    [SerializeField] private Button _createAccountButton;
    private string _mail;

    protected override void SubscriptionsElementsUi()
    {
        base.SubscriptionsElementsUi();

        _mailField.onValueChanged.AddListener(UpdateMail);
        _createAccountButton.onClick.AddListener(CreateAccount);
    }
    public void UpdateMail(string mail)
    {
        _mail = mail;
    }
    public void CreateAccount()
    {
        PlayFabClientAPI.RegisterPlayFabUser(new RegisterPlayFabUserRequest
        {
            Username = _username,
            Email = _mail,
            Password = _password,
            RequireBothUsernameAndEmail = true
        }, result =>
        {
            Debug.Log($"Success:{_username}");
            EnterInGameScene();
        }, error =>
        {
            Debug.LogError($"Error: {error.ErrorMessage}");
        });
    }

}
