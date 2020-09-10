using UnityEngine;
using TMPro;
using System.Collections;

public class UserInput : MonoBehaviour
{
    static string[] FieldName = new string[5] { "Upfield", "Downfield", "Leftfield", "Rightfield", "UsePowerField"};
    public void ChangeInput(TMP_InputField TMIF)
    {
        KeyCode UserKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), TMIF.text.ToUpper());
        for (int i = 0; i < FieldName.Length; i++)
        {
            if (TMIF.name == FieldName[i])
            {
                
                ControlInput.SetKeyMap(FieldName[i], UserKeyCode);
                Debug.Log("Found!!");
                StartCoroutine(SaveKeyDelay());
            }
        }
    }
    public void ToUpper(TMP_InputField TMIF)
    {
        TMIF.text = TMIF.text.ToUpper();
    }
    IEnumerator SaveKeyDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SettingSetter.SaveSetKey();
        SettingSetter.ShowText();
    }
}
