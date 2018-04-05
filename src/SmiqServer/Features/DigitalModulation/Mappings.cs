using System;
using System.Collections.Generic;
using System.Linq;

namespace SmiqServer.Features.DigitalModulation
{
    public static class Mappings
    {
        private static Dictionary<string, FilterType> _filterTypeMap = ((FilterType[])Enum.GetValues(typeof(FilterType))).ToDictionary(x => x.ToSerialString(), x => x);
        private static Dictionary<string, ModulationType> _formatTypeMap = ((ModulationType[])Enum.GetValues(typeof(ModulationType))).ToDictionary(x => x.ToSerialString(), x => x);
        private static Dictionary<string, TriggerModeType> _sequenceTypeMap = ((TriggerModeType[])Enum.GetValues(typeof(TriggerModeType))).ToDictionary(x => x.ToSerialString(), x => x);
        private static Dictionary<string, SourceType> _sourceTypeMap = ((SourceType[])Enum.GetValues(typeof(SourceType))).ToDictionary(x => x.ToSerialString(), x => x);

        public static FilterType ParseFilterType(string x) => _filterTypeMap[x];
        public static ModulationType ParseFormatType(string x) => _formatTypeMap[x];
        public static TriggerModeType ParseSequenceType(string x) => _sequenceTypeMap[x];
        public static SourceType ParseSourceType(string x) => _sourceTypeMap[x];
    }
}
