using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ASCIICrawler {
	class PageParser {

		public List<String> Parse(String Content) {
			Tallyer.PagesTotal++;
			Console.WriteLine("   Length: " + Content.Length);
			Tallyer.CharsTotal += Convert.ToUInt64(Content.Length);
			String ASCIIContent = Regex.Replace(Content, @"[^\u0000-\u007F]", String.Empty);
			Tallyer.CharsASCII += Convert.ToUInt64(ASCIIContent.Length);
			if (Content.Length == ASCIIContent.Length) {
				Tallyer.PagesASCIIOnly++;
			}
			Double Percent = (Convert.ToDouble(ASCIIContent.Length) / Content.Length);
            Console.WriteLine("   ASCII Chars: {0} ({1:P5})", ASCIIContent.Length, Percent);
			//Regex linkParser = new Regex(@"\b(?:https?://|www\.)[^ \f\n\r\t\v\]]+\b", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			Regex linkParser = new Regex(@"((((http|https):(?:\/\/)?)(?:[\-;:&=\+\$,\w]+@)?[A-Za-z0-9\.\-]+|(?:www\.|[\-;:&=\+\$,\w]+@)[A-Za-z0-9\.\-]+)((?:\/[\+~%\/\.\w\-_]*)?\??(?:[\-\+=&;%@\.\w_]*)#?(?:[\.\!\/\\\w]*))?)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			List<String> URLs = new List<String>();
            foreach (Match m in linkParser.Matches(Content)) {
				URLs.Add(m.Value);
            }
			Console.WriteLine("      URLs Found: {0}", URLs.Count);
			return URLs;
		}
	}
}
