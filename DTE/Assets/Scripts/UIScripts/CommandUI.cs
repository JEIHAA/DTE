using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommandUI : MonoBehaviour
{
    [SerializeField]
    private GameObject textChatPrefab;
    [SerializeField]
    private TMP_InputField inputField;
    [SerializeField]
    private Transform parentContent;

    List<string> commands = new List<string>();

    private string ID = "CoctorKO";

    

    public void OnEndEditEventMeghod()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            UpdateChat();
        }
    }

    public void UpdateChat()
    {
        if (inputField.text.Equals("")) return;

        GameObject clone = Instantiate(textChatPrefab, parentContent);

        clone.GetComponent<TextMeshProUGUI>().text = $"{ID} : {inputField.text}";
        commands.Add(clone.GetComponent<TextMeshProUGUI>().text);
        inputField.text = "";


        float offset = 30.0f;
        parentContent.position += new Vector3(0f, offset, 0f); 
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return) && inputField.isFocused == false)
        {
            inputField.ActivateInputField();
        }
    }




}
