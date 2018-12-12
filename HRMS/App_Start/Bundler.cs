using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.App_Start
{
    public static class Bundler
    {
        const string FILENAME = "bundleconfig.json";

        private static string s_config = "";

        public static HtmlString RenderScripts(string bundlePath, bool expandBundle = true)
        {
            if (expandBundle)
            {
                string baseFolder = "http://10.27.1.28/RES/";
                var configFile = Path.Combine(baseFolder, FILENAME);
                var bundles = GetBundles(configFile);
                var bundle = (from b in bundles where b.OutputFileName.Equals(bundlePath.Replace("/RES/", ""), StringComparison.InvariantCultureIgnoreCase) select b).FirstOrDefault();
                if (bundle == null)
                {
                    if (bundlePath.EndsWith(".css"))
                    {
                        return new HtmlString(string.Format("<link href='http://10.27.1.28/RES/{0}' rel='stylesheet'></link>", bundlePath.Replace("/RES/", "")));
                    }
                    else
                    {
                        return new HtmlString(string.Format("<script src='http://10.27.1.28/RES/{0}' type='text/javascript'></script>", bundlePath.Replace("/RES/", "")));
                    }
                }

                var sb = new StringBuilder();
                var inputFiles = GetBundleInputFiles(baseFolder, bundle);
                foreach (var inFile in inputFiles)
                {
                    var fullPath = string.Format("http://10.27.1.28/RES/{0}", inFile);
                    if (bundlePath.EndsWith(".css"))
                    {
                        sb.AppendLine(string.Format("<link href='{0}' rel='stylesheet'></link>", fullPath));
                    }
                    else
                    {
                        sb.AppendLine(string.Format("<script src='{0}' type='text/javascript'></script>", fullPath));
                    }
                }
                return new HtmlString(sb.ToString());
            }
            else
            {
                if (bundlePath.EndsWith(".css"))
                {
                    return new HtmlString(string.Format("<link href='http://10.27.1.28/RES/{0}' rel='stylesheet'></link>", bundlePath.Replace("/RES/", "")));
                }
                else
                {
                    return new HtmlString(string.Format("<script src='http://10.27.1.28/RES/{0}' type='text/javascript'></script>", bundlePath.Replace("/RES/", "")));
                }
            }
        }


        static List<string> GetBundleInputFiles(string baseFolder, Bundle bundle)
        {
            List<string> inputFiles = new List<string>();
            string ext = Path.GetExtension(bundle.OutputFileName);
            foreach (string inFile in bundle.InputFiles)
            {
                string fullPath = Path.Combine(baseFolder, inFile);

                if (Directory.Exists(fullPath))
                {
                    DirectoryInfo dir = new DirectoryInfo(fullPath);
                    var files = dir.GetFiles("*" + ext, SearchOption.TopDirectoryOnly);
                    inputFiles.AddRange(files.Select(f => string.Format("{0}/{1}", inFile, f.Name)));
                }
                else
                {
                    inputFiles.Add(inFile);
                }
            }
            return inputFiles;
        }

        static IEnumerable<Bundle> GetBundles(string configFile)
        {
            Uri uri = new Uri(configFile);

            if (uri.Host.Equals(System.String.Empty) == true)
            {
                FileInfo file = new FileInfo(configFile);

                if (!file.Exists)
                    return Enumerable.Empty<Bundle>();

                string content = File.ReadAllText(configFile);
                return JsonConvert.DeserializeObject<IEnumerable<Bundle>>(content);

            }
            else
            {
                if (string.IsNullOrEmpty(s_config))
                {
                    System.Net.WebClient wc = new System.Net.WebClient();
                    s_config = Encoding.UTF8.GetString(wc.DownloadData(configFile));
                }

                return JsonConvert.DeserializeObject<IEnumerable<Bundle>>(s_config);
            }
        }

    }

    class Bundle
    {
        [JsonProperty("outputFileName")]
        public string OutputFileName { get; set; }

        [JsonProperty("inputFiles")]
        public List<string> InputFiles { get; set; } = new List<string>();

        [JsonProperty("minify")]
        public Dictionary<string, object> Minify { get; set; } = new Dictionary<string, object> { { "enabled", true } };

        [JsonProperty("includeInProject")]
        public bool IncludeInProject { get; set; }

        [JsonProperty("sourceMaps")]
        public bool SourceMaps { get; set; }
    }
}
