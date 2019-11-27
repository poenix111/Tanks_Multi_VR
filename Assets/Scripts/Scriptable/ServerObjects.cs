using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
namespace Project.Scriptable
{   
    [CreateAssetMenu(fileName = "Server_Objects", menuName = "Scriptable Objects/Server Objects", order = 3)]
    public class ServerObjects : ScriptableObject
    {
        public List<ServerObjectData> objects;
        public ServerObjectData GetObjectByName(string name1){
            return objects.SingleOrDefault(x => x.name == name1 );
        }
    }
    [Serializable]
    public class ServerObjectData{
        public string name = "new object";
        public GameObject prefab;
    }
}


