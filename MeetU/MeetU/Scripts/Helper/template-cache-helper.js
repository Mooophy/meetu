(function () {
    "use strict";
    angular
        .module('meetupModule')
        .run(templateCache);
    templateCache.$inject = ['$templateCache', '$http', ];
    function templateCache ($templateCache, $http) {
        $http.get('/Scripts/Views/Meetup/Index.html', { cache: $templateCache });
        $http.get('/Scripts/Views/Profile/profile-display.html', { cache: $templateCache });
        $http.get('/Scripts/Views/Meetup/Create.html', { cache: $templateCache });
        $http.get('/Scripts/Directives/subpage-nav/subpage-nav.html', { cache: $templateCache });
        $http.get('/Scripts/Directives/loading-circle/loading-circle.html', { cache: $templateCache });
    }
})();
