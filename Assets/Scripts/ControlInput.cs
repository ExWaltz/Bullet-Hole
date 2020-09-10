using UnityEngine;
using System.Collections.Generic;

public class ControlInput : MonoBehaviour
{

    static Dictionary<string, KeyCode> KeyMappings;
    public static string[] InputKey = new string[6] 
    {"Upfield", "Downfield", "Leftfield", "Rightfield", "UsePowerField", "SlowDown"};
    public static KeyCode[] KeycodeControl = new KeyCode[6] 
    {KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D, KeyCode.J, KeyCode.LeftShift};

    static Dictionary<string, KeyCode> AltKeyMappings;
    static string[] AltInputKey = new string[6] 
    {"AltUpfield", "AltDownfield", "AltLeftfield", "AltRightfield", "AltUsePowerField", "AltSlowDown" };
    static KeyCode[] AltcontrolInput = new KeyCode[6] 
    {KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.Z, KeyCode.LeftShift};

    static ControlInput()
    {
        StartDic();

    }
    private static void StartDic()
    {
        
        KeyMappings = new Dictionary<string, KeyCode>();
        AltKeyMappings = new Dictionary<string, KeyCode>();
        for (int i = 0; i < InputKey.Length; i++)
        {
            KeyMappings.Add(InputKey[i], KeycodeControl[i]);
            
        }
        for (int i = 0; i < AltInputKey.Length; i++)
        {
            AltKeyMappings.Add(AltInputKey[i], AltcontrolInput[i]);
        }
    }

    public static void SetKeyMap(string keyMap, KeyCode key)
    {
        if (!KeyMappings.ContainsKey(keyMap))
        {
            if(!AltKeyMappings.ContainsKey(keyMap))
            {
                ErrorKeyMap();
                return;
            }
            else
            {
                AltKeyMappings[keyMap] = key;
                return;
            }
        }
        KeyMappings[keyMap] = key;
        
        Debug.Log("Input Changed!");
    }

    public static bool GetKeyDown(string keyMap)
    {
        if (!KeyMappings.ContainsKey(keyMap))
        {
            if (!AltKeyMappings.ContainsKey(keyMap))
            {
                ErrorKeyMap();
            }
            else
            {
                return Input.GetKeyDown(AltKeyMappings[keyMap]);
            }
        }
        return Input.GetKeyDown(KeyMappings[keyMap]);
    }
    public static bool GetAltKeyDown(string keyMap)
    {
        return Input.GetKeyDown(AltKeyMappings[keyMap]);
    }
    public static int GetAxisRaw(string AxisName)
    {
        if(AxisName == "UserHorizontal")
        {
            return CalGetAxisRaw("Leftfield", "Rightfield", "AltLeftfield", "AltRightfield");
        }
        else if (AxisName == "UserVeritical")
        {
            return CalGetAxisRaw("Downfield", "Upfield", "AltDownfield", "AltUpfield");
        }
        else
        {
            ErrorKeyMap();
            return 0;
        }
    }
    public static int CalGetAxisRaw(string NegkeyMap, string PoskeyMap, string AltNegkeyMap, string AltPoskeyMap)
    {

        int IniAxisVal = 0;
        int NegAxisVal = 0;
        int PosAxisVal = 0;

        if (Input.GetKey(KeyMappings[NegkeyMap]))
        {
            NegAxisVal = -1;
            PosAxisVal = 0;
        }
        if (Input.GetKey(KeyMappings[PoskeyMap]))
        {
            NegAxisVal = 0;
            PosAxisVal = 1;
        }
        if (Input.GetKey(AltKeyMappings[AltNegkeyMap]))
        {
            NegAxisVal = -1;
            PosAxisVal = 0;
        }
        if (Input.GetKey(AltKeyMappings[AltPoskeyMap]))
        {
            NegAxisVal = 0;
            PosAxisVal = 1;
        }
        return IniAxisVal + (NegAxisVal + PosAxisVal);
    }

    static void ErrorKeyMap()
    {
        throw new System.Exception("Set KeyMap does not belong on this KeyMap");
    }

}

