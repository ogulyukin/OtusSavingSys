using System;
using System.IO;
using UnityEngine;

namespace SaveSystem.FileSaverSystem
{
    public class Saver
    {
        private readonly string filename;

        public Saver(string filename)
        {
            this.filename = filename;
        }

        public void Save(string[] data)
        {
            if(File.Exists(filename))
            {
                File.Delete(filename);
            }
        
            try
            {
                StreamWriter sw = new StreamWriter(filename);
                foreach (var entry in data)
                {
                    sw.WriteLine(entry);
                }
                sw.Close();
            }
            catch(Exception e)
            {
                Debug.Log($"Exception: {e.Message}");
            }
        }
    }
}