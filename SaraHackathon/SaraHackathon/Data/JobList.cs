
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using RestSharp;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;

namespace SaraHackathon.Data
{
    public class JobList
    {
        public Dictionary<string, object> Score { get; set; }
        public Dictionary<string, object> Skills { get; set; }
    }


    public class JobOpeningReader
    {
        [HttpGet()]
        public JobList LoadJobs(string skills)
        {
            JobList jobList = new JobList();
            HttpClient http = new HttpClient();
        //    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "Your Oauth token");
            //var data = http.PostAsync("http://10.148.147.221:3134/rleventtech/"+skills, new StringContent("Data", Encoding.UTF32, "text/json")).Result.Content.ReadAsStringAsync().Result;

            //for calling get

            var data = http.GetAsync("http://10.148.147.221:3134/rleventtech/" + skills).Result.Content.ReadAsStringAsync().Result;
            jobList = JsonConvert.DeserializeObject<JobList>(data);
            return jobList;
        }
        public Dictionary<int, string> JOReader()
        {
            var jobOpenings = LoadJobs("C++,Java");
            //  Dictionary<int, double> usableScore = new Dictionary<int, double>();
            Dictionary<int, string> usableSkills = new Dictionary<int, string>();
            foreach (var item in jobOpenings.Score)
            {
                if (double.Parse(item.Value.ToString()) != 0)
                {
                    if (jobOpenings.Skills.ContainsKey(item.Key))
                        usableSkills.Add(Int32.Parse(item.Key.ToString()), jobOpenings.Skills[item.Key.ToString()].ToString());
                    
                }
                else continue;
            }

            return usableSkills;
        }
    }
}
