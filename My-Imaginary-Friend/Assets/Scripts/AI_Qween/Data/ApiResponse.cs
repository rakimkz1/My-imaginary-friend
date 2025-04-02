

namespace Scripts.AI_Qween
{
    /// <summary>
    /// Форма для получение запроса от ИИ.
    /// </summary>
    [System.Serializable]
    public class ApiResponse
    {

        /// <summary>
        /// Варианты ответов. Обычно их только 1.
        /// </summary>
        public Choice[] choices;
        public string @object;

        /// <summary>
        /// хранит в себе сколько токенов было использовано.
        /// </summary>
        public Usage usage;
        public int created;
        public string system_fingerprint;
        public string model;
        public string id;
    }

    [System.Serializable]
    public class Choice
    {
        /// <summary>
        /// Ответ ИИ который можно выводить на экран.
        /// </summary>
        public Message message;
        public string finish_reason;
        public int index;
        public object logprobs;
    }

    [System.Serializable]
    public class Usage
    {
        /// <summary>
        /// входные токены. Включает в себя память.
        /// </summary>
        public int prompt_tokens;

        /// <summary>
        /// выходные токены.
        /// </summary>
        public int completion_tokens;
        public int total_tokens;
    }
}