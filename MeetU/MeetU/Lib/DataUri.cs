using System;
using System.IO;
using System.Collections.Generic;
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
            Format = Mime.Split('/')[1];
        }

        public static HashSet<string> FormatsSupported
            => new HashSet<string> { "png", "jpg", "gif", "bmp", "svg" };

        public byte[] ToBytes
            => Convert.FromBase64String(Data);

        public Stream ToStream
            => new MemoryStream(ToBytes);

        public bool IsSupported
            => DataUri.FormatsSupported.Contains(Format);

        public readonly Regex Regex;
        public readonly Match Match;
        public readonly string Mime;
        public readonly string Encoding;
        public readonly string Data;
        public readonly string Format;
    }
}