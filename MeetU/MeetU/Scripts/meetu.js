var meetu = angular.module('meetu', []);
meetu.controller('indexController', function ($scope, $http) {
    $http.get('api/Meetups').success(function (meetupList) {
        $scope.meetups = meetupList;
    }).error(function (e) {
        alert(e);
    });
});