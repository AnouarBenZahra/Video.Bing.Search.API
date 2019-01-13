using System;
using System.Collections.Generic;
using System.Text;

namespace Video.Bing.Search.API.Helpers
{
    public class JsonTools
    {
        public static string ToJson(string json)
        {
            if (string.IsNullOrEmpty(json))
                return string.Empty;

            json = json.Replace(Environment.NewLine, "").Replace("\t", "");

            StringBuilder sb = new StringBuilder();
            bool quote = false;
            bool ignore = false;
            int offset = 0;
            int indentLength = 3;

            foreach (char item in json)
            {
                switch (item)
                {
                    case '"':
                        if (!ignore) quote = !quote;
                        break;
                    case '\'':
                        if (quote) ignore = !ignore;
                        break;
                }

                if (quote)
                    sb.Append(item);
                else
                {
                    switch (item)
                    {
                        case '{':
                        case '[':
                            sb.Append(item);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', ++offset * indentLength));
                            break;
                        case '}':
                        case ']':
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', --offset * indentLength));
                            sb.Append(item);
                            break;
                        case ',':
                            sb.Append(item);
                            sb.Append(Environment.NewLine);
                            sb.Append(new string(' ', offset * indentLength));
                            break;
                        case ':':
                            sb.Append(item);
                            sb.Append(' ');
                            break;
                        default:
                            if (item != ' ') sb.Append(item);
                            break;
                    }
                }
            }

            return sb.ToString().Trim();
        }
    }
}
