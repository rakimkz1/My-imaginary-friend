

namespace Scripts.AI_Qween
{
    /// <summary>
    /// ����� ��� ��������� ������� �� ��.
    /// </summary>
    [System.Serializable]
    public class ApiResponse
    {

        /// <summary>
        /// �������� �������. ������ �� ������ 1.
        /// </summary>
        public Choice[] choices;
        public string @object;

        /// <summary>
        /// ������ � ���� ������� ������� ���� ������������.
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
        /// ����� �� ������� ����� �������� �� �����.
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
        /// ������� ������. �������� � ���� ������.
        /// </summary>
        public int prompt_tokens;

        /// <summary>
        /// �������� ������.
        /// </summary>
        public int completion_tokens;
        public int total_tokens;
    }
}