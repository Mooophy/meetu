describe('MeetupApi', function () {
	beforeEach(module('meetupModule'))

	beforeEach(inject(function($injector){
		$httpBackend=$injector.get('$httpBackend');
		$rootScope=$injector.get('$rootScope');
		Service=$injector.get('$api');
	}))

	it('Comments should be exist',function(){
		expect(Service.comment).not.toBeNull();
	})

	it('Comments should get success',function(){
		var result={};
		$httpBackend.expect('GET','/api/Comments?meetupId=123').respond([{meetupId:123}]);
		Service.comment.get().query({meetupId:123},function(data){
		result=data;
		})
		$httpBackend.flush()
		expect(result[0].meetupId).toBe(123);
	})
})
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

describe('routeConfiguration tests', function () {
    var route, location, rootScope, httpBackend;
    beforeEach(module('meetupModule'));
    beforeEach(inject(function ($route, $location, $rootScope, $httpBackend) {
        $httpBackend.when('GET', '/Scripts/Views/Meetup/Index.html').respond({ userId: 'userX' }, { 'A-Token': 'xxx' });
        $httpBackend.when('GET', '/Scripts/Views/Meetup/Create.html').respond({ userId: 'userX' }, { 'A-Token': 'xxx' });
        $httpBackend.when('GET', '/Scripts/Views/Profile/profile-display.html').respond({ userId: 'userX' }, { 'A-Token': 'xxx' });
        $httpBackend.when('GET', '/Scripts/Views/Profile/profile-img-edit.html').respond({ userId: 'userX' }, { 'A-Token': 'xxx' });
        $httpBackend.when('GET', '/Scripts/Directives/subpage-nav/subpage-nav.html').respond({ userId: 'userX' }, { 'A-Token': 'xxx' });
        $httpBackend.when('GET', '/Scripts/Directives/loading-circle/loading-circle.html').respond({ userId: 'userX' }, { 'A-Token': 'xxx' });
        route = $route;
        location = $location;
        rootScope = $rootScope;
        httpBackend = $httpBackend;
    }));

    it('route is initialised to null', function () {
        expect(route.current).toBeUndefined();
    });
    it('route directs to index', function () {
        location.path('/index');
        rootScope.$digest();
        expect(route.current.templateUrl).toEqual('/Scripts/Views/Meetup/Index.html');
    });
    it('route directs to /profile/:profileId', function () {
        location.path('/Profile/1');
        rootScope.$digest();
        expect(route.current.templateUrl).toBe('/Scripts/Views/Profile/profile-display.html');
    });
    it('route directs to /ProfileEdit/Image', function () {
        location.path('/ProfileEdit/Image');
        rootScope.$digest();
        expect(route.current.templateUrl).toBe('/Scripts/Views/Profile/profile-img-edit.html');
    });
    it('route directs to /meetup/create', function () {
        location.path('/Meetup/Create');
        rootScope.$digest();
        expect(route.current.templateUrl).toBe('/Scripts/Views/Meetup/Create.html');
    });
    it('route directs to otherwise', function () {
        location.path('/otherwise');
        rootScope.$digest();
        expect(location.path()).toBe('/index');
        expect(route.current.templateUrl).toEqual('/Scripts/Views/Meetup/Index.html');
    });
});