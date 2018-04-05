export type SourceType = "ExtPar" | "ExtSer" | "Pattern" | "PRBS" | "SerData" | "DataList";
export type ModulationType = "ASK" | "BPSK" | "QPSK" | "QPSK_IS95" | "QPSK_Inmarsat" | "QPSK_ICO" | "QPSK_WCDMA" | "OQPSK" | "OQPSK_IS95" | "P4QPSK" | "P4DQPSK" | "PSK8" | "PSK8Edge" | "GMSK" | "GFSK" | "FSK2" | "FSK4" | "FSK4_APCO25" | "QAM16" | "QAM32" | "QAM64" | "QAM256" | "USER";
export type FilterType = "SquareRootRaisedCosine" | "Cosine" | "Gaussian" | "LinearizedGaussian" | "Bessel1" | "Bessel2" | "IS95" | "EqualizerIS95" | "APCO25" | "Tetra" | "WCDMA" | "Rectangle" | "SplitPhase" | "User";
export type TriggerModeType = "Auto" | "Retrig" | "ArmedAuto" | "ArmedRetrig" | "Single";

export interface DigitalModulation {
    state: boolean;
    source: {
        source: SourceType;
        dataList: string;
        controlList: string;
    },
    modulation: {
        type: ModulationType;
        fskDeviation: number;
    },
    symbolRate: number;
    filter: {
        type: FilterType;
    },
    triggerMode: TriggerModeType;
}