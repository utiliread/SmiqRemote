import { PLATFORM, autoinject } from "aurelia-framework";

import { RouterConfiguration } from "aurelia-router";

@autoinject()
export class Dasboard {
    configureRouter(config: RouterConfiguration) {
        config.map([{ route: '', viewPorts: {
            'digital-modulation': { moduleId: PLATFORM.moduleName("./digital-modulation") },
            'data-list': { moduleId: PLATFORM.moduleName("./data-list") },
            'control-list': { moduleId: PLATFORM.moduleName("./control-list") }
        } }]);
    }
}