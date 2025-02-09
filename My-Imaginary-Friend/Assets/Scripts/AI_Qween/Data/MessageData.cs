

namespace Scripts.AI_Qween
{


    /// <summary>
    /// ��������� � ������� � ��.
    /// </summary>
    /// <remarks>
    /// <para><see cref="role"/> ����������, ��� �������� ���������: "user" (������������) ��� "assistant" (��).</para>
    /// <para><see cref="content"/> �������� ����� ���������.</para>
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