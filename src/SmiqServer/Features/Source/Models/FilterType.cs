using System;

namespace SmiqServer.Features.Source
{
    public enum FilterType
    {
        SquareRootRaisedCosine,
        Cosine,
        Gaussian,
        LinearizedGaussian,
        Bessel1,
        Bessel2,
        IS95,
        EqualizerIS95,
        APCO25,
        Tetra,
        WCDMA,
        Rectangle,
        SplitPhase,
        User
    }

    public static class FilterTypeExtensions
    {
        /// <summary>
        /// Vol 1, pp. 2.107
        /// </summary>
        /// <param name="filterType"></param>
        /// <returns></returns>
        public static string ToSerialString(this FilterType filterType)
        {
            switch (filterType)
            {
                case FilterType.SquareRootRaisedCosine: return "SCOS";
                case FilterType.Cosine: return "COS";
                case FilterType.Gaussian: return "GAUS";
                case FilterType.LinearizedGaussian: return "LGAU";
                case FilterType.Bessel1: return "BESS1";
                case FilterType.Bessel2: return "BESS2";
                case FilterType.IS95: return "IS95";
                case FilterType.EqualizerIS95: return "EIS95";
                case FilterType.APCO25: return "APCO";
                case FilterType.Tetra: return "TETR";
                case FilterType.WCDMA: return "WCDM";
                case FilterType.Rectangle: return "RECT";
                case FilterType.SplitPhase: return "SPH";
                case FilterType.User: return "USER";
                default: throw new NotSupportedException();
            }
        }
    }
}
