var meetu = angular.module('meetu', ['ngResource']);

meetu.controller('indexController', function ($scope, $http, $resource) {
    var Meetup = $resource('/api/Meetups');
    Meetup.query(function (data) {
        $scope.meetups = data;
    });

    //for testing
    $scope.testCreateNewJoin = function(){
        var Join = $resource('/api/Joins')
        var join = new Join();
        Join.query(function (data) {
            alert(data.length);
        })
    }
});