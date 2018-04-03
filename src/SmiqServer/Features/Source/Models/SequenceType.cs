using System;

namespace SmiqServer.Features.Source
{
    public enum SequenceType
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
        public static string ToSerialString(this SequenceType sequenceType)
        {
            switch (sequenceType)
            {
                case SequenceType.Auto: return "AUTO";
                case SequenceType.Retrig: return "RETR";
                case SequenceType.ArmedAuto: return "AAUT";
                case SequenceType.ArmedRetrig: return "ARET";
                case SequenceType.Single: return "SING";
                default: throw new NotSupportedException();
            }
        }
    }
}
