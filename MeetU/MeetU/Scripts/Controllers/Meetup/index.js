(function () {
    "use strict";
    angular
        .module('meetupModule', ['ngResource', 'angularMoment', 'ngRoute', 'angular-confirm', 'ui.bootstrap.tpls', 'ngImgCrop'])
        .controller('MeetupIndexController', MeetupIndexController)
    MeetupIndexController.$inject = ["$scope", "$resource", "$q", "$log", "$confirm"];
    function MeetupIndexController($scope, $resource, $q, $log, $confirm) {
        var vm = this;

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
        vm.hasLoaded = false;
        vm.hasFetchedAll = false;
        $q.all([
            Meetup.query({ start: currentShowingMeetupCount, amount: MEETUPS_PER_PAGE * 2 }, function (data) {
                vm.meetupViews = data;
                currentShowingMeetupCount += MEETUPS_PER_PAGE * 2;
            }).$promise,
            Userview.query(function (userViews) {
                vm.userId = userViews[0].userId;
                vm.userName = userViews[0].userName;
            }).$promise
        ]).then(function () {
            vm.hasLoaded = true;
            $(window).scroll(bindScroll);
            if ($(window).scrollTop() + $(window).height() > $(document).height()) {
                triggerMeetupLoading();
            }
        });
        //
        // Meetup pagination: bind an event handler to window.scroll event
        // trigger it when scrolled to bottom-100px
        //
        function triggerMeetupLoading() {
            var actualFetchedDataCount = 0;
            vm.hasLoaded = false;
            Meetup.query({ start: currentShowingMeetupCount, amount: MEETUPS_PER_PAGE }, function (data) {
                vm.meetupViews.push.apply(vm.meetupViews, data);
                actualFetchedDataCount = data.length;
                currentShowingMeetupCount += actualFetchedDataCount;
                if (actualFetchedDataCount < 5) {
                    vm.hasFetchedAll = true;
                }
                if (!vm.hasFetchedAll) {
                    $(window).bind('scroll', bindScroll);
                } else {
                    vm.hasLoaded = true;
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
        // Unbind scroll event when the scope of the controller is destroyed
        //
        $scope.$on('$destroy', function () {
            $(window).unbind('scroll');
        });

        //
        //  Check if logged userId has joined.
        //
        vm.isIn = function (js) {
            return js.some(function (j) { return j.userId === vm.userId; });
        };
        //
        //  Handle join/quit  button  
        //
        vm.toggleJoin = function (mview) {
            if (vm.isIn(mview.joins)) {
                Join.delete({
                    "meetupId": mview.meetup.id,
                    "userId": vm.userId
                }).$promise.then(
                // if success:
                function () {
                    var joins = mview.joins;
                    joins.splice(joins.findIndex(function (c) { return c.userId === vm.userId; }), 1);
                },
                //if rejected:
                function (e) {
                    $log.error(e);
                });
            }
            else {
                var j = new Join({
                    meetupId: mview.meetup.id,
                    userId: vm.userId
                });

                j.$save(function () {
                    mview.joins.push({
                        meetupId: mview.meetup.id,
                        userId: vm.userId,
                        userName: vm.userName //append username locally only
                    });
                });
            }
        };
        //
        //  toggle Meetup details
        //
        vm.toggleDetail = function (meetupView, isDetailShowing) {
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
        vm.addComment = function (mview) {
            var c = new CommentView({
                "content": mview.newComment,
                "by": vm.userId,
                "meetupId": mview.meetup.id
            });
            c.$save(c, function (response) {
                mview.commentData.push({
                    "id": response.id,
                    "content": mview.newComment,
                    "by": vm.userName,
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
        vm.deleteComment = function (meetupView, commentId) {
            $confirm({ text: 'Are you sure you want to delete this comment?', title: 'Deleting a comment', ok: 'Yes', cancel: 'No' })
                .then(function () {
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
                });
        }
        //
        //  Delete Meetup
        //
        vm.deleteMeetup = function (meetupId) {
            // TODO: need to show a joined name list in confirm box
            $confirm({ text: 'Are you sure you want to delete this MeetUp?', title: 'Deleting a meetup', ok: 'Yes', cancel: 'No' })
                .then(function () {
                    Meetup.delete({ id: meetupId })
                        .$promise
                        .then(function () {
                            var meetupViews = vm.meetupViews;
                            meetupViews.splice(meetupViews.findIndex(function (meetup) {
                                return meetup.meetup.id === meetupId;
                            }), 1);
                        }, function (message) {
                            $log.error(message);
                        });
                });
        }
        //
        //  Generate joined user names 
        //
        vm.parseParticipantName = function (participant) {
            return '@' + participant.userName.muStrip('@').muCapitalizeFirstLetter();
        };
        //
        //  Strip and Capitalize first letter for scope
        //
        vm.polishUserName = function (name) {
            return name.muStrip('@').muCapitalizeFirstLetter();
        };
    }//End of controller
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
