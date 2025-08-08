using UnityEngine;
using System;
using Unity.Netcode;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using Unity.Netcode.Transports.UTP;
#if NEW_INPUT_SYSTEM_INSTALLED
using UnityEngine.InputSystem.UI;
#endif

namespace Horror.UI
{
    public class WebConnectUI : MonoBehaviour
    {
        [SerializeField] GameObject panel;
        [SerializeField] TMP_InputField ipField, portField;
        [SerializeField] TMP_Dropdown clientHostDropdown;
        [SerializeField] Button connectButton;
        void Awake()
        {
            if (!FindAnyObjectByType<EventSystem>())
            {
                var inputType = typeof(StandaloneInputModule);
#if ENABLE_INPUT_SYSTEM && NEW_INPUT_SYSTEM_INSTALLED
                inputType = typeof(InputSystemUIInputModule);                
#endif
                var eventSystem = new GameObject("EventSystem", typeof(EventSystem), inputType);
                eventSystem.transform.SetParent(transform);
            }
        }
        void Start()
        {
            clientHostDropdown.onValueChanged.AddListener(ClientHostChanged);
            connectButton.onClick.AddListener(Connect);
            Refresh();
        }

        private void Connect()
        {
            bool success;
            var transport =(UnityTransport)NetworkManager.Singleton.NetworkConfig.NetworkTransport;
            ushort port = ushort.Parse(portField.text);
            if (clientHostDropdown.value == 0)
            {
                transport.SetConnectionData("127.0.0.1",port);
                success = NetworkManager.Singleton.StartHost();
            }
            else
            {
                transport.SetConnectionData(ipField.text,port);
                success = NetworkManager.Singleton.StartClient();
            }
            Refresh();
            panel.gameObject.SetActive(!success);
        }
        void Refresh()
        {
            ipField.enabled = clientHostDropdown.value != 0;
        }
        private void ClientHostChanged(int option)
        {
            Refresh();
        }
    }
}
