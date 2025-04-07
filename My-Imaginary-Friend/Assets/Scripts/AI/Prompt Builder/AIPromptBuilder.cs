﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;


namespace Scripts.AI_Qween
{
    public class AIPromptBuilder : MonoBehaviour
    {
        public PromptSO defaultPrompt;
        public PromptSO f_meetingPrompt;

        public string player_name = string.Empty;

        public JsonResponceVariety currentJsonVariety;

        private PromptSO prompt;
        /*
        private Memory memory = new Memory()
        {
            long_term_memory = null,
            short_term_memory = null
        };*/


        public void Awake()
        {
            prompt = ScriptableObject.CreateInstance<PromptSO>();
            ChooseJsonVariety(JsonResponceVariety.Meeting);
        }


        public void HistoryAdd(string player, string ai)
        {
            Dialogue dialogue = new Dialogue()
            {
                player_said = player,
                my_reaction_and_responce = ai
            };

            prompt.memory.short_term_memory.Add(dialogue);
        }

        public void ChooseJsonVariety(JsonResponceVariety variety)
        {
            currentJsonVariety = variety;

            switch (variety)
            {
                case JsonResponceVariety.Default:
                    prompt.CopyFrom(defaultPrompt);
                    break;

                case JsonResponceVariety.Meeting:
                    prompt.CopyFrom(f_meetingPrompt);
                    break;

                default:
                    prompt.CopyFrom(defaultPrompt);
                    break;
            }
        }

        private string GetPrompt()
        {
            return JsonUtility.ToJson(prompt);
        }

        public string ToString(string playerRequest)
        {
            RequestData request = new RequestData()
            {
                messages = new List<Message>
                {
                    new Message()
                    {
                        role = "system",
                        content = GetPrompt()
                    },
                    new Message()
                    {
                        role = "user",
                        content = playerRequest
                    }
                }
            };

            return JsonUtility.ToJson(request);
        }
    }

}