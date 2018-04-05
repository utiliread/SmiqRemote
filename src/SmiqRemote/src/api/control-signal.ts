export interface ControlSignal {
    symbol: number;
    burstGate: boolean;
    levelAttenuation: boolean;
    continuousWave: boolean;
    hopping: boolean;
    triggerOutput1: boolean;
    triggerOutput2: boolean;
}