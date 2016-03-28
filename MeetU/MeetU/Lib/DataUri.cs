using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace MeetU.Lib
{
    public class DataUri
    {
        public DataUri(string datauri)
        {
            Regex = new Regex(@"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)", RegexOptions.Compiled);
            Match = Regex.Match(datauri);
            Mime = Match.Groups["mime"].Value;
            Encoding = Match.Groups["encoding"].Value;
            Data = Match.Groups["data"].Value;
        }

        public byte[] ToImage()
        {
            return Convert.FromBase64String(Data);
        }

        public readonly Regex Regex;
        public readonly Match Match;
        public readonly string Mime;
        public readonly string Encoding;
        public readonly string Data;
    }
}