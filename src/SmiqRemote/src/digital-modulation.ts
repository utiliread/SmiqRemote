import { FilterType, ModulationType, SourceType, TriggerModeType } from "./api/digital-modulation";
import { autoinject, observable } from "aurelia-framework";

import { ApiClient } from "./api";

@autoinject()
export class DigitalModulation {
    private isActivated = false;

    @observable()
    state!: boolean;
    
    @observable()
    source!: SourceType;

    @observable()
    format!: ModulationType;

    @observable()
    fskDeviation!: number;

    @observable()
    symbolRate!: number;

    @observable()
    filter!: FilterType;
    
    @observable()
    sequence!: TriggerModeType;
    
    constructor(private api: ApiClient) {
    }

    async activate() {
        const digitalModulation = await this.api.getJson("/DigitalModulation").transfer();
        this.state = digitalModulation.state;
        this.source = digitalModulation.source.source;
        this.format = digitalModulation.modulation.type;
        this.fskDeviation = digitalModulation.modulation.fskDeviation;
        this.symbolRate = digitalModulation.symbolRate;
        this.filter = digitalModulation.filter.type;
        this.sequence = digitalModulation.triggerMode;

        this.isActivated = true;
    }

    stateChanged() { if (this.isActivated) this.api.patchJson("/DigitalModulation", { state: this.state }).send(); }
    sourceChanged() { if (this.isActivated) this.api.patchJson("/DigitalModulation", { source: { source: this.source } }).send(); }
    formatChanged() { if (this.isActivated) this.api.patchJson("/DigitalModulation", { modulation: { type: this.format } }).send(); }
    fskDeviationChanged() { if (this.isActivated) this.api.patchJson("/DigitalModulation", { modulation: { fskDeviation: this.fskDeviation } }).send(); }
    symbolRateChanged() { if (this.isActivated) this.api.patchJson("/DigitalModulation", { symbolRate: this.symbolRate }).send(); }
    filterChanged() { if (this.isActivated) this.api.patchJson("/DigitalModulation", { filter: { type: this.filter } }).send(); }
    sequenceChanged() { if (this.isActivated) this.api.patchJson("/DigitalModulation", { triggerMode: this.sequence }).send(); }
}