using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5
{
    internal class JSON
    {
        public void JsonFileManager(string chto_to, string f_n)
        {
            if (!File.Exists(chto_to + f_n))
            {
                File.Create(chto_to + f_n);
            }
        }
        public static List<Note> JsonReader(List<Note> notes, string chto_to, string f_n)
        {
            notes = new List<Note>();

            if (!File.Exists(chto_to + f_n))
            {
                new JSON().JsonFileManager(chto_to, f_n);
            }
            else
            {
                string jsonString = File.ReadAllText(chto_to + f_n);
                if (jsonString.Length != 0)
                {
                    notes = JsonConvert.DeserializeObject<List<Note>>(jsonString);
                }
            }
            return notes;
        }

        public static void JsonWriter(string chto_to, string f_n, List<Note> notesJson)
        {
            if (!File.Exists(chto_to + f_n))
            {
                new JSON().JsonFileManager(chto_to, f_n);
            }
            else
            {
                string jsonString = JsonConvert.SerializeObject(notesJson);
                File.WriteAllText(chto_to + f_n, jsonString);
            }
        }
    }
}
