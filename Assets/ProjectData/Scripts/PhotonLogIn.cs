using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PhotonLogIn : MonoBehaviour
{
    public UnityEvent ButtonEvent;

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI label;
    [SerializeField] private Image dot;

    private void Start()
    {
        button.onClick.AddListener(ButtonClicked);
    }

    private void ButtonClicked()
    {
        ButtonEvent.Invoke();
    }

    public void OnLogin()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log Out";
        label.text = "Welcome";
        dot.color = label.color = Color.green;
    }

    public void OnLogout()
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "Log In";
        label.text = "Goodbye";
        dot.color = label.color = Color.red;
    }
}
