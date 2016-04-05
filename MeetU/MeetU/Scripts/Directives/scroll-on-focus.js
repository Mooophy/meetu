(function () {
    "use strict";
    angular
        .module("meetupModule")
        .directive('scrollOnFocus', function () {
            return {
                restrict: 'A',
                link: function (scope, $element) {
                    $element.focus(function () {
                        $("body").animate({ scrollTop: $element.offset().top }, 1000);
                    });
                }
            };
        });
})();