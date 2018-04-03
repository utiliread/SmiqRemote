using System;
using System.Text;

namespace SmiqServer
{
    /// <summary>
    /// §3.4.5
    /// </summary>
    public static class BlockData
    {
        public static byte[] Encode(ReadOnlySpan<byte> data)
        {
            var lengthLength = data.Length.ToString().Length;
            var header = string.Format("#{0}{1}", lengthLength, data.Length);

            var encoded = new byte[header.Length + data.Length + 1];

            Encoding.ASCII.GetBytes(header).CopyTo(encoded, 0);
            data.CopyTo(encoded.AsSpan().Slice(header.Length));
            encoded[header.Length + data.Length] = (byte)'\n';

            return encoded;
        }

        public static int DecodeLength(ReadOnlySpan<byte> data)
        {
            if (data[0] != '#')
            {
                throw new ArgumentException();
            }

            var lengthLength = data[1] - '0';
            var dataLength = int.Parse(Encoding.ASCII.GetString(data.Slice(2, lengthLength).ToArray()));
            var headerLength = 2 + lengthLength;

            return headerLength + dataLength + 1;
        }

        public static byte[] Decode(ReadOnlySpan<byte> data)
        {
            if (data.Length != DecodeLength(data))
            {
                throw new ArgumentException();
            }

            var lengthLength = data[1] - '0';
            var headerLength = 2 + lengthLength;

            return data.Slice(headerLength, data.Length - headerLength - 1).ToArray();
        }
    }
}
