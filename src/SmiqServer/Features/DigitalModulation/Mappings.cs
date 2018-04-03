using System;
using System.Collections.Generic;
using System.Linq;

namespace SmiqServer.Features.DigitalModulation
{
    public static class Mappings
    {
        private static Dictionary<string, FilterType> _filterTypeMap = ((FilterType[])Enum.GetValues(typeof(FilterType))).ToDictionary(x => x.ToSerialString(), x => x);
        private static Dictionary<string, FormatType> _formatTypeMap = ((FormatType[])Enum.GetValues(typeof(FormatType))).ToDictionary(x => x.ToSerialString(), x => x);
        private static Dictionary<string, SequenceType> _sequenceTypeMap = ((SequenceType[])Enum.GetValues(typeof(SequenceType))).ToDictionary(x => x.ToSerialString(), x => x);
        private static Dictionary<string, SourceType> _sourceTypeMap = ((SourceType[])Enum.GetValues(typeof(SourceType))).ToDictionary(x => x.ToSerialString(), x => x);

        public static FilterType ParseFilterType(string x) => _filterTypeMap[x];
        public static FormatType ParseFormatType(string x) => _formatTypeMap[x];
        public static SequenceType ParseSequenceType(string x) => _sequenceTypeMap[x];
        public static SourceType ParseSourceType(string x) => _sourceTypeMap[x];
    }
}
