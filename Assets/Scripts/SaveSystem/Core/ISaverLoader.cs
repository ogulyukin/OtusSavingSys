using System.Collections.Generic;

namespace SaveSystem.Core
{
    public interface ISaverLoader
    {
        public void Save(List<Dictionary<string, string>> data);
        public List<Dictionary<string, string>> Load();
        
    }
}
