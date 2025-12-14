using System;
using System.Collections.Generic;

namespace CourseProject.Models
{
    public class GameState
    {
        public int CurrentSceneId { get; set; }
        public Stack<int> SceneHistory { get; set; }
        public List<int> VisitedScenes { get; set; }
        public Dictionary<string, bool> UnlockedEndings { get; set; }
        public List<string> Inventory { get; set; }

        public GameState()
        {
            CurrentSceneId = 1;
            SceneHistory = new Stack<int>();
            VisitedScenes = new List<int>();
            UnlockedEndings = new Dictionary<string, bool>();
            Inventory = new List<string>();
        }
    }
}