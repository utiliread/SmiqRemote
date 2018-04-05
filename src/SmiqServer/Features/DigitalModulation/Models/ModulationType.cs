using System;

namespace SmiqServer.Features.DigitalModulation
{
    public enum ModulationType
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
        public static string ToSerialString(this ModulationType formatType)
        {
            switch (formatType)
            {
                case ModulationType.ASK: return "ASK";
                case ModulationType.BPSK: return "BPSK";
                case ModulationType.QPSK: return "QPSK";
                case ModulationType.QPSK_IS95: return "QIS95";
                case ModulationType.QPSK_Inmarsat: return "QINM";
                case ModulationType.QPSK_ICO: return "QICO";
                case ModulationType.QPSK_WCDMA: return "QWCD";
                case ModulationType.OQPSK: return "OQPS";
                case ModulationType.OQPSK_IS95: return "OIS95";
                case ModulationType.P4QPSK: return "P4QP";
                case ModulationType.P4DQPSK: return "P4DQ";
                case ModulationType.PSK8: return "PSK8";
                case ModulationType.PSK8Edge: return "PSKE8";
                case ModulationType.GMSK: return "GMSK";
                case ModulationType.GFSK: return "GFSK";
                case ModulationType.FSK2: return "FSK2";
                case ModulationType.FSK4: return "FSK4";
                case ModulationType.FSK4_APCO25: return "AFSK4";
                case ModulationType.QAM16: return "QAM16";
                case ModulationType.QAM32: return "QAM32";
                case ModulationType.QAM64: return "QAM64";
                case ModulationType.QAM256: return "QAM256";
                case ModulationType.USER: return "USER";
                default: throw new NotSupportedException();
            }
        }
    }
}
