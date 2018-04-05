import { DigitalModulation } from "./digital-modulation";
import { Frequency } from "./frequency";
import { Http } from "ur-http";
import { HttpBuilder } from "ur-http/dist/esm/http-builder";
import { HttpBuilderOfT } from "ur-http/dist/esm/http-builder-of-t";
import { Level } from "./level";
import { Modulation } from "./modulation";
import { Output } from "./output";

export class ApiClient {
    serverUrl!: string;

    getBinary(action: string): HttpBuilderOfT<ArrayBuffer | null> {
        return Http.get(this.serverUrl + action).expectBinary();
    }

    getString(action: "/Common/Identification"): HttpBuilderOfT<string>;
    getString(action: string): HttpBuilderOfT<string | null> {
        return Http.get(this.serverUrl + action).expectString();
    }

    getJson(action: "/DigitalModulation"): HttpBuilderOfT<DigitalModulation>;
    getJson(action: "/DigitalModulation/ControlLists"): HttpBuilderOfT<string[]>;
    getJson(action: "/DigitalModulation/DataLists"): HttpBuilderOfT<string[]>;
    getJson(action: "/Frequency"): HttpBuilderOfT<Frequency>;
    getJson(action: "/Level"): HttpBuilderOfT<Level>;
    getJson(action: "/Modulation"): HttpBuilderOfT<Modulation>;
    getJson(action: "/Output"): HttpBuilderOfT<Output>;
    getJson<T>(action: string): HttpBuilderOfT<T | null | undefined>;
    getJson<T>(action: string): HttpBuilderOfT<T | null | undefined> {
        return Http.get(this.serverUrl + action).expectJson<T>();
    }

    putBinary(action: string, content: ArrayBuffer): HttpBuilder {
        return Http.put(this.serverUrl + action).with(content, "application/octet-stream");
    }

    putJson(action: string, content: any): HttpBuilder {
        return Http.put(this.serverUrl + action).withJson(content);
    }

    patchJson(action: "/DigitalModulation", fragment: DeepPartial<DigitalModulation>): HttpBuilder;
    patchJson(action: "/Frequency", fragment: DeepPartial<Frequency>): HttpBuilder;
    patchJson(action: "/Level", fragment: DeepPartial<Level>): HttpBuilder;
    patchJson(action: "/Modulation", fragment: DeepPartial<Modulation>): HttpBuilder;
    patchJson(action: "/Output", fragment: DeepPartial<Output>): HttpBuilder;
    patchJson(action: string, fragment: any): HttpBuilder {
        return Http.patch(this.serverUrl + action).withJson(fragment);
    }

    delete(action: string): HttpBuilder {
        return Http.delete(this.serverUrl + action);
    }
}

type DeepPartial<T> = {
    [P in keyof T]?: DeepPartial<T[P]>;
};