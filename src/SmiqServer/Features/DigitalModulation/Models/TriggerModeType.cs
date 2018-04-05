using System;

namespace SmiqServer.Features.DigitalModulation
{
    public enum TriggerModeType
    {
        Auto,
        Retrig,
        ArmedAuto,
        ArmedRetrig,
        Single
    }

    public static class SequenceTypeExtensions
    {
        /// <summary>
        /// Vol 1, pp. 2.109
        /// </summary>
        /// <param name="sequenceType"></param>
        /// <returns></returns>
        public static string ToSerialString(this TriggerModeType sequenceType)
        {
            switch (sequenceType)
            {
                case TriggerModeType.Auto: return "AUTO";
                case TriggerModeType.Retrig: return "RETR";
                case TriggerModeType.ArmedAuto: return "AAUT";
                case TriggerModeType.ArmedRetrig: return "ARET";
                case TriggerModeType.Single: return "SING";
                default: throw new NotSupportedException();
            }
        }
    }
}
