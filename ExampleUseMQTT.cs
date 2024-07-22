using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using mqttConnection;

public class ExampleUseMQTT : MonoBehaviour
{
    private Rigidbody2D rb;
    public string output = "false";

    static string key_api = "VARIABLE_NAME";

    private mqttConnect mqttScript; // Reference to your MQTT script

    public void ReceiveCommand(string command) // Make sure it's public
    {
        Debug.Log($"Received command: {command}");  // For debugging
        output = command;
    }

    void Start()
    {
        // Find the MQTT script in your scene (adjust the name if needed)
        mqttScript = FindObjectOfType<mqttConnect>();

        if (mqttScript != null)
        {
            mqttScript.MessageReceived += HandleMqttMessage;
        }
        else
        {
            Debug.LogError("mqttConnect script not found in the scene!");
        }
        rb = GetComponent<Rigidbody2D>();
    }

    private void HandleMqttMessage(string msg)
    {
        output = msg.Replace("\\", "").Replace("{", "").Replace("}", "").Replace(":", "").Replace("\"", "").Replace(key_api, "");
        Debug.Log(msg);
    }

}
