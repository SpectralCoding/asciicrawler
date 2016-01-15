using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ASCIICrawler {
	class PageRequester {

		public List<String> Request(String URL) {
			Console.WriteLine("Load URL: " + URL);
			PageParser pp = new PageParser();
			String pageContent = String.Empty;
			try {
				using (MyClient client = new MyClient()) {
					client.HeadOnly = true;
					byte[] body = client.DownloadData(URL); // note should be 0-length
					string type = client.ResponseHeaders["content-type"];
					client.HeadOnly = false;
					// check 'tis not binary... we'll use text/, but could
					// check for text/html
					if (type.StartsWith(@"text/")) {
						pageContent = client.DownloadString(URL);
					}
				}
			} catch (Exception) { }
			if (pageContent != String.Empty) {
				return pp.Parse(pageContent);
			} else {
				return new List<String>();
			}
		}
	}

	class MyClient : WebClient {
		public bool HeadOnly { get; set; }
		protected override WebRequest GetWebRequest(Uri address) {
			WebRequest req;
			req = base.GetWebRequest(address);
			if (HeadOnly && req.Method == "GET") {
				req.Method = "HEAD";
			}
			return req;
		}
	}

}
