import { ValidationController, ValidationControllerFactory, ValidationRules } from "aurelia-validation";
import { autoinject, observable } from "aurelia-framework";

import { ApiClient } from "./api"
import { BootstrapFormValidationRenderer } from "./bootstrap-components";
import { DOM } from "aurelia-pal";

@autoinject()
export class DataList {
    private isActivated = false;
    private validationController: ValidationController;

    @observable()
    selectedDataListName!: string | null;
    dataListNames!: string[];
    @observable()
    dataListHex!: string;

    constructor(private api: ApiClient, validationControllerFactory: ValidationControllerFactory) {
        this.validationController = validationControllerFactory.createForCurrentScope();

        this.validationController.addRenderer(new BootstrapFormValidationRenderer());

        ValidationRules
            .customRule('hex', x => x.length % 2 === 0, "\${$displayName} must have an even number of characters.");

        ValidationRules
            .ensure((x: DataList) => x.dataListHex).required().satisfiesRule('hex')
            .on(this);
    }

    async activate() {
        const digitalModulation = await this.api.getJson("/DigitalModulation").transfer();
        this.selectedDataListName = digitalModulation.source.dataList;

        await this.getDataListNames();

        this.isActivated = true;
    }

    async selectedDataListNameChanged() {
        if (!this.selectedDataListName) {
            this.dataListHex = "";
            return;
        }

        if (this.isActivated) {
            await this.api.patchJson("/DigitalModulation", { source: { dataList: this.selectedDataListName } }).send();
        }

        const data = await this.api.getBinary(`/DigitalModulation/DataLists/${this.selectedDataListName}`).transfer();
        this.dataListHex = data ? toHex(data) : '';
    }

    async dataListHexChanged() {
        if (!this.selectedDataListName || !this.isActivated) {
            return;
        }

        const validationResult = await this.validationController.validate();
        if (!validationResult.valid) {
            return;
        }

        const data = fromHex(this.dataListHex);
        await this.api.putBinary(`/DigitalModulation/DataLists/${this.selectedDataListName}`, data).send();
    }

    async create() {
        let name = prompt("Name of new datalist");
        if (name) {
            name = name.toUpperCase();

            await this.api.putBinary(`/DigitalModulation/DataLists/${name}`, new ArrayBuffer(1)).send();
            await this.getDataListNames();
            this.selectedDataListName = name;
        }
    }

    async exportSelected() {
        const data = await this.api.getBinary(`/DigitalModulation/DataLists/${this.selectedDataListName}`).transfer();
        
        if (data) {
            const base64 = btoa(String.fromCharCode.apply(null, new Uint8Array(data)));

            let a = DOM.createElement("a") as HTMLAnchorElement;
            a.setAttribute("href", "data:application/octet-stream;base64," + base64);
            a.setAttribute("download", `${this.selectedDataListName}.bin`);
            a.click();
        }
    }

    async deleteSelected() {
        await this.api.delete(`/DigitalModulation/DataLists/${this.selectedDataListName}`).send();
        this.selectedDataListName = null;
        await this.getDataListNames();
    }

    private async getDataListNames() {
        this.dataListNames = await this.api.getJson("/DigitalModulation/DataLists").transfer();
    }
}

function toHex(buffer: ArrayBuffer) {
    return Array.prototype.map.call(new Uint8Array(buffer), (x: number) => ('0' + x.toString(16)).slice(-2)).join("");
}

function fromHex(hex: string) {
    if (hex.length % 2 !== 0) {
        throw new Error("Invalid hex");
    }

    let result = new ArrayBuffer(hex.length / 2);
    let array = new Uint8Array(result);

    for (let i = 0; i < array.length; i++) {
        array[i] = parseInt(hex.substring(2 * i, 2 * i + 2), 16);
    }

    return result;
}