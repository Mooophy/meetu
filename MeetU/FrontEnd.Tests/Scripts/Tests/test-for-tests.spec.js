/// <reference path="../jasmine/jasmine.js" />
/// <reference path="../jasmine/boot.js" />
/// <reference path="../jasmine/console.js" />
/// <reference path="../jasmine/jasmine-html.js" />
/// <reference path="../angular.js" />
/// <reference path="../angular-mocks.js" />

//
//  the tests here is to test if the setup for jasmine and angular works as expected.
//  --Yue.
//

describe("jasmine", function () {
    it("", function () {
        expect(true).toBe(true);
    });
});

angular.module('test-for-jasmine-app', [])
.controller('PasswordController', function PasswordController($scope) {
    $scope.password = '';
    $scope.grade = function () {
        var size = $scope.password.length;
        if (size > 8) {
            $scope.strength = 'strong';
        } else if (size > 3) {
            $scope.strength = 'medium';
        } else {
            $scope.strength = 'weak';
        }
    };
});

describe('Jasmine + angular', function () {
    beforeEach(module('test-for-jasmine-app'));

    var $controller;

    beforeEach(inject(function (_$controller_) {
        // The injector unwraps the underscores (_) from around the parameter names when matching
        $controller = _$controller_;
    }));

    it('', function () {
        var $scope = {};
        var controller = $controller('PasswordController', { $scope: $scope });
        $scope.password = 'longerthaneightchars';
        $scope.grade();
        expect($scope.strength).toEqual('strong');
    });
});