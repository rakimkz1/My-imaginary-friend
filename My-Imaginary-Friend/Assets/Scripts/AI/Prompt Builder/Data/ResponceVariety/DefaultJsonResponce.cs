
using System;
using UnityEngine;

namespace Scripts.AI_Qween
{

    [System.Serializable]
    public class DefaultJsonResponce   // Дефолт не предназначен для проверок. Этот класс можно будет заменить только внешними проверками.
    {
        public string content;
        public string attitude;
        public string state;


        public virtual string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public virtual T FromJson<T>(string json) where T : DefaultJsonResponce
        {
            T responce = JsonUtility.FromJson<T>(json);
            return responce;
        }

    }
}