using UnityEngine;
using TMPro;

public class SettingSetter : MonoBehaviour
{
    static TMP_InputField[] settingValue;
    static string[] PrefsName =  new string[5] {"Upfield", "Downfield", "Leftfield", "Rightfield", "UsePowerField"};

    private void Awake()
    {
        settingValue = new TMP_InputField[5];
        settingValue[0] = transform.Find("Up/Upfield").GetComponent<TMP_InputField>();
        settingValue[1] = transform.Find("Down/Downfield").GetComponent<TMP_InputField>();
        settingValue[2] = transform.Find("Left/Leftfield").GetComponent<TMP_InputField>();
        settingValue[3] = transform.Find("Right/Rightfield").GetComponent<TMP_InputField>();
        settingValue[4] = transform.Find("UsePower/UsePowerfield").GetComponent<TMP_InputField>();
        ShowText();
    }
    public static void ShowText()
    {
        
        for (int i = 0; i < settingValue.Length; i++)
        {
            Debug.Log(PlayerPrefs.GetString(PrefsName[i],settingValue[i].text));
            settingValue[i].text = PlayerPrefs.GetString(PrefsName[i],settingValue[i].text);
            KeyCode UserKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), settingValue[i].text);
            ControlInput.SetKeyMap(PrefsName[i], UserKeyCode);
            
        }
    }
    public static void SaveSetKey()
    {
        for (int i = 0; i < settingValue.Length; i++)
        {
            PlayerPrefs.SetString(PrefsName[i], settingValue[i].text);
        }
        
    }
}
