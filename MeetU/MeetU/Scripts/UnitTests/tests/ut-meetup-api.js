describe('MeetupApi', function () {
    beforeEach(module('meetupModule'))

    beforeEach(inject(function ($injector) {
        $httpBackend = $injector.get('$httpBackend');
        $rootScope = $injector.get('$rootScope');
        Service = $injector.get('$api');
    }))

    it('Comments should be exist', function () {
        expect(Service.comment).not.toBeNull();
    })

    it('Comments should get success', function () {
        var result = {};
        $httpBackend.expect('GET', '/api/Comments?meetupId=123').respond([{ meetupId: 123 }]);
        Service.comment.get().query({ meetupId: 123 }, function (data) {
            result = data;
        })
        $httpBackend.flush()
        expect(result[0].meetupId).toBe(123);
    })
})