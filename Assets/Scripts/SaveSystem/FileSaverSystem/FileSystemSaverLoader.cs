using System.Collections.Generic;
using Newtonsoft.Json;
using SaveSystem.Core;

namespace SaveSystem.FileSaverSystem
{
    public class FileSystemSaverLoader : ISaverLoader
    {
        private const string Filename = "MySaveGame.sav";
        private readonly Reader reader;
        private readonly Saver saver;
        private readonly AesEncryptionProvider encryptionProvider = new();

        public FileSystemSaverLoader()
        {
            reader = new Reader(Filename);
            saver = new Saver(Filename);
        }
        
        public void Save(List<Dictionary<string, string>> data)
        {
            var strList = new List<string>();
            foreach (var obj in data)
            {
                var str = JsonConvert.SerializeObject(obj);
                strList.Add(encryptionProvider.AesEncryption(str));
            }
            saver.Save(strList.ToArray());
        }

        public List<Dictionary<string, string>> Load()
        {
            var result = new List<Dictionary<string, string>>();
            if (!reader.IsSaveFileExist())
            {
                return result;
            }

            var loadedData = reader.Load();
            foreach (var data in loadedData)
            {
                var obj = JsonConvert.DeserializeObject<Dictionary<string, string>>(encryptionProvider.AesDecryption(data));
                if(obj != null) result.Add(obj);
            }
            return result;
        }
    }
}
