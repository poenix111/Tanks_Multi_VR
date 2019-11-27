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
            print("Entro a setcontrollerid");
            this.id = ID;
            this.isControlling = (NetworkingClient.ClientID == ID) ? true : false;
            
        }

        public void SetSocketReference(SocketIOComponent socket1) {
            this.socket = socket1;
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
