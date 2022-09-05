using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using TMPro;
using UnityEngine.InputSystem;

public class DebugVR : MonoBehaviour
{


    //  private static Dictionary<FieldInfo, dynamic> trackedFieldInfos;
    private static List<Itrackable> trackedFields = new List<Itrackable>();


   // private InputsAsset inputsAsset;

    private XRIDefaultInputActions xriInputs;

    private InputAction toggleConsoleInput;

    [SerializeField]
    private GameObject consoleScreen;


    [SerializeField]
    private List<TextMeshProUGUI> trackedValueTexts, consoleTexts;

    private Queue<string> consoleMessages;


    private void Awake()
    {
       // inputsAsset = new InputsAsset();
        xriInputs = new XRIDefaultInputActions();

        consoleMessages = new Queue<string>();
        //trackedFields = new List<Itrackable>();
      //  toggleConsoleInput = inputsAsset.Player.ToggleConsole;
        toggleConsoleInput = xriInputs.General.ToggleConsole;
    }


    private void OnEnable()
    {
        toggleConsoleInput.Enable();
        toggleConsoleInput.performed += ToggleConsole;

        Application.logMessageReceived += AddConsoleText;
    }


    private void AddConsoleText(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Warning) return;

        consoleMessages.Enqueue(condition);
        if (consoleMessages.Count > consoleTexts.Count)
            consoleMessages.Dequeue();

        var messagesArray = consoleMessages.ToArray();
        for (int i = 0; i < consoleTexts.Count; i++)
        {
            if (i < messagesArray.Length)
                consoleTexts[i].text = messagesArray[i] + " " + stackTrace;
            else
                consoleTexts[i].text = "";
        }


    }



    //the field info reference keeps the class alive, even when destroying the class/object
    public static void TrackValue<T>(T trackedClass, string trackedValue) where T : class
    {
        TrackedClass<T> trackedField = new TrackedClass<T>();

        if (trackedField.TrySetField(trackedClass, trackedValue))
            trackedFields.Add(trackedField);
        else
            Debug.Log("Couldn't find field " + trackedClass + " " + trackedValue);
    }





    public void ToggleConsole(InputAction.CallbackContext obj) => consoleScreen.SetActive(!consoleScreen.activeSelf);





    private void Update()
    {
        for (int i = 0; i < trackedFields.Count && i < trackedValueTexts.Count; i++)
        {
            trackedValueTexts[i].text = trackedFields[i].GetInfo();
        }
    }



    private void OnDisable()
    {
        toggleConsoleInput.Disable();
        toggleConsoleInput.performed -= ToggleConsole;

        Application.logMessageReceived -= AddConsoleText;
    }
}








