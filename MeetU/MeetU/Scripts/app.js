var app = angular.module('meetuApp', ['ngResource']);

app.controller('indexController', function ($scope, $http, $resource) {
    var Meetup = $resource('/api/Meetups');
    var LoggedAs = $resource('/api/loggedUser');
    var Join = $resource('/api/Joins');

    Meetup.query(function (data) {
        $scope.meetupViews = data;
    });

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

    $scope.toggleJoin = function (mview) {
        if ($scope.isIn(mview.joins)) {
            Join.delete({
                "meetupId": mview.meetup.id,
                "userId": $scope.user
            }).$promise.then(
            // if success:
            function () {
                for (var i in mview.joins) {
                    if (mview.joins[i].userId == $scope.user) {
                        mview.joins.splice(i, 1);
                        break;
                    }
                }
            },
            //if rejected
            function (e) {
                alert(e);
            });
        }
        else {
            var newJoin = {
                meetupId: mview.meetup.id,
                userId: $scope.user
            };
            var j = new Join(newJoin);
            j.$save(function () {
                mview.joins.push(newJoin);
            });
        }
    }

    var Comment = $resource('/api/Comments/:meetupId//byMeetupId');
    Comment.query({meetupId: 8},function (data) {
        $scope.comments = data;
    })

});