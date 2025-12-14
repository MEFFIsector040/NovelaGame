using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CourseProject.Services
{
    public class SaveService
    {
        private const string SaveDirectory = "Saves/";

        public SaveService()
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }
        }

        public List<string> GetSaveFiles()
        {
            if (!Directory.Exists(SaveDirectory))
                return new List<string>();

            return Directory.GetFiles(SaveDirectory, "*.json")
                           .Select(Path.GetFileNameWithoutExtension)
                           .ToList();
        }

        public void DeleteSave(string saveName)
        {
            var filePath = Path.Combine(SaveDirectory, saveName + ".json");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public List<string> GetSaveFilePaths()
        {
            if (!Directory.Exists(SaveDirectory))
                return new List<string>();

            return Directory.GetFiles(SaveDirectory, "*.json").ToList();
        }
    }
}