using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using Project.Utility;
using System;
using Project.Scriptable;
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
        [SerializeField]
        private ServerObjects serverSpawnables;
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
                NetworkIdentity n1 = go.GetComponent<NetworkIdentity>();
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
                n1.transform.position = new Vector3(x, y, z);
            });

            On("serverSpawn", (E) =>
            {
                string name = E.data["name"].str;
                string id = E.data["id"].ToString().RemoveQuotes();
                float x = E.data["position"]["x"].f;
                float y = E.data["position"]["y"].f;
                float z = E.data["position"]["z"].f;
                Debug.LogFormat("Server wants us to spawn a (0)", name);

                if (!serverObjects.ContainsKey(id))
                {
                    ServerObjectData sod = serverSpawnables.GetObjectByName(name);
                    var spawnedObject = Instantiate(sod.prefab, networkContainer);
                    spawnedObject.transform.position = new Vector3(x, y, z);

                    var ni = spawnedObject.GetComponent<NetworkIdentity>();
                    ni.SetControllerID(id);
                    ni.SetSocketReference(this);

                    if (name == "Shell")
                    {
                        float directionX = E.data["direction"]["x"].f;
                        float directionY = E.data["direction"]["y"].f;
                        float directionZ = E.data["direction"]["z"].f;
                        
                        //something with atan2

                        float rot = Mathf.Atan2(directionX, directionY) * Mathf.Rad2Deg;
                        Vector3 currentRotation = new Vector3(0,0, rot - 90);

                        spawnedObject.transform.rotation = Quaternion.Euler(currentRotation);

                    }

                    serverObjects.Add(id, ni);
                }

            });
            On("serverUnspawn", (E) =>
            {
                string id = E.data["id"].ToString().RemoveQuotes();
                NetworkIdentity n1 = serverObjects[id];
                serverObjects.Remove(id);
                DestroyImmediate(n1.gameObject);
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
    [Serializable]
    public class ShellData
    {
        public string id;
        public Position position;
        public Position direction;
    }
}

