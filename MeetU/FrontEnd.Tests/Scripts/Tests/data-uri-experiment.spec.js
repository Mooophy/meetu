/// <reference path="../jasmine/boot.js" />
/// <reference path="../jasmine/console.js" />
/// <reference path="../jasmine/jasmine-html.js" />
/// <reference path="../jasmine/jasmine.js" />
/// <reference path="../angular.js" />
/// <reference path="../angular-mocks.js" />

var dataUriExperiment = angular.module('dataUriExperiment', ['ngResource']);
dataUriExperiment.controller('dataUriExperimentController', function ($scope, $resource) {
    var foo = [1, 2, 3];
});

describe("s", function () {
    it("z", function () {
        expect(1).toEqual(1);
    });
});