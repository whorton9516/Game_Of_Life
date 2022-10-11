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
        public static Dictionary<string, bool[,]> LoadTemplatesToDictionary(Dictionary<string, bool[,]> templates)
        {
            templates.Clear();
            string templateString = File.ReadAllText(GetJsonFilePath());
            if (templateString != null)
            {
                templates = JsonConvert.DeserializeObject<Dictionary<string, bool[,]>>(templateString);
                return templates;
            }
            else return null;
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

        // Checks if there is an existing template with the same name
        public static bool CheckForTemplateName(Dictionary<string, bool[,]> templates, string name)
        {
            if (templates.ContainsKey(name)) { return true; }
            return false;
        }

        // Adds frame template to the json file
        public static void AddFrameTemplate(Dictionary<string, bool[,]> templates, int cols, int rows)
        {
            bool[,] grid = new bool[cols, rows];
            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1)
                    {
                        grid[x, y] = true;
                    }
                }
            }
            if(templates.ContainsKey("frame")) { templates.Remove("frame"); }
            templates.Add("frame", grid);
        }
    }
}
