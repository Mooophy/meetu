//
//  This code implements controller 'meetupIndexController',
//  used for template file : ~/Meetups/Index.cshtml.
//
(function () {
    "use strict";
    angular
        .module('meetupModule', ['ngResource', 'angularMoment'])
        .controller('meetupIndexController', function ($scope, $resource) {
            //
            //  Lazy resources
            //
            var Meetup = $resource('/api/Meetups');
            var Userview = $resource('/api/loggedUser');
            var Join = $resource('/api/Joins');
            var CommentView = $resource('/api/Comments/');

            $scope.hasLoaded = false;

            //
            //  Queries
            //
            Meetup.query(function(data) {
                $scope.meetupViews = data;
            }).$promise.then(function() {
                Userview.query(function(userViews) {
                    $scope.userId = userViews[0].userId;
                    $scope.userName = userViews[0].userName;
                }).$promise.then(function() {
                    CommentView.query(function(data) {
                        $scope.allCommentViews = data;
                    }).$promise.then(function() {
                        $scope.hasLoaded = true;
                    });
                });
            });
            //
            //  Check if logged userId has joined.
            //
            $scope.isIn = function (js) {
                return js.some(function (j) { return j.userId === $scope.userId; });
            };
            //
            //  Handle join/quit  button  
            //
            $scope.toggleJoin = function (mview) {
                if ($scope.isIn(mview.joins)) {
                    Join.delete({
                        "meetupId": mview.meetup.id,
                        "userId": $scope.userId
                    }).$promise.then(
                    // if success:
                    function () {
                        mview.joins.splice(mview.joins.indexOf($scope.userId), 1);
                    },
                    //if rejected:
                    function (e) {
                        console.log(e);
                    });
                }
                else {
                    var j = new Join({
                        meetupId: mview.meetup.id,
                        userId: $scope.userId
                    });

                    j.$save(function () {
                        mview.joins.push({
                            meetupId: mview.meetup.id,
                            userId: $scope.userId,
                            userName: $scope.userName //append username locally only
                        });
                    });
                }
            };
            //
            //  Add comment to backend
            //  If succeed, push to local array, otherwise: log it
            //
            $scope.addComment = function (mview) {
                var c = new CommentView({
                    "content": mview.newComment,
                    "by": $scope.userId,
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
                    function (e) {
                        console.log(e);
                    }
                );
            };
            //
            //  Generate joined user names 
            //
            $scope.joinedUserNames = function (js) {
                return js.map(function (j) {
                        return '@' + j.userName.muStrip('@').muCapitalizeFirstLetter();
                    }).join(' ');
            };
            //
            //  Strip and Capitalize first letter for scope
            //
            $scope.polishUserName = function (name) {
                return name.muStrip('@').muCapitalizeFirstLetter();
            };
        });//End of controller
    //
    //  Strip the string specified
    //  To be tested later on
    //
    String.prototype.muStrip = function (strToStrip) {
        if (this.indexOf(strToStrip) < 0)
            return this;
        else
            return this.substring(0, this.indexOf(strToStrip));
    };
    //
    //  Capitalize the first letter
    //  To be tested later on
    //
    String.prototype.muCapitalizeFirstLetter = function () {
        return this.charAt(0).toUpperCase() + this.slice(1);
    };

})();