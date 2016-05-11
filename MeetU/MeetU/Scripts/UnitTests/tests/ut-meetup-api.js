describe('MeetupApi', function () {
    beforeEach(module('meetupModule'));

    var $controller;
    var $scope = null;
    var $q;
    var mockApiService, mockResponse;

    beforeEach(inject(function (_$rootScope_, _$q_) {
        $rootScope = _$rootScope_;
        $q = _$q_;
    }));

    beforeEach(inject(function ($controller) {
        $scope = $rootScope.$new();
        mockApiService = {
            query: function () {
                queryDeferred = $q.defer();
                return { $promise: queryDeferred.promise }
            }
        }

        spyOn(mockApiService, 'query').and.callThrough();
        controller = $controller('MeetupIndexController', {
            $scope: $scope,
            CommentViewService: mockApiService
        });
    }));

    it('should be exist', function () {
        expect(controller).not.toBeNull();
    })

    describe("$resource('/api/Comments/')", function () {
        beforeEach(function () {
            queryDeferred.resolve(mockResponse);
            $rootScope.$apply();
        });
    })
    it('should query the ApiService', function () {
        expect(mockApiService.query).toHaveBeenCalled();
    });

})