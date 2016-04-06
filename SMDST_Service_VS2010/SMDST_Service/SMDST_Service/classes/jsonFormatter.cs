using System;
using System.Text;

namespace SMDST_Service
{
    class jsonFormatter
    {
        protected string json = "";

        internal string jsonFormatter_Tweets(string responseFromServer)
        {
            try
            {
                json = @"{
					'?xml': {
						'@version': '1.0',
						'@encoding': 'utf-8'
					},
					'tweets': {
				'tweet': [";

                string Normalized = responseFromServer.Normalize(NormalizationForm.FormKD);
                json += Normalized + "]}}";

                return json;
            }
            catch (FormatException fex)
            { return fex.ToString(); }
            catch (NullReferenceException nrex)
            { return nrex.ToString(); }
            catch (ArgumentException aex)
            { return aex.ToString(); }
            catch (Exception ex)
            { return ex.ToString(); }
        }

        internal string jsonFormatter_Followers(string responseFromServer)
        {
            try
            {
                json = @"{
				   '?xml': {
					   '@version': '1.0',
					   '@encoding': 'utf-8'
				   },
				   'followers': ";

                string Normalized = responseFromServer.Normalize(NormalizationForm.FormKD);
                json += Normalized + "}";

                return json;
            }
            catch (FormatException fex)
            { return fex.ToString(); }
            catch (NullReferenceException nrex)
            { return nrex.ToString(); }
            catch (ArgumentException aex)
            { return aex.ToString(); }
            catch (Exception ex)
            { return ex.ToString(); }
        }

        internal string jsonFormatter_FollowersInformation(string responseFromServer)
        {
            try
            {
                json = @"{
					'?xml': {
						'@version': '1.0',
						'@encoding': 'utf-8'
					},
					'followersInfo': {
						'followerInfo': [";
                string Normalized_followersInfo = responseFromServer.Normalize(NormalizationForm.FormKD);
                json += Normalized_followersInfo + "]}}";

                return json;
            }
            catch (FormatException fex)
            { return fex.ToString(); }
            catch (NullReferenceException nrex)
            { return nrex.ToString(); }
            catch (ArgumentException aex)
            { return aex.ToString(); }
            catch (Exception ex)
            { return ex.ToString(); }
        }
    }
}
