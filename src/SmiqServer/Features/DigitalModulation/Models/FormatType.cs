using System;

namespace SmiqServer.Features.DigitalModulation
{
    public enum FormatType
    {
        ASK,
        BPSK,
        QPSK,
        QPSK_IS95,
        QPSK_Inmarsat,
        QPSK_ICO,
        QPSK_WCDMA,
        OQPSK,
        OQPSK_IS95,
        P4QPSK,
        P4DQPSK,
        PSK8,
        PSK8Edge,
        GMSK,
        GFSK,
        FSK2,
        FSK4,
        FSK4_APCO25,
        QAM16,
        QAM32,
        QAM64,
        QAM256,
        USER
    }

    public static class FormatTypeExtensions
    {
        /// <summary>
        /// Vol 1, pp. 2.105
        /// </summary>
        /// <param name="formatType"></param>
        /// <returns></returns>
        public static string ToSerialString(this FormatType formatType)
        {
            switch (formatType)
            {
                case FormatType.ASK: return "ASK";
                case FormatType.BPSK: return "BPSK";
                case FormatType.QPSK: return "QPSK";
                case FormatType.QPSK_IS95: return "QIS95";
                case FormatType.QPSK_Inmarsat: return "QINM";
                case FormatType.QPSK_ICO: return "QICO";
                case FormatType.QPSK_WCDMA: return "QWCD";
                case FormatType.OQPSK: return "OQPS";
                case FormatType.OQPSK_IS95: return "OIS95";
                case FormatType.P4QPSK: return "P4QP";
                case FormatType.P4DQPSK: return "P4DQ";
                case FormatType.PSK8: return "PSK8";
                case FormatType.PSK8Edge: return "PSKE8";
                case FormatType.GMSK: return "GMSK";
                case FormatType.GFSK: return "GFSK";
                case FormatType.FSK2: return "FSK2";
                case FormatType.FSK4: return "FSK4";
                case FormatType.FSK4_APCO25: return "AFSK4";
                case FormatType.QAM16: return "QAM16";
                case FormatType.QAM32: return "QAM32";
                case FormatType.QAM64: return "QAM64";
                case FormatType.QAM256: return "QAM256";
                case FormatType.USER: return "USER";
                default: throw new NotSupportedException();
            }
        }
    }
}
