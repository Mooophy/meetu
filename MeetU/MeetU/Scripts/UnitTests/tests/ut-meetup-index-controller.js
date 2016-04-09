describe('meetupIndexController tests', function () {
    beforeEach(module('meetupModule'));

    var $controller;
    var $scope = null;
    beforeEach(inject(function (_$controller_) {
        $controller = _$controller_;
    }));
    beforeEach(inject(function ($rootScope) {
        $scope = $rootScope.$new();
        controller = $controller('meetupIndexController', { $scope: $scope });
    }));
    it('exists', function () {
        expect(controller).not.toBeNull();
    });

    it('$scope.isIn', function () {
        expect(typeof $scope.isIn === "function").toBe(true);
    });
});
