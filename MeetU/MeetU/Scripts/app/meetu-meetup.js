﻿//
//  This code implements controller 'meetupIndexController',
//  used for template file : ~/Meetups/Index.cshtml.
//
(function () {
    "use strict";
    angular
        .module('meetupModule', ['ngResource', 'angularMoment'])
        .controller('meetupIndexController', function ($scope, $resource, $q, $log) {

            var currentShowingMeetupCount = 0;
            var MEETUPS_PER_PAGE = 5;
            //
            //  Lazy resources
            //
            var Meetup = $resource('/api/Meetups');
            var Userview = $resource('/api/loggedUser');
            var Join = $resource('/api/Joins');
            var CommentView = $resource('/api/Comments/');
            //
            //  Queries
            //
            $scope.hasLoaded = false;
            $q.all([
                Meetup.query({ start: currentShowingMeetupCount, amount: MEETUPS_PER_PAGE }, function (data) {
                    $scope.meetupViews = data;
                    currentShowingMeetupCount += MEETUPS_PER_PAGE;
                }).$promise,
                Userview.query(function (userViews) {
                    $scope.userId = userViews[0].userId;
                    $scope.userName = userViews[0].userName;
                }).$promise
            ]).then(function () {
                $scope.hasLoaded = true;
                //todo: should be unbound at some moment;
                $(window).scroll(bindScroll);
                if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
                    triggerMeetupLoading();
                }
            });
            //
            // Meetup pagination: bind an event handler to window.scroll event
            // trigger it when scrolled to bottom-100px
            //
            function triggerMeetupLoading() {
                var hasFetchedAll = false;
                var actualFetchedDataCount = 0;
                Meetup.query({ start: currentShowingMeetupCount, amount: MEETUPS_PER_PAGE }, function (data) {
                    $scope.meetupViews.push.apply($scope.meetupViews, data);
                    actualFetchedDataCount = data.length;
                    currentShowingMeetupCount += actualFetchedDataCount;
                    if (actualFetchedDataCount < 5) {
                        hasFetchedAll = true;
                    }
                    if (!hasFetchedAll) {
                        $(window).bind('scroll', bindScroll);
                    }
                });
            }
            var bindScroll = _.debounce(function () {
                if ($(window).scrollTop() + $(window).height() > $(document).height() - 100) {
                    $(window).unbind('scroll');
                    triggerMeetupLoading();
                }
            }, 200);

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
            //  toggle Meetup details
            //
            $scope.toggleDetail = function (meetupView, isDetailShowing) {
                if (isDetailShowing) {
                    meetupView.commentCount = 0;
                    CommentView.query({ meetupId: meetupView.meetup.id }, function (data) {
                        meetupView.commentData = data;
                        meetupView.commentCount = meetupView.commentData.length;
                    });
                }
            }
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
                    mview.commentData.push({
                        "id": response.id,
                        "content": mview.newComment,
                        "by": $scope.userName,
                        "meetupId": mview.meetup.id,
                        "at": response.at
                    });
                    mview.newComment = "";
                    mview.commentCount = mview.commentData.length;
                },
                    function (e) {
                        $log.error(e);
                    }
                );
            };
            //
            //  Delete comment 
            //
            $scope.deleteComment = function (meetupView, commentId) {
                if (confirm("Are you sure you want to delete this comment?")) {
                    CommentView
                        .delete({ id: commentId })
                        .$promise
                        .then(function () {
                            var comments = meetupView.commentData;
                            comments.splice(comments.findIndex(function (c) {
                                return c.id === commentId;
                            }), 1);
                            meetupView.commentCount = meetupView.commentData.length;
                        }, function (message) {
                            $log.error(message);
                        });
                }
            };

            $scope.deleteMeetup = function (meetupId) {

                // TODO: need to show a joined name list in confirm box
                if (confirm("Are you sure you want to delete this MeetUp?")) {
                    Meetup.delete({ id: meetupId })
                        .$promise
                        .then(function () {
                            var meetupViews = $scope.meetupViews;
                            meetupViews.splice(meetupViews.findIndex(function (meetup) {
                                return meetup.meetup.id === meetupId;
                            }), 1);
                        }, function (message) {
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
