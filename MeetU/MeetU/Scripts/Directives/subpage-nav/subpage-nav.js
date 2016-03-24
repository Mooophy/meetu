(function () {
    "use strict";
    angular
        .module("meetupModule")
        .directive('subpageNav', function () {
            return {
                restrict: 'E',
                transclude: true,
                scope: {
                    'cancel': '&onCancel',
                    'submit': '&onSubmit',
                    'back': '&onBack',
                    'title': '='
                },
                templateUrl: 'subpage-nav.html'
            };
        });
})()
