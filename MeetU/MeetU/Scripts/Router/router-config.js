(function () {
    "use strict";

    angular
        .module("meetupModule")
        .config(route);
    route.$inject = ["$routeProvider"];
    function route($routeProvider) {
        $routeProvider
            .when('/index', {
                templateUrl: '/Scripts/Views/Meetup/Index.html'
            })
            .when('/Profile/:profileId', {
                templateUrl: '/Scripts/Views/Profile/profile-display.html'
            })
            .when('/Meetup/Create', {
                templateUrl: '/Scripts/Views/Meetup/Create.html'
            })
            .when('/Meetup/Edit/:id', {
                templateUrl: '/Scripts/Views/Meetup/edit.html'
            })
            .otherwise({
                redirectTo: '/index'
            });
    }
})()