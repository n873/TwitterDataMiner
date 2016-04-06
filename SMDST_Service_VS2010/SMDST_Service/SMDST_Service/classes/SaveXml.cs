using System.Xml;
using Newtonsoft.Json;

namespace SMDST_Service
{
    class SaveXml
    {
        protected string saveDirectory_Tweets = "C:\\\\SMDST\\Twitter Data\\Tweets\\";
        protected string saveDirectory_FollowerLists = "C:\\\\SMDST\\Twitter Data\\FollowerLists\\";
        protected string saveDirectory_FollowerInfo = "C:\\\\SMDST\\Twitter Data\\FollowerInfo\\";
        protected string xmlFilename;
        protected XmlDocument doc = new XmlDocument();

        internal static long saveXmlFile_Tweets(string username, string json)
        {
            long minTweetId;
            var sx1 = new SaveXml
            {
                doc = (XmlDocument)JsonConvert.DeserializeXmlNode(json)
            };
            minTweetId = long.Parse(sx1.doc.SelectSingleNode(@"//tweet/id[not(.
                                >=../preceding-sibling::tweet/id) and not (. >=../following-sibling::tweet/id)]").InnerText);
            sx1.xmlFilename = username + "_Tweets_minID_" + minTweetId.ToString()
                                    + ".xml";
            sx1.doc.PreserveWhitespace = true;
            sx1.doc.Save(sx1.saveDirectory_Tweets.ToString() + "\\" + sx1.xmlFilename);
            return minTweetId;
        }

        internal static void saveXmlFile_Followers(string userName, string json)
        {
            var sx2 = new SaveXml
            {
                doc = (XmlDocument)JsonConvert.DeserializeXmlNode(json),
                xmlFilename = userName + "_listOfFollowersIds.xml"
            };
            sx2.doc.PreserveWhitespace = true;
            sx2.doc.Save(sx2.saveDirectory_FollowerLists.ToString() + "\\" + sx2.xmlFilename);
        }

        internal static void saveXmlFile_FollowersInformation(string userName, string json, int start, int loopBound)
        {
            var sx3 = new SaveXml
            {
                doc = (XmlDocument)JsonConvert.DeserializeXmlNode(json),
                xmlFilename = userName + "_Followers_" + start.ToString() + "to" + loopBound.ToString() + "_Info.xml"
            };
            sx3.doc.PreserveWhitespace = true;
            sx3.doc.Save(sx3.saveDirectory_FollowerInfo.ToString() + "\\" + sx3.xmlFilename);
        }
    }
}
