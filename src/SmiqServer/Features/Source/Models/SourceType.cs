using System;

namespace SmiqServer.Features.Source
{
    public enum SourceType
    {
        ExtPar,
        ExtSer,
        Pattern,
        PRBS,
        SerData,
        DataList
    }

    public static class SourceTypeExtensions
    {
        /// <summary>
        /// Vol 1, pp. 2.102
        /// </summary>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public static string ToSerialString(this SourceType sourceType)
        {
            switch (sourceType)
            {
                case SourceType.ExtPar: return "PAR";
                case SourceType.ExtSer: return "SER";
                case SourceType.Pattern: return "PATT";
                case SourceType.PRBS: return "PRBS";
                case SourceType.SerData: return "SDAT";
                case SourceType.DataList: return "DLIS";
                default: throw new NotSupportedException();
            }
        }
    }
}
