using Leaf.xNet;

namespace Flock
{
    public class Utils
    {
        public static HttpRequest createRequest(CookieStorage cookies)
        {
            var request = new HttpRequest();

            request.Cookies = cookies;

            request.IgnoreInvalidCookie = true;
            request.IgnoreProtocolErrors = true;

            request.AcceptEncoding = "gzip, deflate, br";
            request.Username =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36";
            request["Accept"] =
                "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7";
            request["Accept-Language"] = "ja,en-US;q=0.9,en;q=0.8";
            request["Sec-Ch-Ua"] = "\"Google Chrome\";v=\"119\", \"Chromium\";v=\"119\", \"Not?A_Brand\";v=\"24\"";
            request["Sec-Ch-Ua-Mobile"] = "?0";
            request["Sec-Ch-Ua-Platform"] = "\"Windows\"";
            request["Sec-Fetch-Dest"] = "document";
            request["Sec-Fetch-Mode"] = "navigate";
            request["Sec-Fetch-Site"] = "none";
            request["Sec-Fetch-User"] = "?1";
            request["Upgrade-Insecure-Requests"] = "1";

            return request;
        }
    }
}