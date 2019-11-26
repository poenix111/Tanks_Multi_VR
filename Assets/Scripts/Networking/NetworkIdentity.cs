using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Project.Utility.Attributes;
using SocketIO;
namespace Project.Networking
{
    public class NetworkIdentity : MonoBehaviour
    {   
        [Header("Helpful values")]
        [SerializeField]
        [GreyOut]
        private string id;
        [SerializeField]
        [GreyOut]
        private bool isControlling;

        private SocketIOComponent socket;


        // Start is called before the first frame update
        public void Awake()
        {
            isControlling = false;

        }

        // Update is called once per frame
        public void SetControllerID(string ID)
        {
            id = ID;
            isControlling = (NetworkingClient.ClientID == ID)? true: false;
            
        }

        public void SetSocketReference(SocketIOComponent socket) {
            this.socket = socket;
        }

        public string GetID() {
            return id;
        }

        public bool GetIsControlling() {
            return isControlling;
        }

        public SocketIOComponent GetSocket() {
            return socket;
        }
    }

}
