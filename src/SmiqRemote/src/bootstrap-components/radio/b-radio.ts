import { bindable, bindingMode } from "aurelia-framework";

let nextId = 0;

export class BRadioCustomElement {
    @bindable()
    id: string = "radio" + nextId++;

    @bindable()
    name!: string;

    @bindable({defaultBindingMode: bindingMode.twoWay})
    checked!: boolean;

    @bindable()
    model!: any;
}