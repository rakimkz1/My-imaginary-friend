using System.Collections.Generic;


namespace Scripts.AI_Qween
{

    /// <summary>
    /// ����� ��� �������� ������� � ��.
    /// </summary>
    [System.Serializable]
    public class RequestData
    {
        /// <summary>
        /// ����� �� �������
        /// </summary>
        public string model = "qwen2.5-72b-instruct";

        /// <summary>
        /// ������ <see cref="messages"/ ������ ���� �� "system". ����������� ������ ������������: "user" � "assistant".>
        /// </summary>
        public List<Message> messages;
    }

}