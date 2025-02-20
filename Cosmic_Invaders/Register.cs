using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public InputField PasswordConfirm;
    public InputField EmailInput;
    public Button SubmitButton;

    // Start is called before the first frame update
    void Start()
    {
        SubmitButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text, EmailInput.text));
        });
    }
}
