var meetu = angular.module('meetu', ['ngResource']);

meetu.controller('indexController', function ($scope, $http, $resource) {
    var Meetup = $resource('/api/Meetups');
    Meetup.query(function (data) {
        $scope.meetups = data;
    });
    
    var LoggedAs = $resource('/api/loggedUser');
    LoggedAs.query(function (users) {
        $scope.user = users[0];
    });

    //Check if logged user is in the relationship table, the argument can be either Joins or Watches of one meetup.
    $scope.isIn = function (arr) {
        for (var i in arr) {
            if (arr[i].userId == $scope.user)
                return true;
        }
        return false;
    }


    //for testing
    $scope.test = function () {
        var Join = $resource('/api/Joins');
        //var LoggedUser = $resource('/api/loggedUser');
        //LoggedUser.get(function (data) {
        //    alert(JSON.stringify( data ));
        //});

        //belew is the example to delete.
        //Join.delete({
        //  "meetupId": 4,
        //  "userId": "b1d9d320-15cc-4d44-ad4d-9bd57d48ecd5"
        //});
        alert("delete test function called");
    }
});