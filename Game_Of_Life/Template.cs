using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Of_Life
{
    public static class Template
    {
        // Returns the file path to the Json file that contains the templates
        public static string GetJsonFilePath()
        {
            try
            {
                var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
                while (directory.Name != "Game_Of_Life")
                {
                    directory = directory.Parent;
                }

                directory = directory.GetDirectories("Resources")[0];

                return directory.GetFiles("templates.json")[0].FullName;
            }
            catch (Exception e)
            {               
                return String.Empty;
            }
        }

        // Saves the new template to the Dictionary of templates
        public static void SaveTemplate(Dictionary<string, bool[,]> templates, string name, bool[,] universe)
        {
            templates.Add(name, universe);
        }

        // Saves the template dictionary to the Json file
        public static void SaveJson(Dictionary<string, bool[,]> templates)
        {
            string templateString = JsonConvert.SerializeObject(templates);
            File.WriteAllText(GetJsonFilePath(), templateString);
        }

        // Loads the Json file of templates into the dictionary
        public static void LoadTemplatesToDictionary(Dictionary<string, bool[,]> templates)
        {
            templates.Clear();
            string templateString = File.ReadAllText(GetJsonFilePath());
            if (templateString != null)
            {
                templates = JsonConvert.DeserializeObject<Dictionary<string, bool[,]>>(templateString);
            }
            else return;
        }

        // Fetches a template from the dictionary
        public static bool[,] GetTemplate(Dictionary<string, bool[,]> templates, string name)
        {
            if (templates.TryGetValue(name, out var ret)) { return ret; }
            else { return null; }
        }

        // Removes a template from the dictionary
        public static void DeleteTemplate(Dictionary<string, bool[,]> templates, string name)
        {
            templates.Remove(name);
        }
    }
}
