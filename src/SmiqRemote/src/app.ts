import { Aurelia, DOM, PLATFORM, autoinject, observable } from 'aurelia-framework';
import { RouteConfig, RouterConfiguration } from 'aurelia-router';

import { ApiClient } from './api';
import { DigitalModulation } from './api/digital-modulation';
import { Frequency } from './api/frequency';
import { Level } from './api/level';
import { Modulation } from './api/modulation';
import { Output } from './api/output';

const routes: RouteConfig[] = [
    { route: '', name: 'dashboard', moduleId: PLATFORM.moduleName('./dashboard') }
];

@autoinject()
export class App {
    private isActivated = false;

    identification!: string;
    
    @observable()
    frequency!: number;
    
    @observable()
    level!: number;
    
    @observable()
    levelOffset!: number;

    @observable()
    state!: boolean;

    importFile!: HTMLInputElement;

    constructor(private api: ApiClient, private aurelia: Aurelia) {
    }

    configureRouter(config: RouterConfiguration) {
        config.options.pushState = true;
        config.map(routes);
    }

    async activate() {
        this.identification = await this.api.getString("/Common/Identification").transfer();

        const frequency = await this.api.getJson("/Frequency").transfer();
        this.frequency = frequency.frequency;

        const level = await this.api.getJson("/Level").transfer();
        this.level = level.amplitude;
        this.levelOffset = level.offset;

        const output = await this.api.getJson("/Output").transfer();
        this.state = output.state;

        this.isActivated = true;
    }

    frequencyChanged() { if (this.isActivated) this.api.patchJson("/Frequency", { frequency: this.frequency }).send(); }
    levelChanged() { if (this.isActivated) this.api.patchJson("/Level", { amplitude: this.level }).send(); }
    levelOffsetChanged() { if (this.isActivated) this.api.patchJson("/Level", { offset: this.levelOffset }).send(); }
    stateChanged() { if (this.isActivated) { this.api.patchJson("/Output", { state: this.state }).send(); } }

    async export() {
        const digitalModulation = await this.api.getJson("/DigitalModulation").transfer();
        const frequency = await this.api.getJson("/Frequency").transfer();
        const level = await this.api.getJson("/Level").transfer();
        const modulation = await this.api.getJson("/Modulation").transfer();
        const output = await this.api.getJson("/Output").transfer();

        const exportObject: ExportObject = {
            digitalModulation: digitalModulation,
            frequency: frequency,
            level: level,
            modulation: modulation,
            output: output
        };

        let a = DOM.createElement("a") as HTMLAnchorElement;
        a.setAttribute("href", "data:text/json;charset=utf-8," + encodeURIComponent(JSON.stringify(exportObject, null, 2)));
        a.setAttribute("download", "export.json");
        a.click();
    }

    openImportDialog() {
        this.importFile.click();
    }

    import() {
        if (!this.importFile.files) {
            return;
        }
        
        const file = this.importFile.files[0];
        const reader = new FileReader();
        reader.onload = async () => {
            const exportObject: ExportObject = JSON.parse(reader.result);

            await this.api.patchJson("/DigitalModulation", exportObject.digitalModulation).send();
            await this.api.patchJson("/Frequency", exportObject.frequency).send();
            await this.api.patchJson("/Level", exportObject.level).send();
            await this.api.patchJson("/Modulation", exportObject.modulation).send();
            await this.api.patchJson("/Output", exportObject.output).send();

            await this.aurelia.setRoot(PLATFORM.moduleName("app"));
        };
        reader.readAsText(file);
    }

    selectDevice() {
        return this.aurelia.setRoot(PLATFORM.moduleName("select-device"));
    }
}

interface ExportObject {
    digitalModulation: DigitalModulation;
    frequency: Frequency;
    level: Level;
    modulation: Modulation;
    output: Output;
}