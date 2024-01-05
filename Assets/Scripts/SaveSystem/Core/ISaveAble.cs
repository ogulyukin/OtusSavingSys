using System.Collections.Generic;

namespace SaveSystem.Core
{
    public interface ISaveAble
    {
        List<Dictionary<string, string>> CaptureState();
        void RestoreState(List<Dictionary<string, string>> loadedData);
    }
}
