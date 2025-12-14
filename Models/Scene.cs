using System;
using System.Collections.Generic;

namespace CourseProject.Models
{
    public class Scene
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string BackgroundMusic { get; set; }
        public bool IsEnding { get; set; }
        public List<Choice> Choices { get; set; }

        public Scene()
        {
            Choices = new List<Choice>();
        }
    }

    public class Choice
    {
        public string Text { get; set; }
        public int NextSceneId { get; set; }
        public Dictionary<string, int> Effects { get; set; }
        public string RequiredStat { get; set; }
        public int RequiredValue { get; set; }

        public Choice()
        {
            Effects = new Dictionary<string, int>();
        }
    }
}