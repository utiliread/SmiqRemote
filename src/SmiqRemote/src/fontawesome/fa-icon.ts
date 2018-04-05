import { AbstractElement, IconName, IconPrefix, icon } from "@fortawesome/fontawesome";
import { DOM, autoinject, bindable } from "aurelia-framework";

@autoinject()
export class FaIconCustomElement {
    @bindable()
    prefix: IconPrefix | undefined = undefined;

    @bindable()
    name!: IconName;

    constructor(private element: Element) {
    }

    attached() {
        const renderedIcon = icon({
            prefix: this.prefix || "fas",
            iconName: this.name
        });

        const { abstract } = renderedIcon;
        
        const element = this.convert(abstract[0]);

        this.element.appendChild(element);
    }

    private convert(abstract: AbstractElement, xmlns?: string) {
        xmlns = abstract.attributes.xmlns || xmlns;
        
        const children = (abstract.children || []).map(x => this.convert(x, xmlns));
        
        const element = xmlns ? document.createElementNS(xmlns, abstract.tag) : document.createElement(abstract.tag);

        for (const attributeName in abstract.attributes) {
            element.setAttribute(attributeName, abstract.attributes[attributeName]);
        }

        for (const child of children) {
            element.appendChild(child);
        }

        return element;
    }
}