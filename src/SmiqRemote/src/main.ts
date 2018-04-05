import { Aurelia, PLATFORM } from 'aurelia-framework';

export async function configure(aurelia: Aurelia) {
    aurelia.use.standardConfiguration();

    aurelia.use.developmentLogging();

    aurelia.use.plugin(PLATFORM.moduleName("aurelia-validation"));

    aurelia.use.feature(PLATFORM.moduleName('bootstrap-components/index'));
    
    await aurelia.start();

    await aurelia.setRoot(PLATFORM.moduleName('select-device'));
}