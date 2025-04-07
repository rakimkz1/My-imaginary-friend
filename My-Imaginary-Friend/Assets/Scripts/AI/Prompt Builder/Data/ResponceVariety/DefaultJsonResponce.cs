
using System;
using UnityEngine;

namespace Scripts.AI_Qween
{

    [System.Serializable]
    public class DefaultJsonResponce   
    {
        public string content;
        public string attitude;
        public string state;


        public virtual string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public static T FromJson<T>(string json) where T : DefaultJsonResponce
        {
            T responce = JsonUtility.FromJson<T>(json);
            return responce;
        }

    }
}