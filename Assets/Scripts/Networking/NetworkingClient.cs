using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using Project.Utility;
using System;
namespace Project.Networking
{
    public class NetworkingClient : SocketIOComponent
    {
        [Header("Network client")]
        [SerializeField]
        private Transform networkContainer;
        [SerializeField]
        private GameObject playerPrefab;
        public static string ClientID { get; private set; }
        private Dictionary<string, NetworkIdentity> serverObjects;
        // Start is called before the first frame update
        public override void Start()
        {
            base.Start();
            initialize();
            setupEvents();
        }
        private void initialize()
        {
            serverObjects = new Dictionary<string, NetworkIdentity>();
        }
        // Update is called once per frame
        public override void Update()
        {
            base.Update();
        }

        private void setupEvents()
        {
            On("open", (E) =>
            {
                Debug.Log("Connection made to the server");
            });

            On("register", (E) =>
            {
                ClientID = E.data["id"].ToString().RemoveQuotes();
                Debug.LogFormat("Our Clent's ID ({0})", ClientID);

            });

            On("spawn", (E) =>
            {
                string id = E.data["id"].ToString().RemoveQuotes();
                GameObject go = Instantiate(playerPrefab, networkContainer);
                go.name = string.Format("Player ({0})", id);
                NetworkIdentity n1 = GetComponent<NetworkIdentity>();
                n1.SetControllerID(id);
                n1.SetSocketReference(this);
                serverObjects.Add(id, n1);
            });
            On("disconnected", (E) =>
            {
                string id = E.data["id"].ToString().RemoveQuotes();
                GameObject go = serverObjects[id].gameObject;
                Destroy(go);
                serverObjects.Remove(id);
            });

            On("updatePosition", (E) =>
            {
                string id = E.data["id"].ToString().RemoveQuotes();
                float x = E.data["position"]["x"].f;
                float y = E.data["position"]["y"].f;
                float z = E.data["position"]["z"].f;

                NetworkIdentity n1 = serverObjects[id];
                n1.transform.position = new Vector3(x,y,z);
            });
        }

    }
    [Serializable]
    public class Player
    {
        public string id;
        public Position position;
    }

    [Serializable]
    public class Position
    {
        public float x;
        public float y;
        public float z;
    }
}

