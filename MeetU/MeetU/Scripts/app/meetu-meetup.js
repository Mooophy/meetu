//
//  This code implements controller 'meetupIndexController',
//  used for template file : ~/Meetups/Index.cshtml.
//
(function () {
    "use strict";
    angular
        .module('meetupModule', ['ngResource', 'angularMoment'])
        .controller('meetupIndexController', function ($scope, $resource, $q, $log, $filter) {
            //
            //  Lazy resources
            //
            var Meetup = $resource('/api/Meetups');
            var Userview = $resource('/api/loggedUser');
            var Join = $resource('/api/Joins');
            var CommentView = $resource('/api/Comments/');

            function initializeComment() {
                angular.forEach($scope.meetupViews, function (value) {
                    value.commentData = $filter('filter')($scope.allCommentViews, function (comment) {
                        return value.meetup.id === comment.meetupId;
                    });
                    value.commentCount = value.commentData.length;
                });
            };

            function initializeData() {
                initializeComment();
            }

            //
            //  Queries
            //
            $scope.hasLoaded = false;
            $q.all([
                Meetup.query(function (data) {
                    $scope.meetupViews = data;
                }).$promise,
                Userview.query(function (userViews) {
                    $scope.userId = userViews[0].userId;
                    $scope.userName = userViews[0].userName;
                }).$promise,
                CommentView.query(function (data) {
                    $scope.allCommentViews = data;
                }).$promise
            ]).then(function () {
                initializeData();
                $scope.hasLoaded = true;
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
                        var joins = mview.joins;
                        joins.splice(joins.findIndex(function (c) { return c.userId === $scope.userId; }), 1);
                    },
                    //if rejected:
                    function (e) {
                        $log.error(e);
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
                    "meetupId": mview.meetup.id
                });
                c.$save(c, function (response) {
                    $scope.allCommentViews.push({
                        "id": response.id,
                        "content": mview.newComment,
                        "by": $scope.userName,
                        "meetupId": mview.meetup.id,
                        "at": response.at
                    });
                    mview.newComment = "";
                        initializeComment();
                    },
                    function (e) {
                        $log.error(e);
                    }
                );
            };
            //
            //  Delete comment 
            //
            $scope.deleteComment = function (commentId) {
                if (confirm("Are you sure you want to delete this comment?")) {
                    CommentView
                        .delete({ id: commentId })
                        .$promise
                        .then(function () {
                            var comments = $scope.allCommentViews;
                            comments.splice(comments.findIndex(function (c) { return c.id === commentId; }), 1);
                            initializeComment();
                        }, function(message) {
                            $log.error(message);
                        });
                }
            };
            //
            //  Generate joined user names 
            //
            $scope.parseParticipantName = function (participant) {
                return '@' + participant.userName.muStrip('@').muCapitalizeFirstLetter();
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
