import { Aurelia, PLATFORM } from 'aurelia-framework';
import { faDownload, faMinus, faPlug, faPlus, faSave, faServer, faTrash, faUpload } from "@fortawesome/fontawesome-free-solid";

import { library }  from "@fortawesome/fontawesome";

library.add(faSave, faPlug, faDownload, faPlus, faUpload, faMinus, faServer, faTrash);

export async function configure(aurelia: Aurelia) {
    aurelia.use.standardConfiguration();

    aurelia.use.developmentLogging();

    aurelia.use.plugin(PLATFORM.moduleName("aurelia-validation"));

    aurelia.use.feature(PLATFORM.moduleName('bootstrap-components/index'));
    aurelia.use.feature(PLATFORM.moduleName('fontawesome/index'));
    
    await aurelia.start();

    await aurelia.setRoot(PLATFORM.moduleName('select-device'));
}