using System.Collections.Generic;


namespace Scripts.AI_Qween
{

    /// <summary>
    /// Форма для отправки запроса к ИИ.
    /// </summary>
    [System.Serializable]
    public class RequestData
    {
        /// <summary>
        /// Лучше не трогать
        /// </summary>
        public string model = "qwen2.5-72b-instruct";

        /// <summary>
        /// Первый <see cref="messages"/ должен быть от "system". Последующие должны чередоваться: "user" и "assistant".>
        /// </summary>
        public List<Message> messages;
    }

}