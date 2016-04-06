using System;
using System.Text;
using System.Threading;
using Twitterizer;

namespace SMDST_Service
{
    class Followers
    {
        protected string json;
        protected int requestRate = 0;
        protected string responseFromServer;
        protected string[] followerIds;
        protected string uri_followers = "https://api.twitter.com/1.1/followers/ids.json?screen_name=";
        protected string uri_followersInfo = "https://api.twitter.com/1.1/users/lookup.json?user_id=";

        internal string[] getFollowers(string userName, OAuthTokens tokens)
        {
            var f1 = new Followers();
            var rc1 = new RequestCall();
            var jf1 = new jsonFormatter();

            f1.responseFromServer = RequestCall.RequestCallToAPI(f1.uri_followers, userName, tokens);
            f1.json = jf1.jsonFormatter_Followers(f1.responseFromServer);
            f1.followerIds = f1.responseFromServer.Substring(8, f1.responseFromServer.IndexOf("]") - 8).Split(',');

            SaveXml.saveXmlFile_Followers(userName, f1.json);

            return f1.followerIds;
        }

        internal string pass100ids(int iterator, int start, int loopBound)
        {


            requestRate++;
            if (requestRate > 10)
            {
                Thread.Sleep(new TimeSpan(0, 5, 0));
                requestRate = 0;
            }

            var pass100Ids = "";
            var builder = new StringBuilder();
            builder.Append(pass100Ids);
            for (iterator = start; iterator < loopBound; iterator++)
            {
                builder.Append(this.followerIds[iterator] + ",");
            }
            pass100Ids = builder.ToString();

            return pass100Ids;
        }

        internal void getFollowersInformation(string userName, OAuthTokens tokens)
        {
            var f2 = new Followers();
            var rc2 = new RequestCall();
            var jf2 = new jsonFormatter();

            f2.followerIds = f2.getFollowers(userName, tokens);
            int maxValue = f2.followerIds.Length;
            var start = 0;
            var iterator = 0;
            var loopBound = 100;
            var iterateCheck = 0;
            string pass100Ids;

            while ((start < maxValue) && (iterateCheck != -1))
            {
                if (loopBound > maxValue && iterateCheck == 0)
                {
                    loopBound = maxValue;
                }


                pass100Ids = f2.pass100ids(iterator, start, loopBound);
                pass100Ids = pass100Ids.Substring(0, pass100Ids.Length - 1);

                f2.responseFromServer = RequestCall.RequestCallToAPI(f2.uri_followersInfo, pass100Ids, tokens);
                f2.json = jf2.jsonFormatter_FollowersInformation(f2.responseFromServer);
                SaveXml.saveXmlFile_FollowersInformation(userName, f2.json, start, loopBound);

                iterateCheck = 1;
                if (loopBound > 99)
                {
                    int loopBoundTemp = loopBound;
                    start += 99;
                    loopBound += 99;
                    if (loopBound > maxValue)
                    {
                        loopBound = maxValue - 1;
                    }
                    if (start > maxValue)
                    {

                        start -= 99;
                        var add = 99 + (start - maxValue);
                        start += add;
                        if (start > maxValue)
                        {
                            start = loopBoundTemp;
                            ++loopBound;
                        }
                        else { ++loopBound; }

                        iterateCheck = -1;

                        pass100Ids = f2.pass100ids(iterator, start, loopBound);
                        pass100Ids = pass100Ids.Substring(0, pass100Ids.Length - 1);
                        f2.responseFromServer = RequestCall.RequestCallToAPI(f2.uri_followersInfo, pass100Ids, tokens);
                        f2.json = jf2.jsonFormatter_FollowersInformation(f2.responseFromServer);
                        SaveXml.saveXmlFile_FollowersInformation(userName, f2.json, start, loopBound);
                    }
                }
                else
                {
                    start = maxValue;
                }
            }

        }
    }
}
