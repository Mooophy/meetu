(function () {
    "use strict";
    angular
        .module("meetupModule")
        .directive('subpageNav', function () {
            return {
                restrict: 'E',
                transclude: true,
                scope: {
                    'hassubmit': '=',
                    'submit': '&onSubmit',
                    'back': '&onBack',
                    'title': '='
                },
                templateUrl: '/Scripts/Directives/subpage-nav/subpage-nav.html'
            };
        });
})()
