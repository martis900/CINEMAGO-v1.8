using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
 
namespace CINEMAGO
{
    class IMDb
    {
        public bool status { get; set; }
        public string Id { get; set; }
        public string Rating { get; set; }
        public string ImdbURL { get; set; }
 
        //Search Engine URLs
        private string GoogleSearch = "http://www.google.com/search?q=imdb+";
        private string BingSearch = "http://www.bing.com/search?q=imdb+";
        private string AskSearch = "http://www.ask.com/web?q=imdb+";
 
        //Constructor
        public IMDb(string MovieName, bool GetExtraInfo = true)
        {
            string imdbUrl = getIMDbUrl(System.Uri.EscapeUriString(MovieName));
            status = false;
            if (!string.IsNullOrEmpty(imdbUrl))
            {
                parseIMDbPage(imdbUrl, GetExtraInfo);
            }
        }
 
        //Get IMDb URL from search results
        private string getIMDbUrl(string MovieName, string searchEngine = "google")
        {
            string url = GoogleSearch + MovieName; //default to Google search
            if (searchEngine.ToLower().Equals("bing")) url = BingSearch + MovieName;
            if (searchEngine.ToLower().Equals("ask")) url = AskSearch + MovieName;
            string html = getUrlData(url);
            ArrayList imdbUrls = matchAll(@"<a href=""(http://www.imdb.com/title/tt\d{7}/)"".*?>.*?</a>", html);
            if (imdbUrls.Count > 0)
                return (string)imdbUrls[0]; //return first IMDb result
            else if (searchEngine.ToLower().Equals("google")) //if Google search fails
                return getIMDbUrl(MovieName, "bing"); //search using Bing
            else if (searchEngine.ToLower().Equals("bing")) //if Bing search fails
                return getIMDbUrl(MovieName, "ask"); //search using Ask
            else //search fails
                return string.Empty;
        }
 
        //Parse IMDb page data
        private void parseIMDbPage(string imdbUrl, bool GetExtraInfo)
        {
            string html = getUrlData(imdbUrl+"combined");
            Id = match(@"<link rel=""canonical"" href=""http://www.imdb.com/title/(tt\d{7})/combined"" />", html);
            if (!string.IsNullOrEmpty(Id))
            {
                Rating = match(@"<b>(\d.\d)/10</b>", html);
            }
 
        }
       private string match(string regex, string html, int i = 1)
       {
           return new Regex(regex, RegexOptions.Multiline).Match(html).Groups[i].Value.Trim();
       }
 	 
       //Match all instances and return as ArrayList
       private ArrayList matchAll(string regex, string html, int i = 1)
       {
           ArrayList list = new ArrayList();
           foreach (Match m in new Regex(regex, RegexOptions.Multiline).Matches(html))
               list.Add(m.Groups[i].Value.Trim());
           return list;
       }
 
        //Strip HTML Tags
        static string StripHTML(string inputString)
        {
            return Regex.Replace(inputString, @"<.*?>", string.Empty);
        }
 
        //Get URL Data
        private string getUrlData(string url)
        {
            WebClient client = new WebClient();
            Random r = new Random();
            //Random IP Address
            client.Headers["X-Forwarded-For"] = r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255);
            //Random User-Agent
            client.Headers["User-Agent"] = "Mozilla/" + r.Next(3, 5) + ".0 (Windows NT " + r.Next(3, 5) + "." + r.Next(0, 2) + "; rv:2.0.1) Gecko/20100101 Firefox/" + r.Next(3, 5) + "." + r.Next(0, 5) + "." + r.Next(0, 5);
            Stream datastream = client.OpenRead(url);
            StreamReader reader = new StreamReader(datastream);
            StringBuilder sb = new StringBuilder();
            while (!reader.EndOfStream)
                sb.Append(reader.ReadLine());
            return sb.ToString();
        }
    }
}
