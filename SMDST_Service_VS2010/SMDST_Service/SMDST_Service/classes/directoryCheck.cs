using System.IO;

namespace SMDST_Service
{
    class directoryCheck
    {
        protected string[] saveDirectories = {
                                                 "C:\\\\SMDST\\Twitter Data\\Tweets\\",
                                                 "C:\\\\SMDST\\Twitter Data\\FollowerLists\\",
                                                 "C:\\\\SMDST\\Twitter Data\\FollowerInfo\\"};

        internal void checkDirectories()
        {
            foreach (string s in saveDirectories)
            {
                if (!Directory.Exists(s))
                    Directory.CreateDirectory(s);
            }
        }

    }
}
