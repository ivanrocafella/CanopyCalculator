﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Utils
{
    public static class FileAction<T> where T : class
    {
        private static string Serialize(T t) => JsonConvert.SerializeObject(t, Formatting.Indented);
        private static T Deserialize(string json) => JsonConvert.DeserializeObject<T>(json);

        public static void WriteAndSerialyze(string path, T t)
        {
            List<T> list = new();
            if (File.Exists(path))
            {
                string content = File.ReadAllText(path);
                if (!string.IsNullOrEmpty(content))
                    list = JsonConvert.DeserializeObject<List<T>>(content);
            }           
            list.Add(t);
            string json = JsonConvert.SerializeObject(list, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public static List<T> ReadAndDeserialyze(string path)
        {
            List<T> list = new();
            if (File.Exists(path))
            {
                string existContent = File.ReadAllText(path);
                list = JsonConvert.DeserializeObject<List<T>>(existContent);
            }
            return list;
        }
    }
}
