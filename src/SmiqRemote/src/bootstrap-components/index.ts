import { FrameworkConfiguration, PLATFORM } from "aurelia-framework";

import { BootstrapFormValidationRenderer } from "./bootstrap-form-validation-renderer";

export function configure(config: FrameworkConfiguration) {
    config.globalResources([
        PLATFORM.moduleName('./checkbox/b-checkbox'),
        PLATFORM.moduleName('./radio/b-radio')
    ]);
}

export {
    BootstrapFormValidationRenderer
};