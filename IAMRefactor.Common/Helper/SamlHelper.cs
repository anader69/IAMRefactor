using IAMRefactor.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace IAMRefactor.Common.Helper
{
    public static class SamlHelper
    {

        public static DateTime? GetIssueInstant()
        {
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year;
            // dateTime = DateTime.Now;
            int month = dateTime.Month;
            // dateTime = DateTime.Now;
            int day = dateTime.Day;
            // dateTime = DateTime.Now;
            int hour = dateTime.Hour;
            //dateTime = DateTime.Now;
            int minute = dateTime.Minute;
            //dateTime = DateTime.Now;
            int second = dateTime.Second;
            //dateTime = DateTime.Now;
            int millisecond = dateTime.Millisecond;
            dateTime = new DateTime(year, month, day, hour, minute, second, millisecond);

            //return new DateTime?(dateTime.AddHours(-3.0));
            return new DateTime?(dateTime);
        }


        public static bool IsNotNullOrEmpty(this string value)
        {
            return !string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Serializes an item to an XML string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns>string</returns>
        public static string SerializeToXmlString<T>(T item)
        {
            var stream = new MemoryStream();
            Serialize(item, stream);

            var reader = new StreamReader(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return reader.ReadToEnd();
        }
        /// <summary>
        /// Gets the instance of XmlSerializerNamespaces that is used by this class.
        /// </summary>
        /// <value>The XmlSerializerNamespaces instance.</value>
        public static XmlSerializerNamespaces XmlNamespaces { get; }
        /// <summary>
        /// Serializes the specified item to a stream.
        /// </summary>
        /// <typeparam name="T">The items type</typeparam>
        /// <param name="item">The item to serialize.</param>
        /// <param name="stream">The stream to serialize to.</param>
        public static void Serialize<T>(T item, Stream stream)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(stream, item, XmlNamespaces);
            stream.Flush();
        }


        public static void AddMessageParameter(this StringBuilder result, string request, string response)
        {
            if (!(response == null || request == null))
            {
                throw new Exception("Request or Response property MUST be set.");
            }

            string value;
            if (request != null)
            {
                result.AppendFormat("{0}=", HttpRedirectBindingConstants.SamlRequest);
                value = request;
            }
            else
            {
                result.AppendFormat("{0}=", HttpRedirectBindingConstants.SamlResponse);
                value = response;
            }

            var encoded = value.DeflateEncode();
            var urlEncoded = encoded.UrlEncode();
            result.Append(urlEncoded.UpperCaseUrlEncode());
        }


        /// <summary>
        ///     If the RelayState property has been set, this method adds it to the query string.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="relayState"></param>
        /// <param name="request"></param>
        public static void AddRelayState(this StringBuilder result, string request, string relayState)
        {
            if (relayState == null)
            {
                return;
            }

            result.Append("&RelayState=");

            // Encode the relay state if we're building a request. Otherwise, append unmodified.
            result.Append(request != null ? relayState.DeflateEncode().UrlEncode() : relayState);
        }

        /// <summary>
        ///     Uppercase the URL-encoded parts of the string. Needed because Ping does not seem to be able to handle lower-cased
        ///     URL-encodings.
        /// </summary>
        public static string UpperCaseUrlEncode(this string value)
        {
            var result = new StringBuilder(value);
            for (var i = 0; i < result.Length; i++)
            {
                if (result[i] == '%')
                {
                    result[++i] = char.ToUpper(result[i]);
                    result[++i] = char.ToUpper(result[i]);
                }
            }

            return result.ToString();
        }

        public static string UrlEncode(this string value)
        {
            return HttpUtility.UrlEncode(value);
        }

        public static string DeflateEncode(this string value)
        {
            var memoryStream = new MemoryStream();
            using (var writer = new StreamWriter(
                new DeflateStream(memoryStream, CompressionMode.Compress, true),
                new UTF8Encoding(false)))
            {
                writer.Write(value);
                writer.Close();
                return Convert.ToBase64String(
                    memoryStream.GetBuffer(),
                    0,
                    (int)memoryStream.Length,
                    options: Base64FormattingOptions.None);
            }
        }

    }
}
