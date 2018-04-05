import { bindable, bindingMode } from "aurelia-framework";

let nextId = 0;

export class BCheckboxCustomElement {
    @bindable()
    id: string = "checkbox" + nextId++;

    @bindable({defaultBindingMode: bindingMode.twoWay})
    checked!: boolean;
}