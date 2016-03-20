(function () {
    "use strict";
    angular
        .module("meetupModule")
        .config(function ($routeProvider, $locationProvider) {
            $routeProvider
                .when('/Profile/:profileId', {
                    templateUrl: '../../Views/Manage/meetup-profile.html',
                    controller: 'ProfileController'
                });
            // configure html5 to get links working on jsfiddle
            $locationProvider.html5Mode(true);
        });
})()