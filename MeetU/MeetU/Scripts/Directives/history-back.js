(function () {
    "use strict";
    angular
        .module("meetupModule")
        .directive('back', back)
    back.$inject = ["$window"];
    function back($window) {
        return {
            restrict: 'A',
            link: function (scope, elem, attrs) {
                elem.bind('click', function () {
                    $window.history.back();
                });
            }
        };
    }
})();
