/// <reference path="../jasmine/boot.js" />
/// <reference path="../jasmine/console.js" />
/// <reference path="../jasmine/jasmine-html.js" />
/// <reference path="../jasmine/jasmine.js" />
/// <reference path="../angular.js" />
/// <reference path="../angular-mocks.js" />
/// <reference path="../../../meetu/scripts/app/meetu-profile.js" />
/// <reference path="../angular-resource.js" />

angular.module('dataUriExperiment',['ngResource'])
.controller('dataUriExperimentController', function ($scope) {
    $scope.bar = { something: 1 };
    $scope.add = function (lhs, rhs) {
        return lhs + rhs;
    };
});

describe("DataUri", function () {
    beforeEach(module('dataUriExperiment'));

    var $controller;
    beforeEach(inject(function (_$controller_) {
        $controller = _$controller_;
    }));

    describe('de', function () {
        it('it', function () {
            var $scope = {};
            var controller = $controller('dataUriExperimentController', { $scope: $scope });
            expect(3).toEqual(3);
            expect($scope.bar.something).toEqual(1);
            expect($scope.add(1, 2)).toEqual(3);
        });
    });
});