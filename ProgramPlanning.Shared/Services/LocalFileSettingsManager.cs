using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProgramPlanning.Shared.Services
{
    public class LocalFileSettingsManager<T> : ISettingsManager<T> where T : class
    {
        private readonly string _filePath;

        public LocalFileSettingsManager(string fileName)
        {
            _filePath = GetLocalFilePath(fileName);
        }

        private string GetLocalFilePath(string fileName)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(appData, fileName);
        }

        public T LoadSettings() =>
            File.Exists(_filePath) ?
            JsonSerializer.Deserialize<T>(File.ReadAllText(_filePath)) :
            null;

        public void SaveSettings(T settings)
        {
            string json = JsonSerializer.Serialize<T>(settings);
            File.WriteAllText(_filePath, json);
        }
    }
}
