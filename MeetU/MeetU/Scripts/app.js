var app = angular.module('meetuApp', ['ngResource']);

app.controller('indexController', function ($scope, $http, $resource, $filter) {
    var Meetup = $resource('/api/Meetups');
    var Userview = $resource('/api/loggedUser');
    var Join = $resource('/api/Joins');
    var CommentView = $resource('/api/Comments/');

    Meetup.query(function (data) {
        $scope.meetupViews = data;
    });
    Userview.query(function (userViews) {
        $scope.user = userViews[0].userId;
        $scope.userName = userViews[0].userName;
    });
    CommentView.query(function (data) {
        $scope.allCommentViews = data;
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
            function (e) { console.log(e); });
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
    //
    //  add comment to server, if successful: push it into local array, otherwise: write log.
    //
    $scope.addComment = function (mview) {
        var c = new CommentView({
            "content": mview.newComment,
            "by": $scope.user,
            "meetupId": mview.meetup.id,
        });
        c.$save(
            function () {
                $scope.allCommentViews.push({
                    "content": mview.newComment,
                    "by": $scope.userName,
                    "meetupId": mview.meetup.id,
                    "at": new Date
                });
                mview.newComment = "";
            },
            function (e) { console.log(e); }
        );
    };
});

// here : date should be type : Date
function convertToTimeAgo(date) {

    //something like :
    return "3 days ago";
}