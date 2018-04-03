using System;
using System.ComponentModel.DataAnnotations;

namespace SmiqServer.Features.DigitalModulation
{
    public class ControlSignalDto
    {
        [Required]
        [Range(1, 0x4000000)]
        public int? Symbol { get; set; }
        public bool BurstGate { get; set; }
        public bool LevelAttenuation { get; set; }
        public bool ContinuousWave { get; set; }
        public bool Hopping { get; set; }
        public bool TriggerOutput1 { get; set; }
        public bool TriggerOutput2 { get; set; }

        public byte[] Encode()
        {
            var value = 0U;

            value |= BurstGate          ? 0x80000000U : 0;
            value |= LevelAttenuation   ? 0x40000000U : 0;
            value |= ContinuousWave     ? 0x20000000U : 0;
            value |= Hopping            ? 0x10000000U : 0;
            value |= TriggerOutput2     ? 0x08000000U : 0;
            value |= TriggerOutput1     ? 0x04000000U : 0;

            value |= (uint)(Symbol.Value - 1);

            var bytes = BitConverter.GetBytes(value);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(bytes);
            }

            return bytes;
        }

        public static ControlSignalDto Decode(ReadOnlySpan<byte> bytes)
        {
            var copy = new byte[4];
            bytes.CopyTo(copy);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(copy);
            }

            var value = BitConverter.ToUInt32(copy, 0);

            return new ControlSignalDto
            {
                Symbol         = (int)(value & 0x03FFFFFFU) + 1,
                BurstGate           = (value & 0x80000000U) > 0,
                LevelAttenuation    = (value & 0x40000000U) > 0,
                ContinuousWave      = (value & 0x20000000U) > 0,
                Hopping             = (value & 0x10000000U) > 0,
                TriggerOutput2      = (value & 0x08000000U) > 0,
                TriggerOutput1      = (value & 0x04000000U) > 0,
            };
        }
    }
}