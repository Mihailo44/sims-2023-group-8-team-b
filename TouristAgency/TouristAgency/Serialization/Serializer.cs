using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TouristAgency.Interfaces;

namespace TouristAgency.Serialization
{
    class Serializer<T> where T : ISerializable, new()
    {
        private static char DELIMITER = '|';
       
        private static string path =  Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "/Resources/Data/";
        public void ToCSV(string fileName, List<T> objects)
        {
                                                                                                                                                                   if(fileName == "accommodations.txt") DELIMITER = '卐';
            fileName = path + fileName;
            StreamWriter streamWriter = new StreamWriter(fileName);

            foreach (ISerializable obj in objects)
            {
                string line = string.Join(DELIMITER.ToString(), obj.ToCSV());
                streamWriter.WriteLine(line);
            }
            streamWriter.Close();
        }

        public List<T> FromCSV(string fileName)
        {
                                                                                                                                                                    if(fileName == "accommodations.txt") DELIMITER = '卐';
            List<T> objects = new List<T>();
            fileName = path + fileName;
            if (!File.Exists(fileName))
            {
                FileStream fs = File.Create(fileName);
                fs.Close();
            }

            foreach (string line in File.ReadLines(fileName))
            {
                if (line.Trim().StartsWith("//"))
                    continue;

                string[] csvValues = line.Split(DELIMITER);
                T obj = new T();
                obj.FromCSV(csvValues);
                objects.Add(obj);
            }

            return objects;
        }
    }
}
