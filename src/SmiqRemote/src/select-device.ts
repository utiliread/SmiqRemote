import { Aurelia, PLATFORM, autoinject } from "aurelia-framework";
import { ValidationControllerFactory, ValidationRules } from "aurelia-validation";

import { ApiClient } from "./api";
import { BootstrapFormValidationRenderer } from "./bootstrap-components";

@autoinject()
export class SelectDevice {
    devices = new Map<string, string>();
    serverUrl = "";

    constructor(private api: ApiClient, private aurelia: Aurelia, validationControllerFactory: ValidationControllerFactory) {
        ValidationRules
            .customRule("server-url", value => /^https?:\/\/.*[^\/]$/.test(value), "\${$displayName} must start with http:// or https:// and must not end with '/'");

        ValidationRules
            .ensure((x: SelectDevice) => x.serverUrl).required().satisfiesRule("server-url")
            .on(this);

        let controller = validationControllerFactory.createForCurrentScope();

        controller.addRenderer(new BootstrapFormValidationRenderer());
    }

    activate() {
        const json = window.localStorage.getItem("devices");
        if (json) {
            const devices: DevicesObject = JSON.parse(json);

            for (const name in devices) {
                this.devices.set(name, devices[name]);
            }
        }
    }

    save() {
        const newName = prompt("Name");

        if (newName) {
            let devices: DevicesObject = {};

            for (const [name,server] of this.devices) {
                devices[name] = server;
            }

            devices[newName] = this.serverUrl;

            const json = JSON.stringify(devices);
            window.localStorage.setItem("devices", json);

            for (const name in devices) {
                this.devices.set(name, devices[name]);
            }
        }
    }

    submit() {
        this.api.serverUrl = this.serverUrl;

        return this.aurelia.setRoot(PLATFORM.moduleName("app"));
    }
}

type DevicesObject = { [name: string]: string };