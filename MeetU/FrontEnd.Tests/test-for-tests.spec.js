/// <reference path="../MeetU/Scripts/angular.js"/>
/// <reference path="../MeetU/Scripts/angular-mocks.js"/>

describe("To check if Jasmine works", function () {
    it("contains spec with an expectation", function () {
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

describe('To check if Jasmine + angular works together', function () {
    beforeEach(module('test-for-jasmine-app'));

    var $controller;

    beforeEach(inject(function (_$controller_) {
        // The injector unwraps the underscores (_) from around the parameter names when matching
        $controller = _$controller_;
    }));

    describe('$scope.grade', function () {
        it('sets the strength to "strong" if the password length is >8 chars', function () {
            var $scope = {};
            var controller = $controller('PasswordController', { $scope: $scope });
            $scope.password = 'longerthaneightchars';
            $scope.grade();
            expect($scope.strength).toEqual('strong');
        });
    });
});