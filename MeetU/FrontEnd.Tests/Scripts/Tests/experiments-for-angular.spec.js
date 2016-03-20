/// <reference path="../jasmine/boot.js" />
/// <reference path="../jasmine/console.js" />
/// <reference path="../jasmine/jasmine-html.js" />
/// <reference path="../jasmine/jasmine.js" />
/// <reference path="../angular.js" />
/// <reference path="../angular-mocks.js" />
/// <reference path="../angular-resource.js" />


//
//  Experiment: 
//      Syntax for service definition using .module(...).factory(...).
//      Also how to unit test.
//  @Yue
//
angular
    .module('serviceModule', [])
    .factory('add', function () {
        return function (lhs, rhs){
            return lhs + rhs;
        };
    });
describe("syntax for service only", function () {

    //inject module that contains the service.
    beforeEach(module('serviceModule'));

    //inject the specific service.
    var add;
    beforeEach(function () {
        inject(function ($injector) {
            add = $injector.get('add');
        });
    });

    //test the service injected.
    it('', function () {
        expect(add(41, 2)).toEqual(43);
    });
});
//
//  Experiment: 
//      Syntax for service injected into a controller defined in another module.
//      Also the unit test for the controller.
//  @Yue
//
angular
    .module('controllerModule', ['serviceModule'])
    .controller('ctrl', ['$scope', 'add', function ($scope, add) {
        $scope.number = add(1, 42);
    }]);
describe('syntax for service + controller across modules', function () {

    beforeEach(module('controllerModule'));

    var $controller;
    beforeEach(inject(function (_$controller_) {
        $controller = _$controller_;
    }));

    it('', function () {
        var $scope = {};
        var c = $controller('ctrl', { $scope: $scope });
        expect($scope.number).toEqual(43);
    });
})