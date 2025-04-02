using System.Collections.Generic;

namespace Scripts.AI_Qween
{
    [System.Serializable]
    public class Memory
    {
        public List<string> long_term_memory;
        public List<Dialogue> short_term_memory;
    }


    [System.Serializable]
    public class Dialogue
    {
        public string player_said;
        public string my_reaction_and_responce;
    }
}