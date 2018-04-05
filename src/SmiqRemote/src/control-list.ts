import { BindingEngine, DOM, Disposable, ICollectionObserverSplice, autoinject, observable } from "aurelia-framework";

import { ApiClient } from "./api";
import { ControlSignal } from "./api/control-signal";

@autoinject()
export class ControlList {
    private isActivated = false;
    
    @observable()
    selectedControlListName!: string | null;
    controlListNames!: string[];
    signals: ControlSignalViewModel[] = [];

    symbolMax = 0x4000000;

    private disposables: Disposable[] = [];

    constructor(private api: ApiClient, private bindingEngine: BindingEngine) {
    }

    async activate() {
        const digitalModulation = await this.api.getJson("/DigitalModulation").transfer();
        this.selectedControlListName = digitalModulation.source.controlList;

        await this.getControlListNames();

        this.isActivated = true;
    }

    attached() {
        this.disposables.push(this.bindingEngine.collectionObserver(this.signals).subscribe(this.signalsSpliced.bind(this)));
    }

    detached() {
        for (const disposable of this.disposables) {
            disposable.dispose();
        }
    }

    async selectedControlListNameChanged() {
        if (!this.selectedControlListName) {
            return;
        }

        if (this.isActivated) {
            await this.api.patchJson("/DigitalModulation", { source: { controlList: this.selectedControlListName } }).send();
        }

        const signals = await this.api.getJson<ControlSignal[]>(`/DigitalModulation/ControlLists/${this.selectedControlListName}`).transfer();

        if (signals) {
            Array.prototype.splice.call(this.signals, 0, this.signals.length, ...signals.map(x => {
                const signal = new ControlSignalViewModel(this.bindingEngine, x);
                this.disposables.push(signal.subscribe(x => this.save()));
                return signal;
            }));
        }
    }

    signalsSpliced() {
        return this.save();
    }

    async create() {
        let name = prompt("Name of new datalist");
        if (name) {
            name = name.toUpperCase();
            
            const signalObject = new ControlSignalViewModel(this.bindingEngine).toObject();
            await this.api.putJson(`/DigitalModulation/ControlLists/${name}`, [signalObject]).send();
            await this.getControlListNames();
            this.selectedControlListName = name;
        }
    }

    async exportSelected() {
        const signals = await this.api.getJson<ControlSignal[]>(`/DigitalModulation/ControlLists/${this.selectedControlListName}`).transfer();

        let a = DOM.createElement("a") as HTMLAnchorElement;
        a.setAttribute("href", "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(signals, null, 2)));
        a.setAttribute("download", `${this.selectedControlListName}.json`);
        a.click();
    }

    async deleteSelected() {
        await this.api.delete(`/DigitalModulation/ControlLists/${this.selectedControlListName}`).send();
        this.selectedControlListName = null;
        await this.getControlListNames();
    }

    addSignal() {
        const lastSymbol = this.signals[this.signals.length - 1].symbol;

        const signal = new ControlSignalViewModel(this.bindingEngine, {
            symbol: lastSymbol + 1
        });

        this.disposables.push(signal.subscribe(x => this.save()));

        this.signals.push(signal);
    }

    removeSignal(index: number) {
        this.signals.splice(index, 1);
    }

    private async getControlListNames() {
        this.controlListNames = await this.api.getJson("/DigitalModulation/ControlLists").transfer();
    }

    private save() {
        return this.api.putJson(`/DigitalModulation/ControlLists/${this.selectedControlListName}`, this.signals.map(x => x.toObject())).send();
    }
}

class ControlSignalViewModel implements ControlSignal {
    public symbol: number = 1;
    public burstGate: boolean = true;
    public levelAttenuation: boolean = false;
    public continuousWave: boolean = false;
    public hopping: boolean = false;
    public triggerOutput1: boolean = false;
    public triggerOutput2: boolean = false;

    constructor(private bindingEngine: BindingEngine, init?: Partial<ControlSignal>) {
        if (init) {
            Object.assign(this, init);
        }
    }

    toObject() {
        let value: ControlSignal = {
            symbol: this.symbol,
            burstGate: this.burstGate,
            levelAttenuation: this.levelAttenuation,
            continuousWave: this.continuousWave,
            hopping: this.hopping,
            triggerOutput1: this.triggerOutput1,
            triggerOutput2: this.triggerOutput2
        };

        return value;
    }

    subscribe(callback: (value?: ControlSignalViewModel, propertyName?: string) => void): Disposable {
        const disposables: Disposable[] = []; 
        for (const propertyName in this) {
            disposables.push(this.bindingEngine.propertyObserver(this, propertyName).subscribe(x => callback(this, propertyName)));
        }
        return {
            dispose: () => {
                for (const disposable of disposables) {
                    disposable.dispose();
                }
            }
        }
    }
}