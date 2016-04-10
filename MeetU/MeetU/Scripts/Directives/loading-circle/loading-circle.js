(function () {
    "use strict";
    angular
        .module("meetupModule")
        .directive('loadingCircle', function () {
            return {
                restrict: 'E',
                scope: {
                    'size': '='
                },
                templateUrl: '/Scripts/Directives/loading-circle/loading-circle.html'
            };
        });
})();