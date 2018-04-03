using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;

namespace SmiqServer.InputFormatters
{
    public class RawInputFormatter : InputFormatter
    {
        public RawInputFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/octet-stream"));
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            using (var stream = new MemoryStream(2048))
            {
                await context.HttpContext.Request.Body.CopyToAsync(stream);

                var bytes = stream.ToArray();

                return await InputFormatterResult.SuccessAsync(bytes);
            }
        }
    }
}
