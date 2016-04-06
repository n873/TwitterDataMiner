using System;
using System.Threading;
using Twitterizer;

namespace SMDST_Service
{
    class userTweets
    {
        protected string responseFromServer = "";
        protected string json = "";
        protected long minTweetId = 0;
        protected string uri_tweets = "https://api.twitter.com/1.1/statuses/user_timeline.json?count=200&include_rts=1&screen_name=";

        internal long tweetsRequest(string userName, OAuthTokens tokens)
        {
            var t2 = new userTweets();
            var j1 = new jsonFormatter();

            t2.responseFromServer = RequestCall.RequestCallToAPI(uri_tweets, userName, tokens);
            t2.json = j1.jsonFormatter_Tweets(t2.responseFromServer);
            t2.minTweetId = SaveXml.saveXmlFile_Tweets(userName, t2.json);
            return t2.minTweetId;
        }

        internal int getTweets(string[] userNames, OAuthTokens tokens, int requestRate)
        {
            var t1 = new userTweets();
            foreach (string user in userNames)
            {
                var responseCheck = 0;
                long minCheck = 0;

                while (responseCheck < 1)
                {
                    requestRate++;
                    if (requestRate > 50)
                    {
                        Thread.Sleep(new TimeSpan(0, 5, 0));
                        requestRate = 0;
                    }
                    t1.minTweetId = t1.tweetsRequest(user, tokens);
                    responseCheck++;
                }
                while (minCheck != t1.minTweetId)
                {
                    requestRate++;
                    if (requestRate > 50)
                    {
                        Thread.Sleep(new TimeSpan(0, 5, 0));
                        requestRate = 0;
                    }
                    minCheck = t1.minTweetId;
                    t1.minTweetId = t1.tweetsRequest(user, tokens);
                }

                var f1 = new Followers();

                f1.getFollowersInformation(user, tokens);
            }
            return requestRate;
        }
    }
}
