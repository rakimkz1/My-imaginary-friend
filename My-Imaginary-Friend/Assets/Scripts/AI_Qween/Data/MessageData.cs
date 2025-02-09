

namespace Scripts.AI_Qween
{


    /// <summary>
    /// Сообщение в диалоге с ИИ.
    /// </summary>
    /// <remarks>
    /// <para><see cref="role"/> определяет, кто отправил сообщение: "user" (пользователь) или "assistant" (ИИ).</para>
    /// <para><see cref="content"/> содержит текст сообщения.</para>
    /// </remarks>
    [System.Serializable]
    public class Message
    {
        /// <summary>
        /// "user" or "assistant" ("system" to configure AI)
        /// </summary>
        public string role;


        public string content;
    }
}