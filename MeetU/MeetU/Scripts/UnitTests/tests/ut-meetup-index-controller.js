describe('meetupIndexController tests', function () {
    beforeEach(module('meetupModule'));

    var $controller;
    var $scope = null;
    beforeEach(inject(function (_$controller_) {
        $controller = _$controller_;
    }));
    beforeEach(inject(function ($rootScope) {
        $scope = $rootScope.$new();
        controller = $controller('MeetupIndexController', { $scope: $scope });
    }));
    it('exists', function () {
        expect(controller).not.toBeNull();
    });

    it('$scope.isIn', function () {
        var vm = $controller("MeetupIndexController", { $scope: $scope });
        expect(typeof vm.isIn === "function").toBe(true);
    });
});
