using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmiqServer.InputFormatters
{
    public class HexInputFormatter : InputFormatter
    {
        public HexInputFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/x-hex"));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            using (var reader = new StreamReader(context.HttpContext.Request.Body))
            {
                var hex = await reader.ReadToEndAsync();

                if (hex.Length % 2 == 1)
                {
                    return await InputFormatterResult.FailureAsync();
                }

                var bytes = ToByteArray(hex);
                
                return await InputFormatterResult.SuccessAsync(bytes);
            }
        }

        private static byte[] ToByteArray(string hex)
        {
            var result = new byte[hex.Length / 2];

            for (var i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(hex.Substring(2 * i, 2), 16);
            }

            return result;
        }
    }
}
