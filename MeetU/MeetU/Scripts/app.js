var meetu = angular.module('meetu', ['ngResource']);

meetu.controller('indexController', function ($scope, $http, $resource) {
    var Meetup = $resource('/api/Meetups');
    Meetup.query(function (data) {
        $scope.meetups = data;
    });

    //for testing
    $scope.deleteOneJoin = function () {
        var Join = $resource('/api/Joins')

        //belew is the example to delete.
        //Join.delete({
        //  "meetupId": 4,
        //  "userId": "b1d9d320-15cc-4d44-ad4d-9bd57d48ecd5"
        //});
        alert("delete test function called");
    }
});