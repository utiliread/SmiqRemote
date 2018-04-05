import * as HtmlPlugin from "html-webpack-plugin";

import { Configuration, HotModuleReplacementPlugin } from "webpack";

import { AureliaPlugin } from "aurelia-webpack-plugin";
import { resolve } from "path";

const config: Configuration = {
    entry: [ "aurelia-bootstrapper"],
    output: {
        filename: "bundle.js",
        path: resolve("wwwroot")
    },
    module: {
        rules: [
            { test: /\.html$/, use: "html-loader" },
            { test: /\.ts$/, use: "ts-loader" },
        ]
    },
    resolve: {
        extensions: [".ts", ".js"],
        modules: ["src", "node_modules"].map(x => resolve(x))
    },
    plugins: [
        new AureliaPlugin(),
        new HtmlPlugin({template: 'index.html'}),
        new HotModuleReplacementPlugin()
    ],
    devServer: {
        contentBase: "wwwroot",
        hot: true
    }
}

export default config;